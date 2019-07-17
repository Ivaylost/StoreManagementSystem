using StoreManagementSystem.Data.Context;
using StoreManagementSystem.Data.Models;
using StoreManagementSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreManagementSystem.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ShopContext context;

        public SupplierService(ShopContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Supplier CreateSupplier(string supplierName, int identificationNumber, string representedBy,
                                        string companyAddress, string phoneNumber)
        {
            if (this.context.Suppliers.Any(c => c.SupplierName == supplierName))

            {
                throw new ArgumentException($"Supplier with name {supplierName} already exists");
            }

            var supplaier = new Supplier()
            {
                SupplierName = supplierName,
                IdentificationNumber = identificationNumber,
                RepresentedBy = representedBy,
                Address = companyAddress,
                PhoneNumber = (phoneNumber == "" ? null : phoneNumber)
            };

            this.context.Suppliers.Add(supplaier);
            this.context.SaveChanges();

            return supplaier;
        }

        public Supplier GetSupplier(string supplierName)
        {
            var supplier = this.context.Suppliers
                .FirstOrDefault(c => c.SupplierName == supplierName);

            if (supplier == null)
            {
                throw new ArgumentException($"Supplier {supplierName} does not exist");
            }

            return supplier;
        }

        public Supplier ChangeAddress(Supplier supplier, string newAddress)
        {
            supplier.Address = newAddress;
            this.context.SaveChanges();
            return supplier;
        }


        public Supplier FindByName(string name)
        {
            return this.context.Suppliers
                .FirstOrDefault(s => s.SupplierName == name);
        }

        public Supplier ChangeRepresenter(Supplier supplier, string newRepresenter)
        {
            supplier.RepresentedBy = newRepresenter;
            this.context.SaveChanges();
            return supplier;
        }
    }
}
