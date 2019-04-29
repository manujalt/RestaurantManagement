using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagementSystems.Models
{
    public class OrderDetail
    {
        //unique
        [Key]
        public int OrderItemID { get; set; }

        public int? OrderID { get; set; }

        public int? MenuItemID { get; set; }

        [DataType(DataType.Currency)]
        //[Column(TypeName = "Price")]
        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public virtual MenuItem Menus { get; set; }
        public virtual Order Order { get; set; }

    }


}
