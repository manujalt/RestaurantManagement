using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantManagementSystems.Data;
using RestaurantManagementSystems.Models;

namespace RestaurantManagementSystems.Views.OrderDetails
{
    public class CreateModel : PageModel
    {
        private readonly RestaurantManagementSystems.Data.ApplicationDbContext _context;

        public CreateModel(RestaurantManagementSystems.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["MenuItemID"] = new SelectList(_context.MenuItmes, "MenuID", "MenuID");
        ViewData["OrderID"] = new SelectList(_context.Orders, "OrderID", "OrderID");
            return Page();
        }

        [BindProperty]
        public OrderDetail OrderDetail { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.OrderItems.Add(OrderDetail);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}