using Microsoft.AspNetCore.Mvc;
using WMS_ERP_Backend.Models;
using WMS_ERP_Backend.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WMS_ERP_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly RoleService _roleService;

        public RoleController(RoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Role>>> GetAll()
        {
            var roles = await _roleService.GetAll();
            if (roles == null || roles.Count() == 0)
            {
                return NotFound(
                    new
                    {
                        statusCode = 404,
                        type = "error",
                        message = "no roles",
                        data = new { roles = roles },
                    }
                );
            }
            return Ok(
                new
                {
                    statusCode = 200,
                    type = "success",
                    message = "fetching roles success",
                    data = new { roles = roles },
                }
            );
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetById(int id) => Ok(await _roleService.GetById(id));

        [HttpPost]
        public async Task<ActionResult> Create(Role role)
        {
            if (role == null)
            {
                return BadRequest(
                    new
                    {
                        statusCode = 400,
                        type = "error",
                        message = "role is null",
                        data = role,
                    }
                );
            }
            await _roleService.Create(role);
            return Ok(
                new
                {
                    statusCode = 200,
                    type = "success",
                    message = "role is added",
                    data = role,
                }
            );
        }

        [HttpPut]
        public async Task<ActionResult> Update(Role role)
        {
            await _roleService.Update(role);
            return Ok($"Role with id = {role.Id} has been updated");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _roleService.Delete(id);
            return Ok($"Role with id = {id} has been deleted");
        }

        [HttpDelete("deleteRoles")]
        public async Task<ActionResult> DeleteRoles([FromBody] List<int> roles)
        {
            if (roles == null || !roles.Any())
            {
                return BadRequest(
                    new
                    {
                        statusCode = 400,
                        type = "error",
                        message = "links list is empty",
                        data = roles,
                    }
                );
            }

            await _roleService.DeleteMany(roles);
            return Ok(
                new
                {
                    statusCode = 200,
                    type = "success",
                    message = "links are deleted",
                    data = roles,
                }
            );
        }
    }
}
