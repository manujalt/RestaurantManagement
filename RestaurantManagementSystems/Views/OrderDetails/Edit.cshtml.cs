using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystems.Data;
using RestaurantManagementSystems.Models;

namespace RestaurantManagementSystems.Views.OrderDetails
{
    public class EditModel : PageModel
    {
        private readonly RestaurantManagementSystems.Data.ApplicationDbContext _context;

        public EditModel(RestaurantManagementSystems.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OrderDetail OrderDetail { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OrderDetail = await _context.OrderItems
                .Include(o => o.Menus)
                .Include(o => o.Order).FirstOrDefaultAsync(m => m.OrderItemID == id);

            if (OrderDetail == null)
            {
                return NotFound();
            }
           ViewData["MenuItemID"] = new SelectList(_context.MenuItmes, "MenuID", "MenuID");
           ViewData["OrderID"] = new SelectList(_context.Orders, "OrderID", "OrderID");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(OrderDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderDetailExists(OrderDetail.OrderItemID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool OrderDetailExists(int id)
        {
            return _context.OrderItems.Any(e => e.OrderItemID == id);
        }
    }
}
