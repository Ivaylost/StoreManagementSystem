using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace StoreManagementSystem.Data.Models
{
    public class Product
    {
        public int Id { get; set; }

        [StringLength(20)]
        [Required]
        public string ProductName { get; set; }

        [Required]
        [Range(0.01, 100000)]
        public decimal BuyPrice { get; set; }

        [Required]
        [Range(0.01, 100000)]
        public decimal SellPrice { get; set; }

        [Required]
        [Range(1, 100000)]
        public int AvailableQuantity { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }


        public ICollection<OrderProduct> OrderProduct { get; set; }

        public ICollection<ProductSupplier> ProductSuppliers { get; set; }
        
    }
}
