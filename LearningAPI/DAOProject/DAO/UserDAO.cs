using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WMS_ERP_Backend.DaoProject.Connection;
using WMS_ERP_Backend.DaoProject.IDao;
using WMS_ERP_Backend.DaoProject.Services;
using WMS_ERP_Backend.Models;

namespace WMS_ERP_Backend.DaoProject.Dao
{
    public class UserDao : IUserDao
    {
        private readonly DbConnection _context;
        private readonly JwtService _jwtService;

        public UserDao(DbConnection context, JwtService jwtService)
        {
            _context = context;
            _jwtService = new JwtService(
                "3fd133c1259bb53e43826ce7758897e6bafb804fb93b89f2e553796d7af58c99"
            );
        }

        public User GetById(int userId)
        {
            string query =
                @"SELECT UserId, FirstName, LastName, Email, Password, RoleId, MenuId, CreatedAt, UpdatedAt
                             FROM [User]
                             WHERE UserId = @UserId";

            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                UserId = Convert.ToInt32(reader["UserId"]),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Email = reader["Email"].ToString(),
                                Password = reader["Password"].ToString(),
                                RoleId = Convert.ToInt32(reader["RoleId"]),
                                MenuId = Convert.ToInt32(reader["MenuId"]),
                                CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                                UpdatedAt = reader["UpdatedAt"] as DateTime?,
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public List<User> GetAll()
        {
            string query =
                @"SELECT UserId, FirstName, LastName, Email, Password, RoleId, MenuId, CreatedAt, UpdatedAt
                             FROM [User]";

            var users = new List<User>();

            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(
                                new User
                                {
                                    UserId = Convert.ToInt32(reader["UserId"]),
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    Password = reader["Password"].ToString(),
                                    RoleId = Convert.ToInt32(reader["RoleId"]),
                                    MenuId = Convert.ToInt32(reader["MenuId"]),
                                    CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                                    UpdatedAt = reader["UpdatedAt"] as DateTime?,
                                }
                            );
                        }
                    }
                }
            }

            return users;
        }

        public int Create(User user)
        {
            string hashedPassword = _jwtService.HashPassword(user.Password);

            string query =
                @"INSERT INTO [User] (FirstName, LastName, Email, Password, RoleId, MenuId, CreatedAt)
                             VALUES (@FirstName, @LastName, @Email, @Password, @RoleId, @MenuId, @CreatedAt);
                             SELECT SCOPE_IDENTITY();";

            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", user.FirstName);
                    command.Parameters.AddWithValue("@LastName", user.LastName);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@Password", hashedPassword);
                    command.Parameters.AddWithValue("@RoleId", user.RoleId);
                    command.Parameters.AddWithValue("@MenuId", user.MenuId);
                    command.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public bool Update(User user)
        {
            string query =
                @"UPDATE [User]
                             SET FirstName = @FirstName, LastName = @LastName, Email = @Email,
                                 RoleId = @RoleId, MenuId = @MenuId, UpdatedAt = @UpdatedAt
                             WHERE UserId = @UserId";

            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", user.FirstName);
                    command.Parameters.AddWithValue("@LastName", user.LastName);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@RoleId", user.RoleId);
                    command.Parameters.AddWithValue("@MenuId", user.MenuId);
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                    command.Parameters.AddWithValue("@UserId", user.UserId);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Delete(int userId)
        {
            string query = @"DELETE FROM [User] WHERE UserId = @UserId";

            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool DeleteMany(List<int> usersIds)
        {
            string query = @"DELETE FROM [User] WHERE UserId = @UserId";

            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                foreach (var user in usersIds)
                {
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", user);
                        command.ExecuteNonQuery();
                    }
                }
            }

            return true;
        }
    }
}
