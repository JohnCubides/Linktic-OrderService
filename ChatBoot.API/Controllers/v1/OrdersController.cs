using Microsoft.AspNetCore.Mvc;
using OrderManagementService.Core.Entities;
using OrderManagementService.Core.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace OrderManagementService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/orders
        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetAllOrders()
        {
            var orders = _orderService.GetAllOrders();
            return Ok(orders);
        }

        // GET: api/orders/{id}
        [HttpGet("{id}")]
        public ActionResult<Order> GetOrderById(Guid id)
        {
            var order = _orderService.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        // POST: api/orders
        [HttpPost]
        public ActionResult CreateOrder([FromBody] Order order)
        {
            var createdOrder = _orderService.CreateOrder(order);
            return CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.OrderId }, createdOrder);
        }

        // PUT: api/orders/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateOrder(Guid id, [FromBody] Order order)
        {
            if (id != order.OrderId)
            {
                return BadRequest("El ID del pedido no coincide con el ID proporcionado.");
            }

            var updatedOrder = _orderService.UpdateOrder(order);
            return NoContent();
        }

        // DELETE: api/orders/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteOrder(Guid id)
        {
            _orderService.DeleteOrder(id);
            return NoContent();
        }
    }
}
