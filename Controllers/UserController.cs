using CoffeeShopApi.Data;
using CoffeeShopApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShopApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var userList = _userRepository.GetAllUsers();
            return Ok(userList);
        }


        [HttpGet("{UserID}")]
        public IActionResult GetUserByID(int UserID)
        {
            var userList = _userRepository.GetUserByID(UserID);
            return Ok(userList);
        }


        [HttpDelete("{UserID}")]
        public IActionResult DeleteUserByID(int UserID)
        {
            var isDeleted = _userRepository.DeleteUserByID(UserID);
            if (isDeleted)
                return Ok(new { Message = "User deleted successfully." });
            else
                return NotFound(new { Message = "User not found or could not be deleted." });
        }

        [HttpPost]
        public IActionResult InsertUser([FromBody] UserModel user)
        {
            if (user == null)
                return BadRequest(new { Message = "User data is required." });

            var isInserted = _userRepository.InsertUser(user);
            if (isInserted)
                return Ok(new { Message = "User inserted successfully." });
            else
                return StatusCode(500, new { Message = "User could not be inserted." });
        }

        [HttpPut("{UserID}")]
        public IActionResult UpdateUser(int UserID, [FromBody] UserModel user)
        {
            if (user == null || UserID != user.UserID)
                return BadRequest(new { Message = "Invalid user data or ID mismatch." });

            var isUpdated = _userRepository.UpdateUser(user);
            if (isUpdated)
                return Ok(new { Message = "User updated successfully." });
            else
                return NotFound(new { Message = "User not found or could not be updated." });
        }
    }
}
