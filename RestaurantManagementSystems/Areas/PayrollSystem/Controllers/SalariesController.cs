using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystems.Data;
using RestaurantManagementSystems.PayrollSystem.Models;

namespace RestaurantManagementSystems.Areas.PayrollSystem.Controllers
{
    [Area("PayrollSystem")]
    public class SalariesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalariesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PayrollSystem/Salaries
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Salaries.Include(s => s.Employee);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PayrollSystem/Salaries
        public async Task<IActionResult> Index1(int? id)
        {
            //return View(await _context.Salaries.ToListAsync());
            if (id == null)
            {
                return NotFound();
            }
            //var salaries = _context.Salaries.Where(m => m.Employee.EmployeeID == id);
            //if (salaries == null)
            //{
            //    return NotFound();
            //}
            return View(_context.Salaries.Where(m => m.EmployeeID == id).OrderBy(p => p.SailaryMonth.Date));
        }

        // GET: PayrollSystem/Salaries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salaries = await _context.Salaries
                .Include(s => s.Employee)
                .FirstOrDefaultAsync(m => m.SailaryID == id);
            if (salaries == null)
            {
                return NotFound();
            }

            return View(salaries);
        }

        // GET: PayrollSystem/Salaries/Create
        public IActionResult Create()
        {
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "EmployeeID", "EmployeeID");
            return View();
        }

        // POST: PayrollSystem/Salaries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SailaryID,SailaryMonth,SailaryAmount,OtherGifts,EmployeeID")] Salaries salaries)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salaries);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "EmployeeID", "EmployeeID", salaries.EmployeeID);
            return View(salaries);
        }

        // GET: PayrollSystem/Salaries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salaries = await _context.Salaries.FindAsync(id);
            if (salaries == null)
            {
                return NotFound();
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "EmployeeID", "EmployeeID", salaries.EmployeeID);
            return View(salaries);
        }

        // POST: PayrollSystem/Salaries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SailaryID,SailaryMonth,SailaryAmount,OtherGifts,EmployeeID")] Salaries salaries)
        {
            if (id != salaries.SailaryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salaries);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalariesExists(salaries.SailaryID))
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
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "EmployeeID", "EmployeeID", salaries.EmployeeID);
            return View(salaries);
        }

        // GET: PayrollSystem/Salaries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salaries = await _context.Salaries
                .Include(s => s.Employee)
                .FirstOrDefaultAsync(m => m.SailaryID == id);
            if (salaries == null)
            {
                return NotFound();
            }

            return View(salaries);
        }

        // POST: PayrollSystem/Salaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salaries = await _context.Salaries.FindAsync(id);
            _context.Salaries.Remove(salaries);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalariesExists(int id)
        {
            return _context.Salaries.Any(e => e.SailaryID == id);
        }
    }
}
