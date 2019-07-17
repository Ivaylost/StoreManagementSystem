using StoreManagementSystem.Core.IO;
using StoreManagementSystem.Core.Providers;
using StoreManagementSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManagementSystem.Commands.Products
{
    public class OrderStornoCommand : ICommand
    {
        private readonly IOrderService orderservice;
        private readonly IWriter writer;
        private readonly IReader reader;
        private readonly IPramaterValidator validator;

        public OrderStornoCommand(IOrderService orderservice, IWriter writer,
            IPramaterValidator validator, IReader reader)
        {
            this.orderservice = orderservice ?? throw new ArgumentNullException(nameof(orderservice));
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
            this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
            this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
        }
        public IReadOnlyList<string> ProvideParameters()
        {
            var parameters = new List<string>();

            writer.WriteLine("Enter an order Id");
            var name = validator.Validate();
            parameters.Add(name);
            return (parameters);
        }


        public string Execute(IReadOnlyList<string> parameters)
        {
            int orderId = int.Parse(parameters[0]);

            var order = this.orderservice.FindById(orderId);
                this.orderservice.OrderStorno(order);

            return $"Order with {orderId} was deleted";

        }
    }
}
