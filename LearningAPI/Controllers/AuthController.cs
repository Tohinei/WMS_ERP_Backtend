using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using WMS_ERP_Backend.DaoProject.Services;
using WMS_ERP_Backend.Models;
using WMS_ERP_Backend.Services;

namespace WMS_ERP_Backend.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly string _connectionString;
        private readonly MenuService _menuService;

        public AuthController(IConfiguration configuration, MenuService menuService)
        {
            _jwtService = new JwtService(
                "3fd133c1259bb53e43826ce7758897e6bafb804fb93b89f2e553796d7af58c99"
            );
            _connectionString = configuration.GetConnectionString("Connection");
            _menuService = menuService;
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
        {
            dynamic user = await GetUserByEmailAsync(request.Email);
            if (user == null || !_jwtService.VerifyPassword(request.Password, user.Password))
            {
                return Unauthorized("Invalid credentials.");
            }

            var token = _jwtService.GenerateToken(user.Email);
            return Ok(
                new
                {
                    data = new { Token = token, loggedUser = user },
                    message = "logged in",
                    type = "success",
                    statusCode = 200,
                }
            );
        }

        private async Task<object> GetUserByEmailAsync(string email)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                Console.WriteLine(connection.ConnectionString);
                var query =
                    "SELECT UserId, FirstName, LastName, Email, Password, RoleId, MenuId, CreatedAt, UpdatedAt FROM [User] WHERE Email = @Email";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            dynamic menu = _menuService.GetById(Convert.ToInt32(reader["MenuId"]));
                            var sessions = menu.Sessions;
                            var menuName = menu.Menu.MenuName;

                            return new
                            {
                                UserId = Convert.ToInt32(reader["UserId"]),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Email = reader["Email"].ToString(),
                                Password = reader["Password"].ToString(),
                                RoleId = Convert.ToInt32(reader["RoleId"]),
                                Menu = new
                                {
                                    MenuId = Convert.ToInt32(reader["MenuId"]),
                                    MenuName = menuName,
                                    Sessions = sessions,
                                },
                                CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                                UpdatedAt = reader["UpdatedAt"] as DateTime?,
                            };
                        }
                    }
                }
            }

            return null;
        }
        //[HttpPost("sign-up")]
        //public Task<IActionResult> SignUp([FromBody] Request request) { }

        //[HttpPost("sign-out")]
        //public Task<IActionResult> SignOut([FromBody] Request request) { }
    }
}
