using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagementSystems.Models;
using RestaurantManagementSystems.Data;
using RestaurantManagementSystems.ViewModels;

namespace RestaurantManagementSystems.Controllers
{
    public class ShoppingCartController : Controller
{
        

        public ShoppingCartController(ApplicationDbContext context)
        {
            storeDB = context;
        }
        public ApplicationDbContext storeDB { get; }

        //
        // GET: /ShoppingCart/
    public ActionResult Index()
    {
        var cart = ShoppingCart.GetCart(this.HttpContext,storeDB);
        // Set up our ViewModel
        var viewModel = new ShoppingCartViewModel
        {
            CartItems = cart.GetCartItems(storeDB),
            CartTotal = cart.GetTotal(storeDB),
        };
        // Return the view
        return View(viewModel);
    }
        //
        // GET: /Store/AddToCart/5
        public ActionResult AddToCart(int id)
    {
        // Retrieve the menu from the database
        var addedMenu = storeDB.MenuItmes
        .Single(MenuItem => MenuItem.MenuID == id);
        // Add it to the shopping cart
        var cart = ShoppingCart.GetCart(this.HttpContext,storeDB);
        cart.AddToCart(addedMenu, storeDB);
        // Go back to the main store page for more shopping
        return RedirectToAction("Index1", "MenuItems");
    }
    //
    // AJAX: /ShoppingCart/RemoveFromCart/5

    [HttpPost]
    public ActionResult RemoveFromCart(int id)
    {
        // Remove the item from the cart
        var cart = ShoppingCart.GetCart(this.HttpContext,storeDB);
            // Get the name of the album to display confirmation
            int MenuId = storeDB.Carts
            .Single(item => item.RecordId == id).MenuItemId;

             string menuname= storeDB.MenuItmes
            .Single(item => item.MenuID == MenuId).Name;
            // Remove from cart
            int itemCount = cart.RemoveFromCart(id,storeDB);
        // Display the confirmation message
        var results = new ShoppingCartRemoveViewModel
        {
            Message = menuname +
            " has been removed from your shopping cart.",
            CartTotal = cart.GetTotal(storeDB),
            CartCount = cart.GetCount(storeDB),
            ItemCount = itemCount,
            DeleteId = id
        };
        return Json(results);
    }
//
    // GET: /ShoppingCart/CartSummary
    
    //[ChildActionOnly]
    public ActionResult CartSummary()
    {
        var cart = ShoppingCart.GetCart(this.HttpContext,storeDB);
        ViewData["CartCount"] = cart.GetCount(storeDB);
        return PartialView("CartSummary");
    }
}
}
