using StoreManagementSystem.Core.IO;
using StoreManagementSystem.Core.Providers;
using StoreManagementSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreManagementSystem.Commands.Products
{
    public class ShowProductsInCategoryCommand : ICommand
    {
        private readonly IProductService productService;
        private readonly IWriter writer;
        private readonly IPramaterValidator validator;



        public ShowProductsInCategoryCommand(IProductService productService, IWriter writer, IPramaterValidator validator)
        {
            this.productService = productService ?? throw new ArgumentNullException(nameof(productService));
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
            this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public IReadOnlyList<string> ProvideParameters()
        {
            var parameters = new List<string>();
            writer.WriteLine("Enter category name:");
            var categoryName = validator.Validate();
            parameters.Add(categoryName);
            return parameters;
        }

        public string Execute(IReadOnlyList<string> parameters)
        {
            var categoryName = parameters[0];

            var products = this.productService.GetProductsFromCategory(categoryName);

            var result = new StringBuilder();
            result.AppendLine($"Found {products.Count} products");

            result.Append(string.Join(Environment.NewLine,products.Select(p =>
            $"|*| Id: {p.Id} |*|" + Environment.NewLine +
            $" Name: {p.ProductName} |*|" + Environment.NewLine +
            $" Available Quantity: {p.AvailableQuantity} |*|" + Environment.NewLine +
            $" Buy Price: {p.BuyPrice}$ |*|" + Environment.NewLine +
            $" Sell Price: {p.SellPrice}$ |*|" + Environment.NewLine +
            $" Category:{categoryName}" + Environment.NewLine +
            $"====================================")));

            return result.ToString();
            
        }

    }
}
