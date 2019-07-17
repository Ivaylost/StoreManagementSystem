using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoreManagementSystem.Data.Context;
using StoreManagementSystem.Data.Models;
using StoreManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManagementSystem.Tests.CategoryServiveTests
{
    [TestClass]
    public class CreateCategory_Should
    {
        [TestMethod]
        public void Throw_Argument_Exception_If_Category_Exists()
        {
            using (var arrangeContext = new ShopContext(TestUtils.GetOptions(nameof(Throw_Argument_Exception_If_Category_Exists))))
            {
                var sut = new CategoryService(arrangeContext);

                var category = new Category()
                {
                   CategoryName="Food",
                   CreatedOn=DateTime.Now

                };

                arrangeContext.Categories.Add(category);
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new ShopContext(TestUtils.GetOptions(nameof(Throw_Argument_Exception_If_Category_Exists))))
            {
                var sut = new CategoryService(assertContext);


                var ex = Assert.ThrowsException<ArgumentException>(() => sut.CreateCategory("Food"));
                Assert.AreEqual($"Category Food already exists", ex.Message);
            }
        }

        [TestMethod]
        public void Succeed_If_Category_DoesntExist()
        {
           
            using (var assertContext = new ShopContext(TestUtils.GetOptions(nameof(Succeed_If_Category_DoesntExist))))
            {
                var sut = new CategoryService(assertContext);


                var category = sut.CreateCategory("Food");

                Assert.AreEqual("Food", category.CategoryName);
               
            }
        }
    }
}
