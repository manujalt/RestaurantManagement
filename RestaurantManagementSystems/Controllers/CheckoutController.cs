using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagementSystems.Models;
using RestaurantManagementSystems.Data;


namespace RestaurantManagementSystems.Controllers
{
[Authorize]
public class CheckoutController : Controller
{
        private readonly ApplicationDbContext storeDB;

        public CheckoutController(ApplicationDbContext context)
        {
            storeDB = context;
        }

        //ApplicationDbContext storeDB;
        const string PromoCode = "FREE";
        //
        // GET: /Checkout/AddressAndPayment
        public ActionResult AddressAndPayment()
        {
                return View();
        }
        //
        // POST: /Checkout/AddressAndPayment
        [HttpPost]
        public ActionResult AddressAndPayment(IFormCollection values)
        {
                var order = new Order();
                TryUpdateModelAsync(order);
                try
                {
                    if (string.Equals(values["PromoCode"], PromoCode,
                    StringComparison.OrdinalIgnoreCase) == false)
                    {
                        return View(order);
                    }
                    else
                    {
                        order.PersonName = User.Identity.Name;
                        order.OrderDate = DateTime.Now;
                        //Save Order
                        storeDB.Orders.Add(order);
                        storeDB.SaveChanges();
                        //Process the order
                        var cart = ShoppingCart.GetCart(this.HttpContext,storeDB);
                        cart.CreateOrder(order,storeDB);
                        return RedirectToAction("Complete", new { id = order.OrderID });
                    }
                }
                catch
                {
                    //Invalid - redisplay with errors
                    return View(order);
                }
        }
        //
        // GET: /Checkout/Complete
        public ActionResult Complete(int id)
        {
            // Validate customer owns this order
            bool isValid = storeDB.Orders.Any(
            o => o.OrderID == id &&
            o.PersonName == User.Identity.Name);
            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }

        }
}
}