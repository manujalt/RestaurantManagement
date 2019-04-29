using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagementSystems.PayrollSystem.Models
{
    //sailaryID, employeeid, monthYear, sailaryamount, otherGifts
    public class Salaries
    {
        public enum Gender
        {
            Male, Female
        }

        [Key]
        public int SailaryID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Sailary Month-Year")]
        public DateTime SailaryMonth { get; set; }

        [DataType(DataType.Currency)]
        //[Column(TypeName = "total money")]
        public decimal SailaryAmount { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string OtherGifts { get; set; }

        public int EmployeeID { get; set; }

        public virtual Employee Employee { get; set; }
        //public virtual Order Order { get; set; }

        //public List<Sailary> SailariesByMonthYear { get; set; }
    }
}
