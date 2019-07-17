using Microsoft.EntityFrameworkCore;
using StoreManagementSystem.Data.Context;
using StoreManagementSystem.Data.Models;
using StoreManagementSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreManagementSystem.Services
{
    public class ProductService : IProductService
    {
        private readonly ShopContext context;

        public ProductService(ShopContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Product CreateProduct(string producName,string categoryName,int availableQuantity,decimal buyPrice,
            decimal sellPrice,IReadOnlyCollection<Supplier> suppliers)
        {
            if (this.context.Products.Any(p => p.ProductName == producName))
            {
                throw new ArgumentException($"Product {producName} already exists");
            }

            var category = this.context.Categories
                .FirstOrDefault(c => c.CategoryName == categoryName);

            if (category==null)
            {
                throw new ArgumentException($"Category {categoryName} does not exist");
            }

            var product = new Product() { ProductName = producName,
                Category = category,
                AvailableQuantity = availableQuantity,
                BuyPrice = buyPrice,
                SellPrice = sellPrice
            };

            product.ProductSuppliers = suppliers
                .Select(supplier => new ProductSupplier() { Supplier = supplier })
                .ToList();

            this.context.Products.Add(product);
            this.context.SaveChanges();

            return product;
        }

        public IReadOnlyCollection<Product> GetProductsFromCategory(string categoryName)
        {

            var category = this.context.Categories
                .FirstOrDefault(c => c.CategoryName == categoryName);

            if (category == null)
            {
                throw new ArgumentException($"Category {categoryName} does not exist");
            }

            return this.context.Products
                .Where(p => p.Category == category)
                .Take(this.context.Products.Count())
                .ToList();
        }

        public Product FindByName(string name)
        {
            return this.context.Products
                .FirstOrDefault(p => p.ProductName == name);
        }

        public IReadOnlyCollection<Product> GetMostExpensiveProducts(string categoryName)
        {

            var category = this.context.Categories
                .FirstOrDefault(c => c.CategoryName == categoryName);

            if (category == null)
            {
                throw new ArgumentException($"Category {categoryName} does not exist");
            }

            return this.context.Products
                .Where(p => p.Category == category)
                .OrderByDescending(s=>s.SellPrice)
                .Take(5)
                .ToList();
        }

        public IReadOnlyCollection<Product> GetProductsWithCategory()
        {
            return this.context.Products
                .Include(p => p.Category)
                .ToList();
        }

        public IReadOnlyCollection<Product> GetProductsLessThen(int quantity)
        {
            return this.context.Products
                .Where(q => q.AvailableQuantity <= quantity)
                .Take(this.context.Products.Count())
                //.Select(q => q.AvailableQuantity)
                .ToList();
        }

        public IReadOnlyCollection<Product> OrderByName(IReadOnlyCollection<Product> products)
        {
            return products
                .OrderBy(p => p.ProductName)
                .ToList();
        }
    }
}
