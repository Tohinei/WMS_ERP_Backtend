using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WMS_ERP_Backend.Models;

namespace WMS_ERP_Backend.DaoProject.Connection
{
    public class DbConnection
    {
        private readonly string _connectionString;

        public DbConnection(IConfiguration configuration)
        {
            var connStr = configuration.GetConnectionString("Connection");
            if (string.IsNullOrWhiteSpace(connStr))
            {
                throw new Exception("Connection is empty");
            }
            _connectionString = connStr;
        }

        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
