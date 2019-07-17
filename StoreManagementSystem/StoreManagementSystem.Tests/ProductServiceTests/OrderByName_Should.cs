using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoreManagementSystem.Data.Context;
using StoreManagementSystem.Data.Models;
using StoreManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreManagementSystem.Tests.ProductServiceTests
{
    [TestClass]
    public class OrderByName_Should
    {
        [TestMethod]
        public void Should_Order()
        {
            using (var assertContext = new ShopContext(TestUtils.GetOptions(nameof(Should_Order))))
            {
                var sut = new ProductService(assertContext);
                List<Product> products = new List<Product>();
                
                products.Add(new Product()
                {
                    ProductName = "TestProduct",
                    BuyPrice = (decimal)1.20,
                    SellPrice = (decimal)1.30,
                    AvailableQuantity = 100,
                    Category = new Category
                    {
                        CategoryName = "Clothes",
                        CreatedOn = DateTime.Now
                    }
                });

                var orderProducts = sut.OrderByName(products);

                Assert.IsTrue(orderProducts.Any());
            }
        }
    }
}
