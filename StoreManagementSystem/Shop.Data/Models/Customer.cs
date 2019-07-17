using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StoreManagementSystem.Data.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [StringLength(20)]
        [Required]
        public string FirstName { get; set; }

        [StringLength(20)]
        [Required]
        public string LastName { get; set; }

        public string Email { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
