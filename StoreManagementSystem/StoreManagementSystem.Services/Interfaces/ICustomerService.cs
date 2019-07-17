using StoreManagementSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManagementSystem.Services.Interfaces
{
    public interface ICustomerService
    {
        Customer CreateCustomer(string firstName, string secondName, string eMail);
        string CreateCustomerJson(Customer customer);
    }
}
