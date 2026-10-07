using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.DTOs;
using UserManagementAPI.Services;

namespace UserManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/users?page=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page < 1 || pageSize < 1)
            {
                return BadRequest(new { message = "Page and pageSize must be greater than 0." });
            }

            var result = await _userService.GetUsersAsync(page, pageSize);
            return Ok(new
            {
                TotalCount = result.TotalCount,
                Page = page,
                PageSize = pageSize,
                Users = result.Users
            });
        }

        // GET: api/users/search?name=value
        [HttpGet("search")]
        public async Task<IActionResult> SearchUsers([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest(new { message = "Search query cannot be empty." });
            }

            var users = await _userService.SearchUsersAsync(name);
            return Ok(users);
        }

        // GET: api/users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound(new { message = $"User with ID {id} not found." });
            }
            return Ok(user);
        }

        // POST: api/users
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdUser = await _userService.CreateUserAsync(createUserDto);
            
            if (createdUser == null)
            {
                return Conflict(new { message = "A user with this email already exists." });
            }

            return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
        }

        // PUT: api/users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto updateUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isUpdated = await _userService.UpdateUserAsync(id, updateUserDto);
            if (!isUpdated)
            {
                return NotFound(new { message = $"User with ID {id} not found." });
            }

            return NoContent();
        }

        // DELETE: api/users/5
        // Example of an authorized endpoint
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var isDeleted = await _userService.DeleteUserAsync(id);
            if (!isDeleted)
            {
                return NotFound(new { message = $"User with ID {id} not found." });
            }

            return NoContent();
        }
    }
}
