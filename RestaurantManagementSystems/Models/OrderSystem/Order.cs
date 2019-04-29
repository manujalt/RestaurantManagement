using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagementSystems.Models
{
    public class Order
    {
        public enum PaymentTypes
        {
            Pre, Post
        }

        public enum PaymentModes
        {
            Cash, Card
        }

        public enum OrderStatus
        {
            InProcess, Deliverd
        }
        
        [Key]
        public int OrderID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string PersonName { get; set; }

        [DataType(DataType.Currency)]
        //[Column(TypeName = "total money")]
        public decimal TotalAmount { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "order date")]
        public DateTime OrderDate { get; set; }

        //pre or post
        [DisplayFormat(NullDisplayText = "Pre")]
        public PaymentTypes? PaymentType { get; set; }
        
        //cash or card
        [DisplayFormat(NullDisplayText = "Cash")]
        public PaymentModes? PaymentMode { get; set; }


        //InProcess or Deliverd
        [DisplayFormat(NullDisplayText = "InProcess")]
        public OrderStatus Orderstatus { get; set; }

        public List<OrderDetail> OrderItem { get; set; }

    }

    

}
