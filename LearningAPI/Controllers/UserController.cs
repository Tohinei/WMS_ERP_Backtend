using System.Threading.Tasks;
using LearningAPI.Models;
using LearningAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LearningAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAll() => Ok(await _userService.GetAll());

        [HttpGet("/{id}")]
        public async Task<ActionResult<User>> GetById(int id) => Ok(await _userService.GetById(id));

        [HttpGet("/role/{role}")]
        public async Task<ActionResult<List<User>>> GetByRole(String role) =>
            Ok(await _userService.GetByRole(role));

        [HttpPost]
        public async Task<ActionResult> Add(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            await _userService.Add(user);
            return Ok($"New user with id = {user.Id} has been added");
        }

        [HttpPut]
        public async Task<ActionResult> Update(User user)
        {
            await _userService.Update(user);
            return Ok($"User with id = {user.Id} has been updated");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _userService.Delete(id);
            return Ok($"User with id = {id} has been deleted");
        }

        [HttpPatch]
        public async Task<ActionResult> UpdateRole(int id, String role)
        {
            await _userService.UpdateRole(id, role);
            return Ok($"User with id = {id} role has been updated to the role of {role}");
        }

        [HttpDelete("deleteUsers")]
        public async Task<ActionResult> DeleteUsers([FromBody] int[] userIds)
        {
            if (userIds == null || userIds.Length == 0)
            {
                return BadRequest();
            }

            await _userService.DeleteUsers(userIds);
            return Ok();
        }
    }
}
