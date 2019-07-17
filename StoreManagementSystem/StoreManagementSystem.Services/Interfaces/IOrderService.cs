using StoreManagementSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManagementSystem.Services.Interfaces
{
    public interface IOrderService
    {
        Order CreateOrder(string customerName, IReadOnlyCollection<Product> products);

        Order FindById(int orderId);

        Order OrderStorno(Order order);
    }
}
