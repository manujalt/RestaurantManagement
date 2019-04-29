using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagementSystems.PayrollSystem.Models
{
    //id, name, address, phone, gender, Position,  sailary, joiningdate, managerid,departmentid, sailaryID
    public class Employee
    {
        public enum Gender
        {
            Male, Female
        }

        [Key]
        public int EmployeeID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(150, MinimumLength = 3)]
        public string Address { get; set; }

        public long Phone { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Position { get; set; }

        //pre or post
        [DisplayFormat(NullDisplayText = "Male")]
        public Gender? EGender { get; set; }

        [DataType(DataType.Currency)]
        //[Column(TypeName = "total money")]
        public decimal Salary { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Joining date")]
        public DateTime JoiningDate { get; set; }

        public int ManagerID { get; set; }

        public int DepartmentID { get; set; }
        //public int DependentID { get; set; }

        public virtual Department Department { get; set; }
        //public virtual Order Order { get; set; }
        public List<Dependent> Dependents { get; set; }

        public List<Salaries> SailariesByMonthYear { get; set; }
    }
}
