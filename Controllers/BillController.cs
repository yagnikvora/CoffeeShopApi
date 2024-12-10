using CoffeeShopApi.Data;
using CoffeeShopApi.Data.WebApiDemo.Data;
using CoffeeShopApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShopApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly BillRepository _billRepository;

        public BillController(BillRepository billRepository)
        {
            _billRepository = billRepository;
        }


        [HttpGet]
        public IActionResult GetAllBills()
        {
            var billList = _billRepository.GetAllBills();
            return Ok(billList);
        }


        [HttpGet("{BillID}")]
        public IActionResult GetBillByID(int BillID)
        {
            var billList = _billRepository.GetBillByID(BillID);
            return Ok(billList);
        }


        [HttpDelete("{BillID}")]
        public IActionResult DeleteBillByID(int BillID)
        {
            var isDeleted = _billRepository.DeleteBillByID(BillID);
            if (isDeleted)
                return Ok(new { Message = "Bill deleted successfully." });
            else
                return NotFound(new { Message = "Bill not found or could not be deleted." });
        }

        [HttpPost]
        public IActionResult InsertBill([FromBody] BillModel bill)
        {
            if (bill == null)
                return BadRequest(new { Message = "Bill data is required." });

            var isInserted = _billRepository.InsertBill(bill);
            if (isInserted)
                return Ok(new { Message = "Bill inserted successfully." });
            else
                return StatusCode(500, new { Message = "Bill could not be inserted." });
        }

        [HttpPut("{BillID}")]
        public IActionResult UpdateBill(int BillID, [FromBody] BillModel bill)
        {
            if (bill == null || BillID != bill.BillID)
                return BadRequest(new { Message = "Invalid bill data or ID mismatch." });

            var isUpdated = _billRepository.UpdateBill(bill);
            if (isUpdated)
                return Ok(new { Message = "Bill updated successfully." });
            else
                return NotFound(new { Message = "Bill not found or could not be updated." });
        }
    }
}
