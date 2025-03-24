using Microsoft.AspNetCore.Mvc;
using WMS_ERP_Backend.Models;
using WMS_ERP_Backend.Services;

namespace WMS_ERP_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LinkController : ControllerBase
    {
        private readonly LinkService _linkService;

        public LinkController(LinkService linkService)
        {
            _linkService = linkService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Link>>> GetAll()
        {
            var links = await _linkService.GetAll();
            if (links == null || links.Count() == 0)
            {
                return NotFound(
                    new
                    {
                        statusCode = 404,
                        type = "error",
                        message = "no links",
                        data = new { links = links },
                    }
                );
            }
            return Ok(
                new
                {
                    statusCode = 200,
                    type = "success",
                    message = "fetching links success",
                    data = new { links = links },
                }
            );
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Link>> GetById(int id) => Ok(await _linkService.GetById(id));

        [HttpPost]
        public async Task<ActionResult> Create(Link link)
        {
            if (link == null)
            {
                return BadRequest(
                    new
                    {
                        statusCode = 400,
                        type = "error",
                        message = "link is null",
                        data = link,
                    }
                );
            }
            await _linkService.Create(link);
            return Ok(
                new
                {
                    statusCode = 200,
                    type = "success",
                    message = "link is added",
                    data = link,
                }
            );
        }

        [HttpPut]
        public async Task<ActionResult> Update(Link link)
        {
            await _linkService.Update(link);
            return Ok($"Link with id = {link.Id} has been updated");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _linkService.Delete(id);

            return Ok(
                new
                {
                    statusCode = 200,
                    type = "success",
                    message = "link is deleted",
                }
            );
        }

        [HttpDelete("deleteLinks")]
        public async Task<ActionResult> DeleteLinks([FromBody] List<int> links)
        {
            if (links == null || !links.Any())
            {
                return BadRequest(
                    new
                    {
                        statusCode = 400,
                        type = "error",
                        message = "links list is empty",
                        data = links,
                    }
                );
            }

            await _linkService.DeleteMany(links);
            return Ok(
                new
                {
                    statusCode = 200,
                    type = "success",
                    message = "links are deleted",
                    data = links,
                }
            );
        }
    }
}
