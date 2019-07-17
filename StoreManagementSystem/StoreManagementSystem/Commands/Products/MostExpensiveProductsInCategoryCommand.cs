using StoreManagementSystem.Core.IO;
using StoreManagementSystem.Core.Providers;
using StoreManagementSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreManagementSystem.Commands.Products
{
    public class MostExpensiveProductsInCategoryCommand : ICommand
    {
        private readonly IProductService productService;
        private readonly IWriter writer;
        private readonly IPramaterValidator validator;



        public MostExpensiveProductsInCategoryCommand(IProductService productService, IWriter writer, IPramaterValidator validator)
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

            var products = this.productService.GetMostExpensiveProducts(categoryName);

            var result = new StringBuilder();

            result.Append(string.Join(Environment.NewLine, products.Select(p =>
             $"|*| Id: {p.Id} |*|" +
             $" Name: {p.ProductName} |*|" +
             $" Available Quantity: {p.AvailableQuantity} |*|" +
             $" Buy Price: {p.BuyPrice}$ |*|" +
             $" Sell Price: {p.SellPrice}$ |*|" +
             $" Category:{categoryName}")));

            return result.ToString();
        }

    }
}
