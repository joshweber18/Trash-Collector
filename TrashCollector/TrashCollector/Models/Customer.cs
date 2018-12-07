using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }
        public string Address { get; set; }
        public int ZipCode { get; set; }
        public double BalanceDue { get; set; }
        public bool PickupStatus { get; set; }

        [ForeignKey("ApplicationUser")]
        [Display(Name = "User Type")]
        public string AppUserID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}