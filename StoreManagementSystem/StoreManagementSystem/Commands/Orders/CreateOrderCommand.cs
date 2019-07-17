using StoreManagementSystem.Core.IO;
using StoreManagementSystem.Core.Providers;
using StoreManagementSystem.Data.Models;
using StoreManagementSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreManagementSystem.Commands.Orders
{
    public class CreateOrderCommand : ICommand
    {

        private readonly IOrderService orderservice;
        private readonly IProductService productservice;
        private readonly IWriter writer;
        private readonly IReader reader;
        private readonly IPramaterValidator validator;

        public CreateOrderCommand(IOrderService orderservice, IProductService productservice, IWriter writer, 
            IPramaterValidator validator, IReader reader)
        {
            this.orderservice = orderservice ?? throw new ArgumentNullException(nameof(orderservice));
            this.productservice = productservice ?? throw new ArgumentNullException(nameof(productservice));
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
            this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
            this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
        }
        public IReadOnlyList<string> ProvideParameters()
        {
            var parameters = new List<string>();

            writer.WriteLine("Enter customer's name");
            var name = validator.Validate();
            parameters.Add(name);
            writer.WriteLine("Enter product names separated by comma");
            var productNames = validator.Validate();
            parameters.Add(productNames);
            return (parameters);
        }


        public string Execute(IReadOnlyList<string> parameters)
        {
            string customerName = parameters[0];
            List<Product> products= parameters[1]
                .Split(',').ToList()
                .Select(this.productservice.FindByName)
                .ToList();

            if (products.Any(p => p == null))
            {
                return "Cannot find one of specified products";
            }

            var order = this.orderservice.CreateOrder(customerName, products);

            return $"{customerName} created order with id: {order.Id}";

        }

    }
}
