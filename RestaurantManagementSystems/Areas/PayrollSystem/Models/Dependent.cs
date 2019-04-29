using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagementSystems.PayrollSystem.Models
{
    //dependentid, employeeid, name, relationship, phoneNo
    public class Dependent
    {
        [Key]
        public int DependentID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Relationship { get; set; }

        public long PhoneNo { get; set; }

        public int EmployeeID { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
