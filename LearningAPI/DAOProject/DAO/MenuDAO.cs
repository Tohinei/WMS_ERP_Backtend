using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WMS_ERP_Backend.DaoProject.Connection;
using WMS_ERP_Backend.DaoProject.IDao;
using WMS_ERP_Backend.Models;

namespace WMS_ERP_Backend.DaoProject.IDao
{
    public class MenuDao : IMenuDao
    {
        private readonly DbConnection _context;

        public MenuDao(DbConnection context)
        {
            _context = context;
        }

        public int Create(Menu menu)
        {
            string query =
                @"INSERT INTO Menu (MenuName, Description, CreatedAt)
                VALUES (@MenuName, @Description, @CreatedAt);";

            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MenuName", menu.MenuName);
                    command.Parameters.AddWithValue("@Description", menu.Description);
                    command.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public bool Delete(int menuId)
        {
            string query = "DELETEFROM Menu WHERE MenuId = @MenuId";

            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MenuId", menuId);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public List<Menu> GetAll()
        {
            List<Menu> menus = new List<Menu>();
            string query = "SELECT * FROM Menu";

            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            menus.Add(
                                new Menu
                                {
                                    MenuId = (int)reader["MenuId"],
                                    MenuName = reader["MenuName"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    CreatedAt = (DateTime)reader["CreatedAt"],
                                    UpdatedAt = reader["UpdatedAt"] as DateTime?,
                                }
                            );
                        }
                    }
                }
            }

            return menus;
        }

        public object GetById(int menuId)
        {
            string query =
                @"SELECT m.MenuId, m.MenuName, m.Description, m.CreatedAt, m.UpdatedAt, s.SessionId, s.SessionName, s.Path, s.Icon 
                FROM Menu m
                LEFT JOIN Session s ON m.MenuId = s.MenuId
                WHERE m.MenuId = @MenuId;
";

            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MenuId", menuId);

                    using (var reader = command.ExecuteReader())
                    {
                        Menu menu = null;
                        List<Session> sessions = new List<Session>();

                        while (reader.Read())
                        {
                            if (menu == null)
                            {
                                menu = new Menu
                                {
                                    MenuId = Convert.ToInt32(reader["MenuId"]),
                                    MenuName = reader["MenuName"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                                    UpdatedAt =
                                        reader["UpdatedAt"] != DBNull.Value
                                            ? Convert.ToDateTime(reader["UpdatedAt"])
                                            : (DateTime?)null,
                                };
                            }

                            if (reader["SessionId"] != DBNull.Value)
                            {
                                sessions.Add(
                                    new Session
                                    {
                                        SessionId = Convert.ToInt32(reader["SessionId"]),
                                        SessionName = reader["SessionName"].ToString(),
                                        Path = reader["Path"].ToString(),
                                        Icon = reader["Icon"].ToString(),
                                        MenuId = Convert.ToInt32(reader["MenuId"]),
                                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                                        UpdatedAt =
                                            reader["UpdatedAt"] != DBNull.Value
                                                ? Convert.ToDateTime(reader["UpdatedAt"])
                                                : (DateTime?)null,
                                    }
                                );
                            }
                        }

                        return new { Menu = menu, Sessions = sessions };
                    }
                }
            }
        }

        public bool Update(Menu menu)
        {
            string query =
                @"
                UPDATE Menu
                SET MenuName = @MenuName,
                    Description = @Description,
                    UpdatedAt = @UpdatedAt
                WHERE MenuId = @MenuId";

            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MenuId", menu.MenuId);
                    command.Parameters.AddWithValue("@MenuName", menu.MenuName);
                    command.Parameters.AddWithValue("@Description", menu.Description);
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
