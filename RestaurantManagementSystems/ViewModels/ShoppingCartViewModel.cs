using System.Collections.Generic;
using RestaurantManagementSystems.Models;

namespace RestaurantManagementSystems.ViewModels
{
public class ShoppingCartViewModel
{
public List<Cart> CartItems { get; set; }
public decimal CartTotal { get; set; }
public List<RestaurantManagementSystems.Models.MenuItem> menuItems { get; set; }
    }
}