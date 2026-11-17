using Microsoft.Data.SqlClient;

namespace SalesManagement.DAL
{
    public class DBConnect
    {
        private static readonly string connStr = @"Data Source=ADMIN-PC\SQLEXPRESS;Initial Catalog=SalesDB;Integrated Security=True;TrustServerCertificate=True";
        public static SqlConnection GetConnection() => new SqlConnection(connStr);
    }
}