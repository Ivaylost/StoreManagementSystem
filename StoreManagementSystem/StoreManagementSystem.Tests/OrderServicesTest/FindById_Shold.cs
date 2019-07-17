using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoreManagementSystem.Data.Context;
using StoreManagementSystem.Data.Models;
using StoreManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManagementSystem.Tests.OrderServicesTest
{
    [TestClass]
    public class FindById_Shold
    {

        [TestMethod]
        public void FindByIdReturnOrder()
        {
            using (var arrangeContext = new ShopContext(TestUtils.GetOptions(nameof(FindByIdReturnOrder))))
            {
                arrangeContext.Orders.Add(new Order()
                {
                    OrderDate = DateTime.Parse("12/12/2018 12:00:00 AM")
                });

                arrangeContext.SaveChanges();
            }

            using (var assertContext = new ShopContext(TestUtils.GetOptions(nameof(FindByIdReturnOrder))))
            {
                var sut = new OrderService(assertContext);
                var order = sut.FindById(3);
                Assert.AreEqual(3, order.Id);

            }
        }

        [TestMethod]
        public void FindByIdReturnExeption()
        {

            using (var assertContext = new ShopContext(TestUtils.GetOptions(nameof(FindByIdReturnExeption))))
            {
                var sut = new OrderService(assertContext);

                var ex = Assert.ThrowsException<ArgumentException>(() => sut.FindById(1));
                Assert.AreEqual("Order with 1 does not exist", ex.Message);
            }
        }
    }
}
