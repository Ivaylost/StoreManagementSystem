using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoreManagementSystem.Data.Context;
using StoreManagementSystem.Data.Models;
using StoreManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManagementSystem.Tests.ProductServiceTests
{
    [TestClass]
    public class FindByName_Should
    {
        [TestMethod]
        public void Succeed_When_ProductExists()
        {
            using (var arrangeContext = new ShopContext(TestUtils.GetOptions(nameof(Succeed_When_ProductExists))))
            {
                var sut = new ProductService(arrangeContext);


                var product = new Product
                {

                    ProductName = "TestProduct",
                    BuyPrice = (decimal)1.20,
                    SellPrice = (decimal)1.30,
                    AvailableQuantity = 100

                };

                arrangeContext.Products.Add(product);
                arrangeContext.SaveChanges();

            }
            using (var assertContext = new ShopContext(TestUtils.GetOptions(nameof(Succeed_When_ProductExists))))
            {
                var sut = new ProductService(assertContext);


                var productByName = sut.FindByName("TestProduct");

                Assert.AreEqual("TestProduct", productByName.ProductName);
            }
        }
    }
}
