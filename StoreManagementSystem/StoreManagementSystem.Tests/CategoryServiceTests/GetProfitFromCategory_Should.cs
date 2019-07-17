using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoreManagementSystem.Data.Context;
using StoreManagementSystem.Data.Models;
using StoreManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManagementSystem.Tests.CategoryServiceTests
{
    [TestClass]
    public class GetProfitFromCategory_Should
    {
        [TestMethod]
        public void Throw_Argument_Exception_If_Category_DoesntExists()
        {
          
            using (var assertContext = new ShopContext(TestUtils.GetOptions(nameof(Throw_Argument_Exception_If_Category_DoesntExists))))
            {
                var sut = new CategoryService(assertContext);


                var ex = Assert.ThrowsException<ArgumentException>(() => sut.GetProfitFromCategory("Food"));
                Assert.AreEqual($"Category Food does not exist", ex.Message);
            }
        }

        [TestMethod]
        public void Succeed_If_Category_Exists()
        {
            using (var arrangeContext = new ShopContext(TestUtils.GetOptions(nameof(Succeed_If_Category_Exists))))
            {
                var sut = new CategoryService(arrangeContext);

                var category = new Category()
                {
                    CategoryName = "Food",
                    CreatedOn = DateTime.Now

                };

                var productOne = new Product()
                {
                    ProductName = "TestProductOne",
                    BuyPrice = (decimal)1.20,
                    SellPrice = (decimal)2.20,
                    AvailableQuantity = 100,
                    Category = category

                };

                var productTwo = new Product()
                {
                    ProductName = "TestProductTwo",
                    BuyPrice = (decimal)1.20,
                    SellPrice = (decimal)2.30,
                    AvailableQuantity = 100,
                    Category = category

                };

                arrangeContext.Categories.Add(category);
                arrangeContext.Products.Add(productOne);
                arrangeContext.Products.Add(productTwo);
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new ShopContext(TestUtils.GetOptions(nameof(Succeed_If_Category_Exists))))
            {
                var sut = new CategoryService(assertContext);


                var profit= sut.GetProfitFromCategory("Food");

                Assert.AreEqual("2.1", profit);
            }
        }
    }
}
