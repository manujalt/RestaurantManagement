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
    public class VendorTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VendorTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: VendorTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.VendorType.ToListAsync());
        }

        // GET: VendorTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorType = await _context.VendorType
                .FirstOrDefaultAsync(m => m.VendorTypeId == id);
            if (vendorType == null)
            {
                return NotFound();
            }

            return View(vendorType);
        }

        // GET: VendorTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VendorTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VendorTypeId,VendorTypeName,Description")] VendorType vendorType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vendorType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vendorType);
        }

        // GET: VendorTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorType = await _context.VendorType.FindAsync(id);
            if (vendorType == null)
            {
                return NotFound();
            }
            return View(vendorType);
        }

        // POST: VendorTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VendorTypeId,VendorTypeName,Description")] VendorType vendorType)
        {
            if (id != vendorType.VendorTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendorType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendorTypeExists(vendorType.VendorTypeId))
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
            return View(vendorType);
        }

        // GET: VendorTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorType = await _context.VendorType
                .FirstOrDefaultAsync(m => m.VendorTypeId == id);
            if (vendorType == null)
            {
                return NotFound();
            }

            return View(vendorType);
        }

        // POST: VendorTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendorType = await _context.VendorType.FindAsync(id);
            _context.VendorType.Remove(vendorType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendorTypeExists(int id)
        {
            return _context.VendorType.Any(e => e.VendorTypeId == id);
        }
    }
}
