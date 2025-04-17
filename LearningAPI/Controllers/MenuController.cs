﻿using Microsoft.AspNetCore.Mvc;
using WMS_ERP_Backend.Models;
using WMS_ERP_Backend.Services;

namespace WMS_ERP_Backend.Controllers
{
    [Route("menu")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly MenuService _menuService;

        public MenuController(MenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet("{menuId}")]
        public ActionResult<object> GetById(int menuId)
        {
            var menu = _menuService.GetById(menuId);
            if (menu == null)
            {
                return NotFound(
                    new
                    {
                        data = (object)null,
                        type = "error",
                        statusCode = 404,
                        message = "Menu not found",
                    }
                );
            }
            return Ok(
                new
                {
                    data = menu,
                    type = "success",
                    statusCode = 200,
                    message = "Menu fetched successfully",
                }
            );
        }

        [HttpGet]
        public ActionResult<object> GetAll()
        {
            var menus = _menuService.GetAll();
            return Ok(
                new
                {
                    data = menus,
                    type = "success",
                    statusCode = 200,
                    message = "Menus fetched successfully",
                }
            );
        }

        [HttpPost]
        public ActionResult<object> Create(Menu menu)
        {
            var menuId = _menuService.Create(menu);
            return CreatedAtAction(
                nameof(GetById),
                new { menuId = menuId },
                new
                {
                    data = menuId,
                    type = "success",
                    statusCode = 201,
                    message = "Menu created successfully",
                }
            );
        }

        [HttpPut]
        public ActionResult<object> Update(Menu menu)
        {
            var success = _menuService.Update(menu);
            if (!success)
            {
                return NotFound(
                    new
                    {
                        data = (object)null,
                        type = "Menu",
                        statusCode = 404,
                        message = "Menu not found",
                    }
                );
            }
            return NoContent();
        }

        [HttpDelete("{menuId}")]
        public ActionResult<object> Delete(int menuId)
        {
            var success = _menuService.Delete(menuId);
            if (!success)
            {
                return NotFound(
                    new
                    {
                        data = (object)null,
                        type = "Menu",
                        statusCode = 404,
                        message = "Menu not found",
                    }
                );
            }
            return NoContent();
        }
    }
}
