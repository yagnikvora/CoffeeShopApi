using CoffeeShopApi.Data;
using CoffeeShopApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShopApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerRepository _customerRepository;

        public CustomerController(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }


        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            var customerList = _customerRepository.GetAllCustomers();
            return Ok(customerList);
        }


        [HttpGet("{CustomerID}")]
        public IActionResult GetCustomerByID(int CustomerID)
        {
            var customerList = _customerRepository.GetCustomerByID(CustomerID);
            return Ok(customerList);
        }


        [HttpDelete("{CustomerID}")]
        public IActionResult DeleteCustomerByID(int CustomerID)
        {
            var isDeleted = _customerRepository.DeleteCustomerByID(CustomerID);
            if (isDeleted)
                return Ok(new { Message = "Customer deleted successfully." });
            else
                return NotFound(new { Message = "Customer not found or could not be deleted." });
        }

        [HttpPost]
        public IActionResult InsertCustomer([FromBody] CustomerModel customer)
        {
            if (customer == null)
                return BadRequest(new { Message = "Customer data is required." });

            var isInserted = _customerRepository.InsertCustomer(customer);
            if (isInserted)
                return Ok(new { Message = "Customer inserted successfully." });
            else
                return StatusCode(500, new { Message = "Customer could not be inserted." });
        }

        [HttpPut("{CustomerID}")]
        public IActionResult UpdateCustomer(int CustomerID, [FromBody] CustomerModel customer)
        {
            if (customer == null || CustomerID != customer.CustomerID)
                return BadRequest(new { Message = "Invalid customer data or ID mismatch." });

            var isUpdated = _customerRepository.UpdateCustomer(customer);
            if (isUpdated)
                return Ok(new { Message = "Customer updated successfully." });
            else
                return NotFound(new { Message = "Customer not found or could not be updated." });
        }
    }
}
