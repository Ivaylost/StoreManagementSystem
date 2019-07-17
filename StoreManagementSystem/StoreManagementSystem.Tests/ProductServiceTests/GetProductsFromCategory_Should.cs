using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoreManagementSystem.Data.Context;
using StoreManagementSystem.Data.Models;
using StoreManagementSystem.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreManagementSystem.Tests.ProductServiceTests
{
    [TestClass]
    public class GetProductsFromCategory_Should
    {
        [TestMethod]
        public void Throw_Argument_Exception_If_CategoryDoesntExists()
        {
            using (var assertContext = new ShopContext(TestUtils.GetOptions(nameof(Throw_Argument_Exception_If_CategoryDoesntExists))))
            {
                var sut = new ProductService(assertContext);

                var ex = Assert.ThrowsException<ArgumentException>(() => sut.GetProductsFromCategory("Clothes"));
                Assert.AreEqual($"Category Clothes does not exist", ex.Message);
            }
        }

        [TestMethod]
        public void Succeed_When_CategoryExists()
        {
            using (var arrangeContext = new ShopContext(TestUtils.GetOptions(nameof(Succeed_When_CategoryExists))))
            {
                var sut = new ProductService(arrangeContext);


                var category = new Category
                {
                    CategoryName = "Clothes",
                    CreatedOn = DateTime.Now
                };

                arrangeContext.Categories.Add(category);
                arrangeContext.SaveChanges();


            }
            using (var assertContext = new ShopContext(TestUtils.GetOptions(nameof(Succeed_When_CategoryExists))))
            {
                var sut = new ProductService(assertContext);

                var products = sut.GetProductsFromCategory("Clothes");

                Assert.IsTrue(products.Any());
            }
        }
    }
}
