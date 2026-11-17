using System.Data.SqlClient;

namespace HRM_Payroll_System.DAL
{
    public class DBConnection
    {
        // Thay đổi Server Name phù hợp với máy của bạn (VD: ., .\SQLEXPRESS, localhost)
        private static readonly string connectionString =
            @"Data Source=ADMIN-PC\SQLEXPRESS;Initial Catalog=HRMDB;Integrated Security=True;TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}