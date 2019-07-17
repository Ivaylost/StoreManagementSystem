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
    public class GetProductsLessThen_Should
    {

        [TestMethod]
        public void Succeed_When_ThereAreProducts_WIthLessQuantity()
        {
            using (var arrangeContext = new ShopContext(TestUtils.GetOptions(nameof(Succeed_When_ThereAreProducts_WIthLessQuantity))))
            {
                var sut = new ProductService(arrangeContext);


                var product = new Product()
                {
                    ProductName = "TestProduct",
                    BuyPrice = (decimal)1.20,
                    SellPrice = (decimal)1.30,
                    AvailableQuantity = 10,
                    Category = new Category
                    {
                        CategoryName = "Clothes",
                        CreatedOn = DateTime.Now
                    }

                };

                arrangeContext.Products.Add(product);
                arrangeContext.SaveChanges();


            }
            using (var assertContext = new ShopContext(TestUtils.GetOptions(nameof(Succeed_When_ThereAreProducts_WIthLessQuantity))))
            {
                var sut = new ProductService(assertContext);
                int quantity = 20;

                var products = sut.GetProductsLessThen(quantity);

                Assert.IsTrue(products.Any());
            }
        }
    }
}
