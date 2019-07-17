using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoreManagementSystem.Data.Context;
using StoreManagementSystem.Data.Models;
using StoreManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreManagementSystem.Tests.CustomerServiceTest
{
    [TestClass]
    public class CreateCustomer_Should
    {
        [TestMethod]
        public void CustomerAlreadyExist()
        {
            using (var arrangeContext = new ShopContext(TestUtils.GetOptions(nameof(CreateCustomer_Should))))
            {
                arrangeContext.Customers.Add(new Customer()
                {
                    FirstName = "Ivan",
                    LastName = "Ivanov"
                });
                arrangeContext.SaveChanges();
            }

            using (var assertContext = new ShopContext(TestUtils.GetOptions(nameof(CreateCustomer_Should))))
            {
                var sut = new CustomerService(assertContext);
                var ex = Assert.ThrowsException<ArgumentException>(() => sut.CreateCustomer("Ivan", "Ivanov", "ii@abv.bg"));
                Assert.AreEqual("Customer with first name Ivan and last name Ivanov already exists", ex.Message);
            }
        }

        [TestMethod]
        public void ReturnCreatedCustomer()
        {
            using (var assertContext = new ShopContext(TestUtils.GetOptions(nameof(ReturnCreatedCustomer))))
            {
                var sut = new CustomerService(assertContext);
                var customer = sut.CreateCustomer("Ivan", "Ivanov", "ii@abv.bg");
                Assert.AreEqual("Ivan", customer.FirstName);
                Assert.AreEqual("Ivanov", customer.LastName);
                Assert.AreEqual("ii@abv.bg", customer.Email);
            }
        }

    }
}
