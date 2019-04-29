using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystems.Data;
using RestaurantManagementSystems.Models;

namespace RestaurantManagementSystems.Views
{
    public class DetailsModel : PageModel
    {
        private readonly RestaurantManagementSystems.Data.ApplicationDbContext _context;

        public DetailsModel(RestaurantManagementSystems.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Order Order { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order = await _context.Orders.FirstOrDefaultAsync(m => m.OrderID == id);

            if (Order == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
