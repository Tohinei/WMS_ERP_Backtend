using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WMS_ERP_Backend.DaoProject.Connection;
using WMS_ERP_Backend.DaoProject.IDao;
using WMS_ERP_Backend.Models;

namespace WMS_ERP_Backend.DaoProject.Dao
{
    public class SessionDao : ISessionDao
    {
        private readonly DbConnection _context;

        public SessionDao(DbConnection context)
        {
            _context = context;
        }

        public int Create(Session session)
        {
            string query =
                @"INSERT INTO Session (MenuId, SessionName, Path, Icon, CreatedAt)
                VALUES (@MenuId, @SessionName, @Path, @Icon, @CreatedAt);";

            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MenuId", session.MenuId);
                    command.Parameters.AddWithValue("@SessionName", session.SessionName);
                    command.Parameters.AddWithValue("@Path", session.Path);
                    command.Parameters.AddWithValue("@Icon", session.Icon);
                    command.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public bool Delete(int sessionId)
        {
            string query = "DELETE FROM Session WHERE SessionId = @SessionId";

            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SessionId", sessionId);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public List<Session> GetAll()
        {
            List<Session> Session = new List<Session>();
            string query = "SELECT * FROM Session";

            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Session.Add(
                                new Session
                                {
                                    SessionId = (int)reader["SessionId"],
                                    MenuId = (int)reader["MenuId"],
                                    SessionName = reader["SessionName"].ToString(),
                                    Path = reader["Path"].ToString(),
                                    Icon = reader["Icon"].ToString(),
                                    CreatedAt = (DateTime)reader["CreatedAt"],
                                    UpdatedAt = reader["UpdatedAt"] as DateTime?,
                                }
                            );
                        }
                    }
                }
            }

            return Session;
        }

        public Session GetById(int sessionId)
        {
            Session session = null;
            string query = "SELECT * FROM Session WHERE SessionId = @SessionId";

            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SessionId", sessionId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            session = new Session
                            {
                                SessionId = (int)reader["SessionId"],
                                MenuId = (int)reader["MenuId"],
                                SessionName = reader["SessionName"].ToString(),
                                Path = reader["Path"].ToString(),
                                Icon = reader["Icon"].ToString(),
                                CreatedAt = (DateTime)reader["CreatedAt"],
                                UpdatedAt = reader["UpdatedAt"] as DateTime?,
                            };
                        }
                    }
                }
            }
            return session;
        }

        public bool Update(Session session)
        {
            string query =
                @"UPDATE Session SET MenuId = @MenuId,
                    SessionName = @SessionName,
                    Path = @Path,
                    Icon = @Icon,
                    UpdatedAt = @UpdatedAt
                WHERE SessionId = @SessionId";

            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SessionId", session.SessionId);
                    command.Parameters.AddWithValue("@MenuId", session.MenuId);
                    command.Parameters.AddWithValue("@SessionName", session.SessionName);
                    command.Parameters.AddWithValue("@Path", session.Path);
                    command.Parameters.AddWithValue("@Icon", session.Icon);
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
