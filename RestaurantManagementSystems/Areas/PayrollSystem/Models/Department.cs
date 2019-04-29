using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagementSystems.PayrollSystem.Models
{
    //name = kitchen, store, counter, waiter
    //Departmentid DepartmentName location
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string DepartmentName { get; set; }

        [StringLength(150, MinimumLength = 3)]
        public string Location { get; set; }

    }
}
