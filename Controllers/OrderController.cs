using CoffeeShopApi.Data.WebApiDemo.Data;
using CoffeeShopApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShopApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderRepository _orderRepository;

        public OrderController(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }


        [HttpGet]
        public IActionResult GetAllOrders()
        {
            var orderList = _orderRepository.GetAllOrders();
            return Ok(orderList);
        }


        [HttpGet("{OrderID}")]
        public IActionResult GetOrderByID(int OrderID)
        {
            var orderList = _orderRepository.GetOrderByID(OrderID);
            return Ok(orderList);
        }


        [HttpDelete("{OrderID}")]
        public IActionResult DeleteOrderByID(int OrderID)
        {
            var isDeleted = _orderRepository.DeleteOrderByID(OrderID);
            if (isDeleted)
                return Ok(new { Message = "Order deleted successfully." });
            else
                return NotFound(new { Message = "Order not found or could not be deleted." });
        }

        [HttpPost]
        public IActionResult InsertOrder([FromBody] OrderModel order)
        {
            if (order == null)
                return BadRequest(new { Message = "Order data is required." });

            var isInserted = _orderRepository.InsertOrder(order);
            if (isInserted)
                return Ok(new { Message = "Order inserted successfully." });
            else
                return StatusCode(500, new { Message = "Order could not be inserted." });
        }

        [HttpPut("{OrderID}")]
        public IActionResult UpdateOrder(int OrderID, [FromBody] OrderModel order)
        {
            if (order == null || OrderID != order.OrderID)
                return BadRequest(new { Message = "Invalid order data or ID mismatch." });

            var isUpdated = _orderRepository.UpdateOrder(order);
            if (isUpdated)
                return Ok(new { Message = "Order updated successfully." });
            else
                return NotFound(new { Message = "Order not found or could not be updated." });
        }
    }
}
