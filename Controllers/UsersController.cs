using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UsersManagementAPI.Models;
using UsersManagementAPI.Services;

namespace UsersManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserSevice _userService;
        public UsersController(IUserSevice userService)
        {
            _userService = userService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllUsers()
        {
            var usersList = await _userService.GetAllUsersAsync();
            return Ok(usersList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if(user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddNewUser([FromBody] UserModel userModel)
        {
            var id = await _userService.AddUserAsync(userModel);
            return CreatedAtAction(nameof(GetUserById), new { id = id, controller = "users" }, id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromBody] UserModel userModel, [FromRoute] int id)
        {
            await _userService.UpdateUserAsync(id, userModel);
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUserPatch([FromBody] JsonPatchDocument userModel, [FromRoute] int id)
        {
            await _userService.UpdateUserPatchAsync(id, userModel);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            await _userService.DeleteUserAsync(id);
            return Ok();
        }
    }
}
