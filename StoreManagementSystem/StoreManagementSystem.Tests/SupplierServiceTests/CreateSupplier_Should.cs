using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoreManagementSystem.Data.Context;
using StoreManagementSystem.Data.Models;
using StoreManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManagementSystem.Tests.SupplierServiceTests
{
    [TestClass]
    public class CreateSupplier_Should
    {
        [TestMethod]
        public void Throw_Argument_Exception_If_Supplier_Exists()
        {
            using (var arrangeContext = new ShopContext(TestUtils.GetOptions(nameof(Throw_Argument_Exception_If_Supplier_Exists))))
            {
                var sut = new SupplierService(arrangeContext);

                var supplier = new Supplier()
                {
                    SupplierName="Gosho",
                    Address="Sofia adress",
                    IdentificationNumber=1010101010,
                    PhoneNumber="0872828828",
                    RepresentedBy="Pesho"

                };

                arrangeContext.Suppliers.Add(supplier);
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new ShopContext(TestUtils.GetOptions(nameof(Throw_Argument_Exception_If_Supplier_Exists))))
            {
                var sut = new SupplierService(assertContext);


                var ex = Assert.ThrowsException<ArgumentException>(() => sut.CreateSupplier("Gosho", 1010101010, "Tosho", "Sofia adress", "0872828828"));
                Assert.AreEqual($"Supplier with name Gosho already exists", ex.Message);
            }
        }

        [TestMethod]
        public void Succeed_If_Supplier_DoesntExist()
        {

            using (var assertContext = new ShopContext(TestUtils.GetOptions(nameof(Succeed_If_Supplier_DoesntExist))))
            {
                var sut = new SupplierService(assertContext);


                var supplier = sut.CreateSupplier("Gosho", 1010101010, "Tosho", "Sofia adress", "0872828828");

                Assert.AreEqual("Gosho", supplier.SupplierName);
                Assert.AreEqual(1010101010, supplier.IdentificationNumber);
                Assert.AreEqual("Tosho", supplier.RepresentedBy);
                Assert.AreEqual("Sofia adress", supplier.Address);
                Assert.AreEqual("0872828828", supplier.PhoneNumber);

            }
        }
    }
}
