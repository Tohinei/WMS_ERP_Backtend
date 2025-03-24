using Microsoft.AspNetCore.Mvc;
using WMS_ERP_Backend.Models;
using WMS_ERP_Backend.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WMS_ERP_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly MenuService _menuService;

        public MenuController(MenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Menu>>> GetAll()
        {
            var menus = await _menuService.GetAll();
            if (menus == null || menus.Count() == 0)
            {
                return NotFound(
                    new
                    {
                        statusCode = 404,
                        type = "error",
                        message = "no menus",
                        data = new { menus = menus },
                    }
                );
            }
            return Ok(
                new
                {
                    statusCode = 200,
                    type = "success",
                    message = "fetching menus success",
                    data = new { menus = menus },
                }
            );
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Menu>> GetById(int id) => Ok(await _menuService.GetById(id));

        [HttpPost]
        public async Task<ActionResult> Create(Menu menu)
        {
            if (menu == null)
            {
                return BadRequest(
                    new
                    {
                        statusCode = 400,
                        type = "error",
                        message = "Menu is null",
                    }
                );
            }
            var id = await _menuService.Create(menu);
            return Ok(
                new
                {
                    statusCode = 200,
                    type = "success",
                    message = "Menu is added",
                    data = id,
                }
            );
        }

        [HttpPut]
        public async Task<ActionResult> Update(Menu menu)
        {
            await _menuService.Update(menu);
            return Ok($"Menu with id = {menu.Id} has been updated");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _menuService.Delete(id);
            return Ok($"Menu with id = {id} has been deleted");
        }

        [HttpDelete("deleteMenus")]
        public async Task<ActionResult> DeleteMenus([FromBody] List<int> menus)
        {
            if (menus == null || !menus.Any())
            {
                return BadRequest(
                    new
                    {
                        statusCode = 400,
                        type = "error",
                        message = "links list is empty",
                        data = new { menus = menus },
                    }
                );
            }

            await _menuService.DeleteMany(menus);
            return Ok(
                new
                {
                    statusCode = 200,
                    type = "success",
                    message = "links are deleted",
                    data = new { menus = menus },
                }
            );
        }
    }
}
