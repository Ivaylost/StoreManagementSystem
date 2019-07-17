using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StoreManagementSystem.Data.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string CategoryName { get; set; }

        public DateTime CreatedOn { get; set; }

        public ICollection<Product> Products { get; set; }


    }
}
