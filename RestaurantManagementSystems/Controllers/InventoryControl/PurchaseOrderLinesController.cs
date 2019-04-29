using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystems.Data;
using coderush.Models;

namespace RestaurantManagementSystems.Controllers.InventoryControl
{
    public class PurchaseOrderLinesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PurchaseOrderLinesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PurchaseOrderLines
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PurchaseOrderLine.Include(p => p.PurchaseOrder);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PurchaseOrderLines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseOrderLine = await _context.PurchaseOrderLine
                .Include(p => p.PurchaseOrder)
                .FirstOrDefaultAsync(m => m.PurchaseOrderLineId == id);
            if (purchaseOrderLine == null)
            {
                return NotFound();
            }

            return View(purchaseOrderLine);
        }

        // GET: PurchaseOrderLines/Create
        public IActionResult Create()
        {
            ViewData["PurchaseOrderId"] = new SelectList(_context.PurchaseOrder, "PurchaseOrderId", "PurchaseOrderId");
            return View();
        }

        // POST: PurchaseOrderLines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PurchaseOrderLineId,PurchaseOrderId,ProductId,Description,Quantity,Price,Amount,DiscountPercentage,DiscountAmount,SubTotal,TaxPercentage,TaxAmount,Total")] PurchaseOrderLine purchaseOrderLine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(purchaseOrderLine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PurchaseOrderId"] = new SelectList(_context.PurchaseOrder, "PurchaseOrderId", "PurchaseOrderId", purchaseOrderLine.PurchaseOrderId);
            return View(purchaseOrderLine);
        }

        // GET: PurchaseOrderLines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseOrderLine = await _context.PurchaseOrderLine.FindAsync(id);
            if (purchaseOrderLine == null)
            {
                return NotFound();
            }
            ViewData["PurchaseOrderId"] = new SelectList(_context.PurchaseOrder, "PurchaseOrderId", "PurchaseOrderId", purchaseOrderLine.PurchaseOrderId);
            return View(purchaseOrderLine);
        }

        // POST: PurchaseOrderLines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PurchaseOrderLineId,PurchaseOrderId,ProductId,Description,Quantity,Price,Amount,DiscountPercentage,DiscountAmount,SubTotal,TaxPercentage,TaxAmount,Total")] PurchaseOrderLine purchaseOrderLine)
        {
            if (id != purchaseOrderLine.PurchaseOrderLineId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchaseOrderLine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseOrderLineExists(purchaseOrderLine.PurchaseOrderLineId))
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
            ViewData["PurchaseOrderId"] = new SelectList(_context.PurchaseOrder, "PurchaseOrderId", "PurchaseOrderId", purchaseOrderLine.PurchaseOrderId);
            return View(purchaseOrderLine);
        }

        // GET: PurchaseOrderLines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseOrderLine = await _context.PurchaseOrderLine
                .Include(p => p.PurchaseOrder)
                .FirstOrDefaultAsync(m => m.PurchaseOrderLineId == id);
            if (purchaseOrderLine == null)
            {
                return NotFound();
            }

            return View(purchaseOrderLine);
        }

        // POST: PurchaseOrderLines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchaseOrderLine = await _context.PurchaseOrderLine.FindAsync(id);
            _context.PurchaseOrderLine.Remove(purchaseOrderLine);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseOrderLineExists(int id)
        {
            return _context.PurchaseOrderLine.Any(e => e.PurchaseOrderLineId == id);
        }
    }
}
