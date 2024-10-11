using OrderManagementService.Core.Entities;
using OrderManagementService.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrderManagementService.Core.Interfaces.Repositories;
using ChatBoot.Core.Interfaces.WebSocket;

namespace OrderManagementService.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IWebSocketHandler _webSocketHandler;

        public OrderService(IRepository<Order> orderRepository, IWebSocketHandler webSocketHandler)
        {
            _orderRepository = orderRepository;
            _webSocketHandler = webSocketHandler;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _orderRepository.GetAll();
        }

        public Order GetOrderById(Guid orderId)
        {
            int intId = ConvertGuidToInt(orderId);
            return _orderRepository.GetById(intId);
        }

        public Order CreateOrder(Order order)
        {
            var createdOrder = _orderRepository.Add(order);
            _webSocketHandler.SendMessageAsync($"Order {createdOrder.OrderId} created successfully").Wait();
            return createdOrder;
        }

        public Order UpdateOrder(Order order)
        {
            var updatedOrder = _orderRepository.Update(order);
            _webSocketHandler.SendMessageAsync($"Order {updatedOrder.OrderId} updated").Wait();
            return updatedOrder;
        }

        public void DeleteOrder(Guid orderId)
        {
            int intId = ConvertGuidToInt(orderId);
            var order = _orderRepository.GetById(intId);
            if (order != null)
            {
                _orderRepository.Delete(order);
                _webSocketHandler.SendMessageAsync($"Order {order.OrderId} deleted").Wait();
            }
        }

        private int ConvertGuidToInt(Guid guid)
        {
            byte[] bytes = guid.ToByteArray();
            int intValue = BitConverter.ToInt32(bytes, 0);
            return Math.Abs(intValue);
        }
    }
}