using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MyPassionProject.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierId { get; set; }

        public string SupplierName { get; set;}
        public string SupplierAddress { get; set;}
        public string SupplierEmail { get; set;}
    }
}