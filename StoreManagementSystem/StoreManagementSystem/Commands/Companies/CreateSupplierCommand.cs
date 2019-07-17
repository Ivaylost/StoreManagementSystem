using StoreManagementSystem.Core.IO;
using StoreManagementSystem.Core.Providers;
using StoreManagementSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManagementSystem.Commands.Companies
{
    public class CreateSupplierCommand : ICommand
    {
        private readonly ISupplierService supplierService;
        private readonly IWriter writer;
        private readonly IReader reader;
        private readonly IPramaterValidator validator;

        public CreateSupplierCommand(ISupplierService supplierService, IWriter writer, IPramaterValidator validator, IReader reader)
        {
            this.supplierService = supplierService ?? throw new ArgumentNullException(nameof(supplierService));
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
            this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
            this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
        }

        public IReadOnlyList<string> ProvideParameters()
        {
            var parameters = new List<string>();

            writer.WriteLine("Enter a supplier name:");
            var supplierName = validator.Validate();
            parameters.Add(supplierName);
            writer.WriteLine("Enter a Identification Number /exactly 10 digits/:");
            var identificationNumber = validator.Validate();
            parameters.Add(identificationNumber);
            writer.WriteLine("Enter a company representative");
            var representedBy = validator.Validate();
            parameters.Add(representedBy);
            writer.WriteLine("Enter a company address");
            var companyAddress = validator.Validate();
            parameters.Add(companyAddress);
            writer.WriteLine("Enter a phone number");
            var phoneNumber = reader.ReadLine();
            parameters.Add(phoneNumber.ToString());
            return (parameters);
        }

        public string Execute(IReadOnlyList<string> parameters)
        {
            string supplierName = parameters[0];
            string identificationNumber = parameters[1];
            string representedBy = parameters[2];
            string companyAddress = parameters[3];
            string phoneNumber = parameters[4];


            var supplier = this.supplierService.CreateSupplier(supplierName, int.Parse(identificationNumber), representedBy, companyAddress, phoneNumber);

            return $"Created supplier with name {supplier.SupplierName} with Id {supplier.Id}";

        }
    }
}
