using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystems.Data;
using RestaurantManagementSystems.Models;

namespace RestaurantManagementSystems.Controllers
{
    public class OrderDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OrderDetails
        public async Task<IActionResult> Report()
        {
            //var applicationDbContext = _context.OrderItems.Include(o => o.Menus).Include(o => o.Order);
            
            return View("Report");
        }

        // GET: OrderDetails
        public async Task<IActionResult> Reporting()
        {
            //var applicationDbContext = _context.OrderItems.Include(o => o.Menus).Include(o => o.Order).Select(m => m.Order.OrderDate.Date == DateTime.Today.Date);

            //var v = _context.OrderItems.Select(m => m.Order.OrderDate.Date == DateTime.Today.Date);
            var query = from c in _context.OrderItems.Where(m => m.Order.OrderDate.Date == DateTime.Today.Date)
                        group c by c.Menus.Name into g
                        select new
                        {
                            MenuName = g.Key,
                            //MenuName = g.Select(p => p.Menus.Name) ,
                            NoOfOrder = g.Select(m => m.Quantity).Sum(),
                            UnitPrice = g.Select(n => n.UnitPrice).Take(1)
                        };


            return Json(query);
        }

        // GET: OrderDetails
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.OrderItems.Include(o => o.Menus).Include(o => o.Order);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: OrderDetails
        public async Task<IActionResult> Index1(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var applicationDbContext = _context.OrderItems.ToListAsync().Result.AsQueryable().Where(m => m.OrderID == id);
            return View(applicationDbContext.ToList());
        }

        // GET: OrderDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderItems
                .Include(o => o.Menus)
                .Include(o => o.Order)
                .FirstOrDefaultAsync(m => m.OrderItemID == id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        // GET: OrderDetails/Create
        public IActionResult Create()
        {
            ViewData["MenuItemID"] = new SelectList(_context.MenuItmes, "MenuID", "MenuID");
            ViewData["OrderID"] = new SelectList(_context.Orders, "OrderID", "OrderID");
            return View();
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderItemID,OrderID,MenuItemID,UnitPrice,Quantity")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MenuItemID"] = new SelectList(_context.MenuItmes, "MenuID", "MenuID", orderDetail.MenuItemID);
            ViewData["OrderID"] = new SelectList(_context.Orders, "OrderID", "OrderID", orderDetail.OrderID);
            return View(orderDetail);
        }

        // GET: OrderDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderItems.FindAsync(id);
            if (orderDetail == null)
            {
                return NotFound();
            }
            ViewData["MenuItemID"] = new SelectList(_context.MenuItmes, "MenuID", "MenuID", orderDetail.MenuItemID);
            ViewData["OrderID"] = new SelectList(_context.Orders, "OrderID", "OrderID", orderDetail.OrderID);
            return View(orderDetail);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderItemID,OrderID,MenuItemID,UnitPrice,Quantity")] OrderDetail orderDetail)
        {
            if (id != orderDetail.OrderItemID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderDetailExists(orderDetail.OrderItemID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MenuItemID"] = new SelectList(_context.MenuItmes, "MenuID", "MenuID", orderDetail.MenuItemID);
            ViewData["OrderID"] = new SelectList(_context.Orders, "OrderID", "OrderID", orderDetail.OrderID);
            return View(orderDetail);
        }

        // GET: OrderDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderItems
                .Include(o => o.Menus)
                .Include(o => o.Order)
                .FirstOrDefaultAsync(m => m.OrderItemID == id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderDetail = await _context.OrderItems.FindAsync(id);
            _context.OrderItems.Remove(orderDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderDetailExists(int id)
        {
            return _context.OrderItems.Any(e => e.OrderItemID == id);
        }
    }
}
