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
    public class CreateCustomerJson_Should
    {

        [TestMethod]
        public void ReturnCreatedCustomerJson()
        {
            using (var assertContext = new ShopContext(TestUtils.GetOptions(nameof(ReturnCreatedCustomerJson))))
            {
                var sut = new CustomerService(assertContext);
                var customer = sut.CreateCustomerJson(new Customer()
                {
                    FirstName = "Ivan",
                    LastName = "Ivanov"
                });

                Assert.IsTrue(assertContext.Customers.Any(p => p.FirstName == "Ivan"));
                Assert.IsTrue(assertContext.Customers.Any(p => p.LastName == "Ivanov"));

            }
        }

        [TestMethod]
        public void CustomerAlreadyExistJson()
        {
            using (var arrangeContext = new ShopContext(TestUtils.GetOptions(nameof(CustomerAlreadyExistJson))))
            {
                arrangeContext.Customers.Add(new Customer()
                {
                    FirstName = "Ivan",
                    LastName = "Ivanov"
                });
                arrangeContext.SaveChanges();
            }

            using (var assertContext = new ShopContext(TestUtils.GetOptions(nameof(CustomerAlreadyExistJson))))
            {
                var sut = new CustomerService(assertContext);
                var message = sut.CreateCustomerJson(new Customer()
                {
                    FirstName = "Ivan",
                    LastName = "Ivanov"
                });
                Assert.AreEqual("Customer with first name Ivan and last name Ivanov already exists", message);
            }
        }
    }
}
