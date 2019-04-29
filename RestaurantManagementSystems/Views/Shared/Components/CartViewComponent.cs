using Microsoft.AspNetCore.Mvc;
using RestaurantManagementSystems.Data;
using RestaurantManagementSystems.Models;
using RestaurantManagementSystems.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantManagementSystems.Controllers
{
    [ViewComponent(Name = "CartView")]
    public class CartViewComponent: ViewComponent
    {

        public CartViewComponent(ApplicationDbContext context)
        {
            storeDB = context;
        }
        public ApplicationDbContext storeDB { get; }

        // GET: /ShoppingCart/Index1
        public async Task<IViewComponentResult> InvokeAsync(int maxPriority, bool isDone)
        {
            var cart = ShoppingCart.GetCart(this.HttpContext, storeDB);
            // Set up our ViewModel
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(storeDB),
                CartTotal = cart.GetTotal(storeDB)
            };
            // Return the view
            return View(viewModel);
        }

        
        //
        // AJAX: /ShoppingCart/RemoveFromCart/5

        //[HttpPost]
        public async Task<IViewComponentResult> RemoveFromCart(int id)
        {
            // Remove the item from the cart
            var cart = ShoppingCart.GetCart(this.HttpContext, storeDB);
            // Get the name of the album to display confirmation
            string Menu = storeDB.Carts
            .Single(item => item.RecordId == id).Menu.Name;
            // Remove from cart
            int itemCount = cart.RemoveFromCart(id, storeDB);
            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = System.Web.HttpUtility.HtmlEncode(Menu) +
                " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(storeDB),
                CartCount = cart.GetCount(storeDB),
                ItemCount = itemCount,
                DeleteId = id
            };
            return View(results);
        }
        //
        // GET: /ShoppingCart/CartSummary

        //[ChildActionOnly]
        public async Task<IViewComponentResult> CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext, storeDB);
            ViewData["CartCount"] = cart.GetCount(storeDB);
            return View("CartSummary");
        }
    }
}
