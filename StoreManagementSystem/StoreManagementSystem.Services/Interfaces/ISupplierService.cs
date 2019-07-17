using StoreManagementSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManagementSystem.Services.Interfaces
{
    public interface ISupplierService
    {
        Supplier CreateSupplier(string supplierName, int identificationNumber, string representedBy, string companyAddress, string phoneNumber);
        Supplier GetSupplier(string supplierName);
        Supplier ChangeAddress(Supplier supplier, string newAddress);
        Supplier FindByName(string name);
        Supplier ChangeRepresenter(Supplier supplier, string newRepresenter);
    }
}
