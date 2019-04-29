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
    public class PurchaseTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PurchaseTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PurchaseTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.PurchaseType.ToListAsync());
        }

        // GET: PurchaseTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseType = await _context.PurchaseType
                .FirstOrDefaultAsync(m => m.PurchaseTypeId == id);
            if (purchaseType == null)
            {
                return NotFound();
            }

            return View(purchaseType);
        }

        // GET: PurchaseTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PurchaseTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PurchaseTypeId,PurchaseTypeName,Description")] PurchaseType purchaseType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(purchaseType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(purchaseType);
        }

        // GET: PurchaseTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseType = await _context.PurchaseType.FindAsync(id);
            if (purchaseType == null)
            {
                return NotFound();
            }
            return View(purchaseType);
        }

        // POST: PurchaseTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PurchaseTypeId,PurchaseTypeName,Description")] PurchaseType purchaseType)
        {
            if (id != purchaseType.PurchaseTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchaseType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseTypeExists(purchaseType.PurchaseTypeId))
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
            return View(purchaseType);
        }

        // GET: PurchaseTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseType = await _context.PurchaseType
                .FirstOrDefaultAsync(m => m.PurchaseTypeId == id);
            if (purchaseType == null)
            {
                return NotFound();
            }

            return View(purchaseType);
        }

        // POST: PurchaseTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchaseType = await _context.PurchaseType.FindAsync(id);
            _context.PurchaseType.Remove(purchaseType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseTypeExists(int id)
        {
            return _context.PurchaseType.Any(e => e.PurchaseTypeId == id);
        }
    }
}
