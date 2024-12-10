using CoffeeShopApi.Data;
using CoffeeShopApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShopApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly OrderDetailRepository _orderDetailRepository;

        public OrderDetailController(OrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }


        [HttpGet]
        public IActionResult GetAllOrderDetails()
        {
            var orderDetailList = _orderDetailRepository.GetAllOrderDetails();
            return Ok(orderDetailList);
        }


        [HttpGet("{OrderDetailID}")]
        public IActionResult GetOrderDetailByID(int OrderDetailID)
        {
            var orderDetailList = _orderDetailRepository.GetOrderDetailByID(OrderDetailID);
            return Ok(orderDetailList);
        }


        [HttpDelete("{OrderDetailID}")]
        public IActionResult DeleteOrderDetailByID(int OrderDetailID)
        {
            var isDeleted = _orderDetailRepository.DeleteOrderDetailByID(OrderDetailID);
            if (isDeleted)
                return Ok(new { Message = "OrderDetail deleted successfully." });
            else
                return NotFound(new { Message = "OrderDetail not found or could not be deleted." });
        }

        [HttpPost]
        public IActionResult InsertOrderDetail([FromBody] OrderDetailModel orderDetail)
        {
            if (orderDetail == null)
                return BadRequest(new { Message = "OrderDetail data is required." });

            var isInserted = _orderDetailRepository.InsertOrderDetail(orderDetail);
            if (isInserted)
                return Ok(new { Message = "OrderDetail inserted successfully." });
            else
                return StatusCode(500, new { Message = "OrderDetail could not be inserted." });
        }

        [HttpPut("{OrderDetailID}")]
        public IActionResult UpdateOrderDetail(int OrderDetailID, [FromBody] OrderDetailModel orderDetail)
        {
            if (orderDetail == null || OrderDetailID != orderDetail.OrderDetailID)
                return BadRequest(new { Message = "Invalid orderDetail data or ID mismatch." });

            var isUpdated = _orderDetailRepository.UpdateOrderDetail(orderDetail);
            if (isUpdated)
                return Ok(new { Message = "OrderDetail updated successfully." });
            else
                return NotFound(new { Message = "OrderDetail not found or could not be updated." });
        }
    }
}
