using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystems.Data;
using RestaurantManagementSystems.Models;

namespace RestaurantManagementSystems.Views.OrderDetails
{
    public class DeleteModel : PageModel
    {
        private readonly RestaurantManagementSystems.Data.ApplicationDbContext _context;

        public DeleteModel(RestaurantManagementSystems.Data.ApplicationDbContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OrderDetail = await _context.OrderItems.FindAsync(id);

            if (OrderDetail != null)
            {
                _context.OrderItems.Remove(OrderDetail);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
