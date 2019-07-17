using StoreManagementSystem.Core.IO;
using StoreManagementSystem.Core.Providers;
using StoreManagementSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreManagementSystem.Commands.Products
{
    public class ProductLessThenCommand : ICommand
    {
        private readonly IProductService productService;
        private readonly IWriter writer;
        private readonly IPramaterValidator validator;



        public ProductLessThenCommand(IProductService productService, IWriter writer, IPramaterValidator validator,
            ISupplierService supplierService)
        {
            this.productService = productService ?? throw new ArgumentNullException(nameof(productService));
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
            this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public IReadOnlyList<string> ProvideParameters()
        {
            var parameters = new List<string>();
            writer.WriteLine("Enter a product quantity:");
            var quantity = validator.Validate();
            parameters.Add(quantity);

            return (parameters);
        }

        public string Execute(IReadOnlyList<string> parameters)
        {
            var quantity = int.Parse(parameters[0]);
            var products = this.productService.GetProductsLessThen(quantity);

            var result = new StringBuilder();
            result.AppendLine($"Found {products.Count} products");

            result.Append(string.Join(Environment.NewLine, products.Select(p =>
             $"|*| Id: {p.Id} |*|" + Environment.NewLine +
             $" Name: {p.ProductName} |*|" + Environment.NewLine +
             $" Available Quantity: {p.AvailableQuantity} |*|" + Environment.NewLine +
             $" Buy Price: {p.BuyPrice}$ |*|" + Environment.NewLine +
             $" Sell Price: {p.SellPrice}$ |*|" + Environment.NewLine +
             $"====================================")));

            return result.ToString();
        }

    }
}
