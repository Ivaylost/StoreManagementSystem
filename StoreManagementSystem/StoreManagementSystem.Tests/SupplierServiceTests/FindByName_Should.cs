﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoreManagementSystem.Data.Context;
using StoreManagementSystem.Data.Models;
using StoreManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManagementSystem.Tests.SupplierServiceTests
{
    [TestClass]
    public class FindByName_Should
    {
        [TestMethod]
        public void Succeed_If_SupplierExists()
        {
            using (var arrangeContext = new ShopContext(TestUtils.GetOptions(nameof(Succeed_If_SupplierExists))))
            {
                var sut = new SupplierService(arrangeContext);

                var supplier = new Supplier()
                {
                    SupplierName = "Tosho",
                    Address = "Sofia adress",
                    IdentificationNumber = 1010101010,
                    PhoneNumber = "0872828828",
                    RepresentedBy = "Pesho"

                };

                arrangeContext.Suppliers.Add(supplier);
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new ShopContext(TestUtils.GetOptions(nameof(Succeed_If_SupplierExists))))
            {
                var sut = new SupplierService(assertContext);

                var supplier = sut.FindByName("Tosho");

                Assert.AreEqual(supplier.SupplierName, "Tosho");
                Assert.AreEqual(supplier.Address, "Sofia adress");
                Assert.AreEqual(supplier.IdentificationNumber, 1010101010);
                Assert.AreEqual(supplier.PhoneNumber, "0872828828");
                Assert.AreEqual(supplier.RepresentedBy, "Pesho");

            }
        }
    }
}
