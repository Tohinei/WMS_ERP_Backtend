using System.Data;
using Microsoft.AspNetCore.Mvc;
using WMS_ERP_Backend.Models;
using WMS_ERP_Backend.Services;

namespace WMS_ERP_Backend.Controllers
{
    [Route("role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleService _roleService;

        public RoleController(RoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("{roleId}")]
        public ActionResult<object> GetById(int roleId)
        {
            var role = _roleService.GetById(roleId);
            if (role == null)
            {
                return NotFound(
                    new
                    {
                        data = (object)null,
                        type = "Role",
                        statusCode = 404,
                        message = "Role not found",
                    }
                );
            }
            return Ok(
                new
                {
                    data = role,
                    type = "Role",
                    statusCode = 200,
                    message = "Role fetched successfully",
                }
            );
        }

        [HttpGet]
        public ActionResult<object> GetAll()
        {
            var roles = _roleService.GetAll();
            return Ok(
                new
                {
                    data = roles,
                    type = "Role",
                    statusCode = 200,
                    message = "Roles fetched successfully",
                }
            );
        }

        [HttpPost]
        public ActionResult<object> Create(Role role)
        {
            var roleId = _roleService.Create(role);
            return CreatedAtAction(
                nameof(GetById),
                new { roleId = roleId },
                new
                {
                    data = roleId,
                    type = "Role",
                    statusCode = 201,
                    message = "Role created successfully",
                }
            );
        }

        [HttpPut]
        public ActionResult<object> Update(Role role)
        {
            var success = _roleService.Update(role);
            if (!success)
            {
                return NotFound(
                    new
                    {
                        data = (object)null,
                        type = "success",
                        statusCode = 404,
                        message = "Role not found",
                    }
                );
            }
            return Ok(
                new
                {
                    data = role,
                    type = "success",
                    statusCode = 200,
                    message = "role updated successfully",
                }
            );
        }

        [HttpDelete("{roleId}")]
        public ActionResult<object> Delete(int roleId)
        {
            var success = _roleService.Delete(roleId);
            if (!success)
            {
                return NotFound(
                    new
                    {
                        data = (object)null,
                        type = "Role",
                        statusCode = 404,
                        message = "Role not found",
                    }
                );
            }
            return Ok(
                new
                {
                    data = roleId,
                    type = "success",
                    statusCode = 200,
                    message = "role deleted successfully",
                }
            );
        }
    }
}
