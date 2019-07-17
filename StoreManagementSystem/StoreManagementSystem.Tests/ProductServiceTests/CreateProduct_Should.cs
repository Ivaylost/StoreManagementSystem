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
    public class CreateProduct_Should
    {
        [TestMethod]
        public void Throw_Argument_Exception_If_Product_Exists()
        {
            using (var arrangeContext = new ShopContext(TestUtils.GetOptions(nameof(Throw_Argument_Exception_If_Product_Exists))))
            {
                var sut = new ProductService(arrangeContext);

                var product = new Product()
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

                };

                arrangeContext.Products.Add(product);
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new ShopContext(TestUtils.GetOptions(nameof(Throw_Argument_Exception_If_Product_Exists))))
            {
                var sut = new ProductService(assertContext);

                List<Supplier> suppliers = new List<Supplier>();

                suppliers.Add(new Supplier()
                {
                    SupplierName = "Gosho",
                    IdentificationNumber = 1010101010,
                    Address = "sofia",
                    RepresentedBy = "pesho",
                    PhoneNumber = "087282888"
                });

                var ex = Assert.ThrowsException<ArgumentException>(() => sut.CreateProduct("TestProduct","Clothes",10,20,30,suppliers));
                Assert.AreEqual($"Product TestProduct already exists",ex.Message);
            }
        }

        [TestMethod]
        public void Throw_Argument_Exception_If_Category_Doesnt_exists()
        {
            using (var assertContext = new ShopContext(TestUtils.GetOptions(nameof(Throw_Argument_Exception_If_Category_Doesnt_exists))))
            {
                var sut = new ProductService(assertContext);
                List<Supplier> suppliers = new List<Supplier>();

                suppliers.Add(new Supplier()
                {
                    SupplierName = "Gosho",
                    IdentificationNumber = 1010101010,
                    Address = "sofia",
                    RepresentedBy = "pesho",
                    PhoneNumber = "087282888"
                });


                var ex = Assert.ThrowsException<ArgumentException>(() => sut.CreateProduct("TestProduct", "Clothes", 10, 20, 30, suppliers));
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
                List<Supplier> suppliers = new List<Supplier>();


                var supplier = new Supplier
                {
                    SupplierName = "Gosho",
                    IdentificationNumber = 1010101010,
                    Address = "sofia",
                    RepresentedBy = "pesho",
                    PhoneNumber = "087282888"
                };

                suppliers.Add(supplier);

                var product =sut.CreateProduct("TestProduct", "Clothes", 10, 20, 30, suppliers);

                Assert.AreEqual("TestProduct", product.ProductName);
                Assert.AreEqual("Clothes", product.Category.CategoryName);
                Assert.AreEqual(10, product.AvailableQuantity);
                Assert.AreEqual(20, product.BuyPrice);
                Assert.AreEqual(30, product.SellPrice);
               
            }
        }
    }
}
