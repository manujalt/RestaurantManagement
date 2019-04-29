using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using RestaurantManagementSystems.Data;
using System.Threading.Tasks;

namespace RestaurantManagementSystems.Models
{
    public partial class ShoppingCart
{
        //ApplicationDbContext storeDB;

        
        public const string CartSessionKey = "CartId";
        string ShoppingCartId { get; set; }

        public ShoppingCart(ApplicationDbContext context)
        {

            storeDB = context;
        }
        public static ApplicationDbContext storeDB;

        public ShoppingCart() { }




        public static ShoppingCart GetCart(HttpContext context,ApplicationDbContext aDBc)
        {
            storeDB = aDBc;
            var cart = new ShoppingCart();
                cart.ShoppingCartId = cart.GetCartId(context);
                return cart;
        }
        // Helper method to simplify shopping cart calls
        public static ShoppingCart GetCart(Controller controller, ApplicationDbContext Adbc)
        {
                return GetCart(controller.HttpContext, Adbc);
        }
        public void AddToCart(MenuItem menu,ApplicationDbContext aDBc)
        {
            // Get the matching cart and menu instances
            storeDB = aDBc;
                var cartItem = storeDB.Carts.FirstOrDefault(
                c => c.CartId == ShoppingCartId
                && c.MenuItemId == menu.MenuID);
                if (cartItem == null)
                {
                        // Create a new cart item if no cart item exists
                        cartItem = new Cart
                        {
                            MenuItemId = menu.MenuID,
                            CartId = ShoppingCartId,
                            Count = 1,
                            Menu=menu,
                            DateCreated = DateTime.Now
                        };
                        storeDB.Carts.Add(cartItem);
                }
                else
                {
                        // If the item does exist in the cart, then add one to the quantity
                        cartItem.Count++;
                }
                // Save changes
                storeDB.SaveChanges();
        }
        public int RemoveFromCart(int id, ApplicationDbContext aDBc)
        {
            // Get the cart
            storeDB = aDBc;
            var cartItem = storeDB.Carts.Single(
            cart => cart.CartId == ShoppingCartId
            && cart.RecordId == id);

                int itemCount = 0;
            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                cartItem.Count--;
                itemCount = cartItem.Count;
                }
                else
                {
                storeDB.Carts.Remove(cartItem);
                }
                // Save changes
                storeDB.SaveChanges();
            }
            return itemCount;
        }
        public void EmptyCart()
        {
                var cartItems = storeDB.Carts.Where(cart => cart.CartId == ShoppingCartId);
                foreach (var cartItem in cartItems)
                {
                    storeDB.Carts.Remove(cartItem);
                }
                // Save changes
                storeDB.SaveChanges();
        }
        public List<Cart> GetCartItems(ApplicationDbContext aDBc)
        {
            storeDB = aDBc;
               var v= storeDB.Carts.Where(cart => cart.CartId == ShoppingCartId).ToList();

            foreach(var v1 in v) { 
                var addedMenu = storeDB.MenuItmes
                .Single(MenuItem => MenuItem.MenuID ==v1.MenuItemId );
                v1.Menu = addedMenu;
            }

            return v;
        }
        public int GetCount(ApplicationDbContext aDBc)
        {
            storeDB = aDBc;
            // Get the count of each item in the cart and sum them up
            int? count = (from cartItems in storeDB.Carts
                where cartItems.CartId == ShoppingCartId
                select (int?)cartItems.Count).Sum();
                // Return 0 if all entries are null
                return count ?? 0;
        }
        public decimal GetTotal(ApplicationDbContext aDBc)
        {
            storeDB = aDBc;
            // Multiply menu price by count of that menu to get
            // the current price for each of those menus in the cart
            // sum all menu price totals to get the cart total
            decimal? total = (from cartItems in storeDB.Carts
                where cartItems.CartId == ShoppingCartId
                select (int?)cartItems.Count * cartItems.Menu.Price).Sum();
                return total ?? decimal.Zero;
        }
        public int CreateOrder(Order order, ApplicationDbContext aDbc)
        {
            storeDB = aDbc;
                decimal orderTotal = 0;
                var cartItems = GetCartItems(storeDB);
                // Iterate over the items in the cart, adding the order details for each
                foreach (var item in cartItems)
                {
                        var orderDetail = new OrderDetail
                        {
                            MenuItemID = item.MenuItemId,
                            OrderID = order.OrderID,
                            UnitPrice = storeDB.MenuItmes.Find( item.MenuItemId).Price,
                            Quantity = item.Count
                        };
                        // Set the order total of the shopping cart
                        orderTotal += (item.Count * item.Menu.Price);
                        storeDB.OrderItems.Add(orderDetail);
                }
                // Set the order's total to the orderTotal count
                order.TotalAmount = orderTotal;
                // Save the order
                storeDB.SaveChanges();
                // Empty the shopping cart
                EmptyCart();
                // Return the OrderId as the confirmation number
                return order.OrderID;
        }
        // We're using HttpContextBase to allow access to cookies.
        public string GetCartId(HttpContext context)
        {
            if (context.Session.Get(CartSessionKey) == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session.SetString(CartSessionKey, context.User.Identity.Name);
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();
                    // Send tempCartId back to client as a cookie
                    context.Session.SetString(CartSessionKey, tempCartId.ToString());
                }
            }
            return context.Session.Get(CartSessionKey).ToString();
        }
        // When a user has logged in, migrate their shopping cart to
        // be associated with their username
        public void MigrateCart(string userName, ApplicationDbContext aDBc)
        {
            storeDB = aDBc;
            var shoppingCart = storeDB.Carts.Where(c => c.CartId == ShoppingCartId);
            foreach (Cart item in shoppingCart)
            {
                item.CartId = userName;
            }
            storeDB.SaveChanges();
        }
    }
}