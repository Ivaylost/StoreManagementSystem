using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StoreManagementSystem.Data.Models
{
    public class Order
    {

        public int Id { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public ICollection<OrderProduct> OrderProduct { get; set; }

    }
}
