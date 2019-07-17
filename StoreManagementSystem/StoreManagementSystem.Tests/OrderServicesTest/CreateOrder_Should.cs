using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoreManagementSystem.Data.Context;
using StoreManagementSystem.Data.Models;
using StoreManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreManagementSystem.Tests.OrderServicesTest
{
    [TestClass]
    public class CreateOrder_Should
    {
        [TestMethod]
        public void CustomerDoesNotExist()
        {
            using (var assertContext = new ShopContext(TestUtils.GetOptions(nameof(CustomerDoesNotExist))))
            {
                var sut = new OrderService(assertContext);

                List<Product> products = new List<Product>();

                var ex = Assert.ThrowsException<ArgumentException>(() => sut.CreateOrder("Pesho", products));
                Assert.AreEqual("Customer with name Pesho does not exist", ex.Message);
            }
        }

        [TestMethod]
        public void CreateOrderContainsCustomerName()
        {
            using (var arrangeContext = new ShopContext(TestUtils.GetOptions(nameof(CreateOrderContainsCustomerName))))
            {
                arrangeContext.Customers.Add(new Customer()
                {
                    FirstName = "Ivan",
                    LastName = "Ivanov"
                });
                arrangeContext.SaveChanges();
            }

            using (var assertContext = new ShopContext(TestUtils.GetOptions(nameof(CreateOrderContainsCustomerName))))
            {
                var sut = new OrderService(assertContext);
                List<Product> products = new List<Product>();
                var order = sut.CreateOrder("Ivan", products);
                Assert.AreEqual("Ivan", order.Customer.FirstName);
            }
        }

        [TestMethod]
        public void CreateOrderContainsProductsList()
        {
            using (var arrangeContext = new ShopContext(TestUtils.GetOptions(nameof(CreateOrderContainsProductsList))))
            {
                arrangeContext.Customers.Add(new Customer()
                {
                    FirstName = "Ivan",
                    LastName = "Ivanov"
                });
                arrangeContext.SaveChanges();
            }

            using (var assertContext = new ShopContext(TestUtils.GetOptions(nameof(CreateOrderContainsProductsList))))
            {
                var sut = new OrderService(assertContext);

                List<Product> products = new List<Product>() {
                    new Product()
                        {
                            ProductName = "TestProductFirst",
                            BuyPrice = (decimal)1.20,
                            SellPrice = (decimal)1.30,
                            AvailableQuantity = 100
                        },
                    new Product()
                        {
                            ProductName = "TestProductSecond",
                            BuyPrice = (decimal)2.20,
                            SellPrice = (decimal)3.30,
                            AvailableQuantity = 1000
                        }
                };
                var order = sut.CreateOrder("Ivan", products);

                order.OrderProduct = products
                .Select(product => new OrderProduct() { Product = product })
                .ToList();

                Assert.AreEqual(order.OrderProduct.Count(), 2);
                Assert.IsTrue(order.OrderProduct.Any(p => p.Product.ProductName == "TestProductFirst"));
                Assert.IsTrue(order.OrderProduct.Any(p => p.Product.ProductName == "TestProductSecond"));
            }
        }

    }
}
