using StoreManagementSystem.Core.IO;
using StoreManagementSystem.Core.Providers;
using StoreManagementSystem.Data.Models;
using StoreManagementSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreManagementSystem.Commands.Products
{
    public class CreateProductCommand : ICommand
    {
        private readonly IProductService productService;
        private readonly ISupplierService supplierService;
        private readonly IWriter writer;
        private readonly IPramaterValidator validator;



        public CreateProductCommand(IProductService productService, IWriter writer, IPramaterValidator validator, 
            ISupplierService supplierService)
        {
            this.productService = productService ?? throw new ArgumentNullException(nameof(productService));
            this.supplierService = supplierService;
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
            this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public IReadOnlyList<string> ProvideParameters()
        {
            var parameters = new List<string>();
            writer.WriteLine("Enter product name:");
            var productName = validator.Validate();
            parameters.Add(productName);
            writer.WriteLine("Enter category name:");
            var categoryName = validator.Validate();
            parameters.Add(categoryName);
            writer.WriteLine("Enter product quantity:");
            var productQuantity = validator.Validate();
            parameters.Add(productQuantity);
            writer.WriteLine("Enter buy price:");
            var buyPrice = validator.Validate();
            parameters.Add(buyPrice);
            writer.WriteLine("Enter sell price:");
            var sellPrice = validator.Validate();
            parameters.Add(sellPrice);
            writer.WriteLine("Enter suppliers separated by comma:");
            var suppliers = validator.Validate();
            parameters.Add(suppliers);
            return (parameters);
        }

        public string Execute(IReadOnlyList<string> parameters)
        {
            string productName = parameters[0];
            string categoryName = parameters[1];
            int availabeQuantity = int.Parse(parameters[2]);
            decimal buyPrice = decimal.Parse(parameters[3]);
            decimal sellPrice = decimal.Parse(parameters[4]);
            List<Supplier> suppliers = parameters[5]
                .Split(',').ToList()
                .Select(this.supplierService.FindByName)
                .ToList();

            if (suppliers.Any(s => s == null))
            {
                return "Cannot find one of specified suppliers";
            }


            var product = this.productService.CreateProduct(productName,categoryName,availabeQuantity,buyPrice,sellPrice, suppliers);

            return $"Created product {product.ProductName} with Id {product.Id}";

        }

    }
}
