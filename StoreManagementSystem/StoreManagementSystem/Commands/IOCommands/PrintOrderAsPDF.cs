using StoreManagementSystem.Core.IO;
using StoreManagementSystem.Core.Providers;
using StoreManagementSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManagementSystem.Commands.IOCommands
{
    public class PrintOrderAsPDF : ICommand
    {
        private readonly IOrderService orderservice;
        private readonly IWriter writer;
        private readonly IReader reader;
        private readonly IPramaterValidator validator;
        private readonly IPrintService print;

        public PrintOrderAsPDF(IOrderService orderservice, IWriter writer,
            IPramaterValidator validator, IReader reader, IPrintService print)
        {
            this.orderservice = orderservice ?? throw new ArgumentNullException(nameof(orderservice));
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
            this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
            this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
            this.print = print ?? throw new ArgumentNullException(nameof(print));
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
            var printPDF = this.print.PrintInPDF(order);

            return $"Order with ID {printPDF.Id} was printed";
        }

    }
}
