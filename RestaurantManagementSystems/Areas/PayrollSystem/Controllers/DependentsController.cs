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
    public class DependentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DependentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PayrollSystem/Dependents
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Dependent.Include(d => d.Employee);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Index1(int? id)
        {
            return View(_context.Dependent.Include(o => o.Employee).ToListAsync().Result.FindAll(m => m.Employee.EmployeeID == id));
            //return View(_context.MenuItmes.ToListAsync().Result.FindAll(m => m.TodaysMenu.Value == MenuItem.InTodaysMenu.Yes));
        }

        // GET: PayrollSystem/Dependents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dependent = await _context.Dependent
                .Include(d => d.Employee)
                .FirstOrDefaultAsync(m => m.DependentID == id);
            if (dependent == null)
            {
                return NotFound();
            }

            return View(dependent);
        }

        // GET: PayrollSystem/Dependents/Create
        public IActionResult Create()
        {
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "EmployeeID", "EmployeeID");
            return View();
        }

        // POST: PayrollSystem/Dependents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DependentID,Name,Relationship,PhoneNo,EmployeeID")] Dependent dependent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dependent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "EmployeeID", "EmployeeID", dependent.EmployeeID);
            return View(dependent);
        }

        // GET: PayrollSystem/Dependents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dependent = await _context.Dependent.FindAsync(id);
            if (dependent == null)
            {
                return NotFound();
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "EmployeeID", "EmployeeID", dependent.EmployeeID);
            return View(dependent);
        }

        // POST: PayrollSystem/Dependents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DependentID,Name,Relationship,PhoneNo,EmployeeID")] Dependent dependent)
        {
            if (id != dependent.DependentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dependent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DependentExists(dependent.DependentID))
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
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "EmployeeID", "EmployeeID", dependent.EmployeeID);
            return View(dependent);
        }

        // GET: PayrollSystem/Dependents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dependent = await _context.Dependent
                .Include(d => d.Employee)
                .FirstOrDefaultAsync(m => m.DependentID == id);
            if (dependent == null)
            {
                return NotFound();
            }

            return View(dependent);
        }

        // POST: PayrollSystem/Dependents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dependent = await _context.Dependent.FindAsync(id);
            _context.Dependent.Remove(dependent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DependentExists(int id)
        {
            return _context.Dependent.Any(e => e.DependentID == id);
        }
    }
}
