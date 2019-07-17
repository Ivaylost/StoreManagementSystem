using StoreManagementSystem.Core.IO;
using StoreManagementSystem.Core.Providers;
using StoreManagementSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManagementSystem.Commands.Companies
{
    public class CreateCustomerCommand : ICommand
    {
        private readonly ICustomerService customerService;
        private readonly IWriter writer;
        private readonly IReader reader;
        private readonly IPramaterValidator validator;

        public CreateCustomerCommand(ICustomerService customerService, IWriter writer, IPramaterValidator validator, IReader reader)
        {
            this.customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
            this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
            this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
        }

        public IReadOnlyList<string> ProvideParameters()
        {
            var parameters = new List<string>();

            writer.WriteLine("Enter a first name:");
            var firstName = validator.Validate();
            parameters.Add(firstName);
            writer.WriteLine("Enter a second name:");
            var secondName = validator.Validate();
            parameters.Add(secondName);
            writer.WriteLine("Enter an e-mail");
            var eMail = reader.ReadLine();
            parameters.Add(eMail.ToString());
            return (parameters);
        }

        public string Execute(IReadOnlyList<string> parameters)
        {
            string firstName = parameters[0];
            string lastName = parameters[1];
            string eMail = parameters[2];


            var customer = this.customerService.CreateCustomer(firstName, lastName, eMail);

            return $"Created customer with name {customer.FirstName} {customer.LastName} with Id {customer.Id}";

        }
    }
}
