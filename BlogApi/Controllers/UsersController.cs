using Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Dto;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromForm] UserDTO userDto)
        {
            _logger.LogInformation("Creating a new user with username: {UserName}", userDto.UserName);
            var newUser = await _userService.CreateUserAsync(userDto);
            _logger.LogInformation("User created successfully with ID: {UserId}", newUser.Id);
            return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromForm] UserDTO userDto)
        {
            _logger.LogInformation("Updating user with ID: {UserId}", userDto.Id);
            var updatedUser = await _userService.UpdateUserAsync(userDto);
            _logger.LogInformation("User updated successfully.");
            return Ok(updatedUser);
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            _logger.LogInformation("Deleting user with ID: {UserId}", id);
            await _userService.DeleteUserAsync(id);
            _logger.LogInformation("User deleted successfully.");
            return NoContent();
        }

        [HttpGet("GetUser{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            _logger.LogInformation("Fetching user with ID: {UserId}", id);
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                _logger.LogWarning("User with ID: {UserId} not found.", id);
                return NotFound();
            }
            return Ok(user);
        }
    }
}
