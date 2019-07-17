using StoreManagementSystem.Data.Context;
using StoreManagementSystem.Data.Models;
using StoreManagementSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreManagementSystem.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ShopContext context;

        public CustomerService(ShopContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Customer CreateCustomer(string firstName, string lastName, string eMail)
        {
            if (this.context.Customers.Any(c => c.FirstName == firstName) && this.context.Customers.Any(c => c.LastName == lastName))

            {
                throw new ArgumentException($"Customer with first name {firstName} and last name {lastName} already exists");
            }

            var customer = new Customer()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = (eMail == "" ? null : eMail)
            };

            this.context.Customers.Add(customer);
            this.context.SaveChanges();

            return customer;
        }

        public string CreateCustomerJson(Customer customer)
        {
            if (this.context.Customers.Any(c => c.FirstName == customer.FirstName) &&
                this.context.Customers.Any(c => c.LastName == customer.LastName))

            {
                return ($"Customer with first name {customer.FirstName} and last name {customer.LastName} already exists");
            }

            this.context.Customers.Add(customer);
            this.context.SaveChanges();
            return "";
        }
    }
}
