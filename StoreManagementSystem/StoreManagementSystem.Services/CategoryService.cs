using StoreManagementSystem.Data.Context;
using StoreManagementSystem.Data.Models;
using StoreManagementSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreManagementSystem.Services
{
    public class CategoryService:ICategoryService
    {
        private readonly ShopContext context;

        public CategoryService(ShopContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Category CreateCategory(string name)
        {
            if (this.context.Categories.Any(c => c.CategoryName == name))
            {
                throw new ArgumentException($"Category {name} already exists");
            }

            var category = new Category() {CategoryName=name,CreatedOn=DateTime.Now};
           
            this.context.Categories.Add(category);
            this.context.SaveChanges();

            return category;
        }

        public string GetProfitFromCategory(string categoryName)
        {

            var category = this.context.Categories
                .FirstOrDefault(c => c.CategoryName == categoryName);

            if (category == null)
            {
                throw new ArgumentException($"Category {categoryName} does not exist");
            }


            var profit = this.context.Products
              .Where(p => p.Category == category)
              .Select(s => s.SellPrice - s.BuyPrice)
              .AsEnumerable()
              .Sum();

            return profit.ToString();
            
        }
        
      
    }
}
