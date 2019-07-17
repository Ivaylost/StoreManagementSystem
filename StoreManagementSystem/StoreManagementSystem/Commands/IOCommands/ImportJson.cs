using StoreManagementSystem.Core.IO;
using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using StoreManagementSystem.Data.Models;
using StoreManagementSystem.Services.Interfaces;

namespace StoreManagementSystem.Commands.IOCommands
{
    public class ImportJson : ICommand
    {
        private readonly IWriter writer;
        private readonly ICustomerService customerService;

        public ImportJson(IWriter writer, ICustomerService customerService)
        {
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
            this.customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
        }

        public IReadOnlyList<string> ProvideParameters()
        {
            var parameters = new List<string>();
            var jsonString = File.ReadAllText(@"C:\C#\StoreManagementSystem\store-management-system\StoreManagementSystem\StoreManagementSystem\Jason.Import\JsonImportFile.json");
            parameters.Add(jsonString);
            return parameters;
        }

        public string Execute(IReadOnlyList<string> parameters)
        {
            List<Customer> customers = JsonConvert.DeserializeObject<List<Customer>>(parameters[0]);
            foreach (var customer in customers)
            {
                var createdCustomer = this.customerService.CreateCustomerJson(customer);
                if (createdCustomer == "")
                {
                    writer.WriteLine($"Customer with name {customer.FirstName} {customer.LastName} was added");
                }
                else
                {
                    writer.WriteLine(createdCustomer);
                }
            }
            return "Successful import";
        }

    }
}
