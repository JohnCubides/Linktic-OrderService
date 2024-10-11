using OrderManagementService.Core.Entities;
using System;
using System.Collections.Generic;

namespace OrderManagementService.Core.Interfaces.Services
{
    public interface IOrderService
    {
        IEnumerable<Order> GetAllOrders();
        Order GetOrderById(Guid orderId);
        Order CreateOrder(Order order);
        Order UpdateOrder(Order order);
        void DeleteOrder(Guid orderId);
    }
}
