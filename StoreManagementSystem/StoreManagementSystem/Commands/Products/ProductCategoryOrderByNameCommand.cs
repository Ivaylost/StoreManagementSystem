using StoreManagementSystem.Core.IO;
using StoreManagementSystem.Core.Providers;
using StoreManagementSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreManagementSystem.Commands.Products
{
    public class ProductCategoryOrderByNameCommand:ICommand
    {
        private readonly IProductService productService;
        private readonly ISupplierService supplierService;
        private readonly IWriter writer;
        private readonly IPramaterValidator validator;

        public ProductCategoryOrderByNameCommand(IProductService productService, IWriter writer, IPramaterValidator validator,
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

            return parameters;
        }

        public string Execute(IReadOnlyList<string> parameters)
        {
            var products = this.productService.GetProductsWithCategory();

            var orderProducts = this.productService.OrderByName(products);

            var result = new StringBuilder();
            result.AppendLine($"Found {products.Count} products");

            result.Append(string.Join(Environment.NewLine, orderProducts.Select(p =>
             $"|*| Id: {p.Id} |*| " + Environment.NewLine +
             $" Name: {p.ProductName} |*|" + Environment.NewLine +
             $" Available Quantity: {p.AvailableQuantity} |*|" + Environment.NewLine +
             $" Buy Price: {p.BuyPrice}$ |*|" + Environment.NewLine +
             $" Sell Price: {p.SellPrice}$ |*|" + Environment.NewLine +
             $" Category:{p.Category.CategoryName}" + Environment.NewLine +
             $" Created on:{p.Category.CreatedOn}" + Environment.NewLine +
             $"=====================================")));

            return result.ToString();
        }
    }
}
