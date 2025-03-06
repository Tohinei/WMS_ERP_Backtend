using LearningAPI.Data;
using LearningAPI.Models;
using LearningAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearningAPI.Controllers
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
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _context.Users.SingleOrDefaultAsync(user =>
                user.Email == request.Email
            );

            if (user == null)
                return BadRequest(new { message = "Invalid email" });

            if (!_jwtService.VerifyPassword(request.Password, user.Password))
                return BadRequest(new { message = "Invalid password" });

            user.IsActive = true;
            await _context.SaveChangesAsync();

            var token = _jwtService.GenerateToken(user.Email);
            return Ok(
                new
                {
                    message = "logged",
                    Token = token,
                    user,
                }
            );
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var userExists = await _context.Users.AnyAsync(x => x.Email == request.Email);

            var hashedPassword = _jwtService.HashPassword(request.Password);

            if (userExists)
                return Unauthorized("Already Exist");

            var user = new User
            {
                Email = request.Email,
                Password = hashedPassword,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Age = request.Age,
                IsActive = false,
                Role = Role.Employee,
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User has been added" });
        }
    }
}
