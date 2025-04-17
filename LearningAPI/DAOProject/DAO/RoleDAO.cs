using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WMS_ERP_Backend.DaoProject.Connection;
using WMS_ERP_Backend.DaoProject.IDao;
using WMS_ERP_Backend.Models;

namespace WMS_ERP_Backend.DaoProject.Dao
{
    public class RoleDao : IRoleDao
    {
        private readonly DbConnection _context;

        public RoleDao(DbConnection context)
        {
            _context = context;
        }

        public Role GetById(int roleId)
        {
            string query =
                @"SELECT RoleId, RoleName, Description, CreatedAt, UpdatedAt 
                            FROM Role 
                             WHERE RoleId = @RoleId";

            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RoleId", roleId);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Role
                            {
                                RoleId = Convert.ToInt32(reader["RoleId"]),
                                RoleName = reader["RoleName"].ToString(),
                                Description = reader["Description"].ToString(),
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

        public List<Role> GetAll()
        {
            string query =
                @"SELECT RoleId, RoleName, Description, CreatedAt, UpdatedAt 
                             FROM Role";

            var roles = new List<Role>();

            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            roles.Add(
                                new Role
                                {
                                    RoleId = Convert.ToInt32(reader["RoleId"]),
                                    RoleName = reader["RoleName"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                                    UpdatedAt = reader["UpdatedAt"] as DateTime?,
                                }
                            );
                        }
                    }
                }
            }

            return roles;
        }

        public int Create(Role role)
        {
            string query =
                @"INSERT INTO Role (RoleName, Description, CreatedAt)
                             VALUES (@RoleName, @Description, @CreatedAt); 
                             SELECT SCOPE_IDENTITY();";

            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RoleName", role.RoleName);
                    command.Parameters.AddWithValue("@Description", role.Description);
                    command.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public bool Update(Role role)
        {
            string query =
                @"UPDATE Role 
                             SET RoleName = @RoleName, Description = @Description, UpdatedAt = @UpdatedAt
                             WHERE RoleId = @RoleId";

            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RoleName", role.RoleName);
                    command.Parameters.AddWithValue("@Description", role.Description);
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                    command.Parameters.AddWithValue("@RoleId", role.RoleId);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Delete(int roleId)
        {
            string query = @"DELETE FROM Role WHERE RoleId = @RoleId";

            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RoleId", roleId);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
