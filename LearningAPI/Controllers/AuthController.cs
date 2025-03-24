using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WMS_ERP_Backend.Data;
using WMS_ERP_Backend.Models;
using WMS_ERP_Backend.Models.AuthModels;
using WMS_ERP_Backend.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WMS_ERP_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _jwtService = new JwtService("mysecretkeymysecretkeymysecretkeymysecretkeymysecretkey");
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest req)
        {
            var user = await _context
                .Users.Include(r => r.Role)
                .ThenInclude(m => m.Menu)
                .ThenInclude(l => l.Links)
                .FirstOrDefaultAsync(user => user.Email == req.Email);

            if (user == null)
                return BadRequest(
                    new
                    {
                        message = "Invalid email",
                        type = "error",
                        statusCode = 400,
                    }
                );

            //if (!_jwtService.VerifyPassword(req.Password, user.Password))
            //return BadRequest(
            //    new
            //    {
            //        message = "Invalid password",
            //        type = "error",
            //        statusCode = 400,
            //    }
            //);

            user.Status = true;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            var token = _jwtService.GenerateToken(user.Email);

            return Ok(
                new
                {
                    statusCode = 200,
                    type = "success",
                    message = "User Logged In",
                    data = new { token = token, loggedUser = user },
                }
            );
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest req)
        {
            var userExists = await _context.Users.AnyAsync(x => x.Email == req.Email);

            var hashedPassword = _jwtService.HashPassword(req.Password);

            if (userExists)
                return Unauthorized("Already Exist");

            var user = new User
            {
                Email = req.Email,
                Password = hashedPassword,
                FirstName = req.FirstName,
                LastName = req.LastName,
                RoleId = req.RoleId,
                Role = req.Role,
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User has been added" });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LoginRequest req)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Email == req.Email);

            user.Status = false;
            _context.Users.Update(user);
            _context.SaveChanges();

            return Ok(
                new
                {
                    message = "you logout",
                    type = "success",
                    statusCode = 200,
                }
            );
        }
    }
}
