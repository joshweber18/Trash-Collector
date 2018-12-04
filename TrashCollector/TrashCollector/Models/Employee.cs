using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }
        public int ZipCode { get; set; }

        [ForeignKey("ApplicationUser")]
        [Display(Name = "User Type")]
        public string AppUserID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}