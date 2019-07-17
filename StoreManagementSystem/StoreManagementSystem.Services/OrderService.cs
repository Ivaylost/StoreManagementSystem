using StoreManagementSystem.Data.Context;
using StoreManagementSystem.Data.Models;
using StoreManagementSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreManagementSystem.Services
{
    public class OrderService : IOrderService
    {
        private readonly ShopContext context;

        public OrderService(ShopContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Order CreateOrder(string customerName, IReadOnlyCollection<Product> products)
        {
         
            var customer = this.context.Customers
                .FirstOrDefault(c => c.FirstName == customerName);

            if (customer == null)
            {
                throw new ArgumentException($"Customer with name {customerName} does not exist");
            }

            var order = new Order()
            {
                Customer = customer,
                OrderDate = DateTime.Now,
                
            };

            order.OrderProduct = products
                .Select(product => new OrderProduct() { Product = product })
                .ToList();

            this.context.Orders.Add(order);
            this.context.SaveChanges();

            return order;
        }

        public Order FindById (int orderId)
        {
            var order = this.context.Orders
                .FirstOrDefault(c => c.Id == orderId);

            if (order == null)
            {
                throw new ArgumentException($"Order with {orderId} does not exist");
            }
            return order;
        }

        public Order OrderStorno(Order order)
        {
            this.context.Orders.Remove(order);
            this.context.SaveChanges();
            return order;
        }
    }
}
