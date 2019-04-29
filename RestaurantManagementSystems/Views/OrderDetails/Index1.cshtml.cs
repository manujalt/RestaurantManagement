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
    public class IndexModel1 : PageModel
    {
        private readonly RestaurantManagementSystems.Data.ApplicationDbContext _context;

        public IndexModel1(RestaurantManagementSystems.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<OrderDetail> OrderDetail { get;set; }

        public async Task OnGetAsync()
        {
            OrderDetail = await _context.OrderItems
                .Include(o => o.Menus)
                .Include(o => o.Order).ToListAsync();
        }
    }
}
