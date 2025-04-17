using Microsoft.AspNetCore.Mvc;
using WMS_ERP_Backend.Models;
using WMS_ERP_Backend.Services;

namespace WMS_ERP_Backend.Controllers
{
    [Route("session")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly SessionService _sessionService;

        public SessionController(SessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpGet("{sessionId}")]
        public ActionResult<object> GetById(int sessionId)
        {
            var session = _sessionService.GetById(sessionId);
            if (session == null)
            {
                return NotFound(
                    new
                    {
                        data = (object)null,
                        type = "Session",
                        statusCode = 404,
                        message = "Session not found",
                    }
                );
            }
            return Ok(
                new
                {
                    data = session,
                    type = "Session",
                    statusCode = 200,
                    message = "Session fetched successfully",
                }
            );
        }

        [HttpGet]
        public ActionResult<object> GetAll()
        {
            var sessions = _sessionService.GetAll();
            return Ok(
                new
                {
                    data = sessions,
                    type = "success",
                    statusCode = 200,
                    message = "Sessions fetched successfully",
                }
            );
        }

        [HttpPost]
        public ActionResult<object> Create(Session session)
        {
            var sessionId = _sessionService.Create(session);
            return Ok(
                new
                {
                    data = sessionId,
                    type = "success",
                    statusCode = 201,
                    message = "Session created successfully",
                }
            );
        }

        [HttpPut]
        public ActionResult<object> Update(Session session)
        {
            var success = _sessionService.Update(session);
            if (!success)
            {
                return NotFound(
                    new
                    {
                        data = (object)null,
                        type = "Session",
                        statusCode = 404,
                        message = "Session not found",
                    }
                );
            }
            return NoContent();
        }

        [HttpDelete("{sessionId}")]
        public ActionResult<object> Delete(int sessionId)
        {
            var success = _sessionService.Delete(sessionId);
            if (!success)
            {
                return NotFound(
                    new
                    {
                        data = (object)null,
                        type = "Session",
                        statusCode = 404,
                        message = "Session not found",
                    }
                );
            }
            return NoContent();
        }
    }
}
