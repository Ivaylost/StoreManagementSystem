using StoreManagementSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManagementSystem.Services.Interfaces
{
    public interface IProductService
    {
       Product CreateProduct(string producName, string categoryName, int availableQuantity, decimal buyPrice, decimal sellPrice, IReadOnlyCollection<Supplier> supliers);

       IReadOnlyCollection<Product> GetProductsFromCategory(string categoryName);

       Product FindByName(string name);

       IReadOnlyCollection<Product> GetMostExpensiveProducts(string categoryName);

        IReadOnlyCollection<Product> GetProductsWithCategory();

        IReadOnlyCollection<Product> GetProductsLessThen(int quantity);

        IReadOnlyCollection<Product> OrderByName(IReadOnlyCollection<Product> products);

    }
}
