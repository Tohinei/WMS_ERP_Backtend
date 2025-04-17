using Microsoft.AspNetCore.Mvc;
using WMS_ERP_Backend.Models;
using WMS_ERP_Backend.Services;

namespace WMS_ERP_Backend.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{userId}")]
        public ActionResult<object> GetById(int userId)
        {
            var user = _userService.GetById(userId);
            if (user == null)
            {
                return NotFound(
                    new
                    {
                        data = (object)null,
                        type = "User",
                        statusCode = 404,
                        message = "User not found",
                    }
                );
            }
            return Ok(
                new
                {
                    data = user,
                    type = "User",
                    statusCode = 200,
                    message = "User fetched successfully",
                }
            );
        }

        [HttpGet]
        public ActionResult<object> GetAll()
        {
            var users = _userService.GetAll();
            return Ok(
                new
                {
                    data = users,
                    type = "User",
                    statusCode = 200,
                    message = "Users fetched successfully",
                }
            );
        }

        [HttpPost]
        public ActionResult<object> Create(User user)
        {
            var userId = _userService.Create(user);
            return Ok(
                new
                {
                    data = userId,
                    type = "success",
                    statusCode = 201,
                    message = "User created successfully",
                }
            );
        }

        [HttpPut]
        public ActionResult<object> Update(User user)
        {
            var success = _userService.Update(user);
            if (!success)
            {
                return NotFound(
                    new
                    {
                        data = (object)null,
                        type = "error",
                        statusCode = 404,
                        message = "User not found",
                    }
                );
            }
            return Ok(
                new
                {
                    data = user,
                    type = "success",
                    statusCode = 200,
                    message = "User updated successfully",
                }
            );
        }

        [HttpDelete("{userId}")]
        public ActionResult<object> Delete(int userId)
        {
            var success = _userService.Delete(userId);
            if (!success)
            {
                return NotFound(
                    new
                    {
                        data = (object)null,
                        type = "User",
                        statusCode = 404,
                        message = "User not found",
                    }
                );
            }
            return NoContent();
        }

        [HttpDelete("many")]
        public ActionResult<object> DeleteMany(int userId)
        {
            var success = _userService.DeleteMany(new List<int> { userId });
            if (!success)
            {
                return NotFound(
                    new
                    {
                        data = (object)null,
                        type = "error",
                        statusCode = 404,
                        message = "User not found",
                    }
                );
            }

            return Ok(
                new
                {
                    data = (object)null,
                    type = "success",
                    statusCode = 200,
                    message = "User deleted",
                }
            );
        }
    }
}
