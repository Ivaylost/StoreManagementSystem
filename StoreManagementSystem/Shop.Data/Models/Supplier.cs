using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StoreManagementSystem.Data.Models
{
    public class Supplier
    {
        public int Id { get; set; }

        [StringLength(20)]
        [Required]
        public string SupplierName { get; set; }

        [MaxLength(10)]
        [Required]
        public int IdentificationNumber { get; set; }

        [StringLength(20)]
        [Required]
        public string RepresentedBy { get; set; }

        [StringLength(20)]
        [Required]
        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public ICollection<ProductSupplier> ProductSuppliers { get; set; }

    }
}
