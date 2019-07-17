using StoreManagementSystem.Core.IO;
using StoreManagementSystem.Core.Providers;
using StoreManagementSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManagementSystem.Commands.Companies
{
    public class ChangeSupplierAddressCommand : ICommand
    {
        private readonly ISupplierService supplierService;
        private readonly IWriter writer;
        private readonly IReader reader;
        private readonly IPramaterValidator validator;

        public ChangeSupplierAddressCommand(ISupplierService supplierService, IWriter writer, IPramaterValidator validator, IReader reader)
        {
            this.supplierService = supplierService ?? throw new ArgumentNullException(nameof(supplierService));
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
            this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
            this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
        }

        public IReadOnlyList<string> ProvideParameters()
        {
            var parameters = new List<string>();
            writer.WriteLine("Enter a Supplier name:");
            var supplierName = validator.Validate();
            parameters.Add(supplierName);

            writer.WriteLine("Enter the new Addres:");
            var newAddress = validator.Validate();
            parameters.Add(newAddress);
            return parameters;
        }

        public string Execute(IReadOnlyList<string> parameters)
        {
            var supplierName = parameters[0];
            var newAddress = parameters[1];

            var supplier = this.supplierService.GetSupplier(supplierName);

            var changedSupplier = this.supplierService.ChangeAddress(supplier, newAddress);


            return $"The company {changedSupplier.SupplierName} addres was changed successfully to {changedSupplier.Address}";

        }
    }
}
