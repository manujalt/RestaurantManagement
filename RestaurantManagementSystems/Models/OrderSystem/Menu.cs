using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagementSystems.Models
{
    public class MenuItem
    {
       
        [Key]
        public int MenuID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        //[Column(TypeName = "Price")]
        public decimal Price { get; set; }

        [DisplayFormat(NullDisplayText = "No Ingridents avalible")]
        [StringLength(500, MinimumLength = 3)]
        public string Ingridents { get; set; }

        [DisplayFormat(NullDisplayText = "No recipe avalible")]
        [StringLength(500, MinimumLength = 3)]
        public string recipe { get; set; }

        [DisplayFormat(NullDisplayText = "No image avalible")]
        [StringLength(500, MinimumLength = 3)]
        public string ImageUrl { get; set; }

        //cash or card
        public enum InTodaysMenu
        {
            Yes, No
        }

        [DisplayFormat(NullDisplayText = "No")]
        public InTodaysMenu? TodaysMenu { get; set; }
    }

}
