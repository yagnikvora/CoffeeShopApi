using CoffeeShopApi.Data.WebApiDemo.Data;
using CoffeeShopApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShopApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductRepository _productRepository;

        public ProductController(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var productList = _productRepository.GetAllProducts();
            return Ok(productList);
        }


        [HttpGet("{ProductID}")]
        public IActionResult GetProductByID(int ProductID)
        {
            var productList = _productRepository.GetProductByID(ProductID);
            return Ok(productList);
        }


        [HttpDelete("{ProductID}")]
        public IActionResult DeleteProductByID(int ProductID)
        {
            var isDeleted = _productRepository.DeleteProductByID(ProductID);
            if (isDeleted)
                return Ok(new { Message = "Product deleted successfully." });
            else
                return NotFound(new { Message = "Product not found or could not be deleted." });
        }

        [HttpPost]
        public IActionResult InsertProduct([FromBody] ProductModel product)
        {
            if (product == null)
                return BadRequest(new { Message = "Product data is required." });

            var isInserted = _productRepository.InsertProduct(product);
            if (isInserted)
                return Ok(new { Message = "Product inserted successfully." });
            else
                return StatusCode(500, new { Message = "Product could not be inserted." });
        }

        [HttpPut("{ProductID}")]
        public IActionResult UpdateProduct(int ProductID, [FromBody] ProductModel product)
        {
            if (product == null || ProductID != product.ProductID)
                return BadRequest(new { Message = "Invalid product data or ID mismatch." });

            var isUpdated = _productRepository.UpdateProduct(product);
            if (isUpdated)
                return Ok(new { Message = "Product updated successfully." });
            else
                return NotFound(new { Message = "Product not found or could not be updated." });
        }
    }
}
