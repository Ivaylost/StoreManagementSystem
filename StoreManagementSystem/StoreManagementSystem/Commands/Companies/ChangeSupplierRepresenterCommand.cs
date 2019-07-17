using StoreManagementSystem.Core.IO;
using StoreManagementSystem.Core.Providers;
using StoreManagementSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManagementSystem.Commands.Companies
{
    public class ChangeSupplierRepresenterCommand : ICommand
    {

        private readonly ISupplierService supplierService;
        private readonly IWriter writer;
        private readonly IReader reader;
        private readonly IPramaterValidator validator;

        public ChangeSupplierRepresenterCommand(ISupplierService supplierService, IWriter writer, IPramaterValidator validator, IReader reader)
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
            writer.WriteLine("Enter the new representer:");
            var newRepresenter = validator.Validate();
            parameters.Add(newRepresenter);
            return parameters;
        }

        public string Execute(IReadOnlyList<string> parameters)
        {
            var supplierName = parameters[0];
            var newRepresenter = parameters[1];

            var supplier = this.supplierService.GetSupplier(supplierName);

            var changedSupplier = this.supplierService.ChangeRepresenter(supplier, newRepresenter);


            return $"The company {changedSupplier.SupplierName}'s representer was changed successfully to {changedSupplier.RepresentedBy}";

        }
    }
}
