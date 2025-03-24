using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WMS_ERP_Backend.Models;
using WMS_ERP_Backend.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WMS_ERP_Backend.Controllers
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
        public async Task<ActionResult<List<User>>> GetAll()
        {
            var users = await _userService.GetAll();
            if (users == null || users.Count() == 0)
            {
                return NotFound(
                    new
                    {
                        statusCode = 404,
                        type = "error",
                        message = "no users",
                        data = new { users = users },
                    }
                );
            }
            return Ok(
                new
                {
                    statusCode = 200,
                    type = "success",
                    message = "fetching users success",
                    data = new { users = users },
                }
            );
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id) => Ok(await _userService.GetById(id));

        [HttpPost]
        public async Task<ActionResult> Create(User user)
        {
            if (user == null)
            {
                return BadRequest(
                    new
                    {
                        statusCode = 400,
                        type = "error",
                        message = "user is null",
                        data = user,
                    }
                );
            }

            await _userService.Create(user);
            return Ok(
                new
                {
                    statusCode = 200,
                    type = "success",
                    message = "user is added",
                    data = user,
                }
            );
        }

        [HttpPut]
        public async Task<ActionResult> Update(User user)
        {
            await _userService.Update(user);
            return Ok(
                new
                {
                    statusCode = 200,
                    type = "success",
                    message = "user is updated",
                    data = user,
                }
            );
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _userService.Delete(id);
            return Ok($"User with id = {id} has been deleted");
        }

        [HttpDelete("deleteUsers")]
        public async Task<ActionResult> DeleteUsers([FromBody] List<int> users)
        {
            if (users == null || !users.Any())
            {
                return BadRequest(
                    new
                    {
                        statusCode = 400,
                        type = "error",
                        message = "links list is empty",
                        data = users,
                    }
                );
            }

            await _userService.DeleteMany(users);
            return Ok(
                new
                {
                    statusCode = 200,
                    type = "success",
                    message = "links are deleted",
                    data = users,
                }
            );
        }
    }
}
