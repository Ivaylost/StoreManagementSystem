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
    public class GetProductsWithCategory_Should
    {
        [TestMethod]
        public void Should_Inlcude()
        {
            using (var arrangeContext = new ShopContext(TestUtils.GetOptions(nameof(Should_Inlcude))))
            {
                var sut = new ProductService(arrangeContext);

                var product = new Product()
                {
                    ProductName = "SomeProduct",
                    BuyPrice = (decimal)1.20,
                    SellPrice = (decimal)1.30,
                    AvailableQuantity = 100,
                    Category = new Category
                    {
                        CategoryName = "Clothes",
                        CreatedOn = DateTime.Now
                    }


                };

                arrangeContext.Products.Add(product);
                arrangeContext.SaveChanges();


            }
            using (var assertContext = new ShopContext(TestUtils.GetOptions(nameof(Should_Inlcude))))
            {
                var sut = new ProductService(assertContext);


                var products = sut.GetProductsWithCategory();

                Assert.IsTrue(products.Any());
                Assert.IsTrue(products.All(p => p.Category != null));
            }
        }
    }
}
