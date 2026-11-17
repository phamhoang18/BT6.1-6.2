using System.Collections.Generic;
using System.Data.SqlClient;
using HRM_Payroll_System.DTO;

namespace HRM_Payroll_System.DAL
{
    public class PhongBanDAL
    {
        public List<PhongBanDTO> GetAll()
        {
            List<PhongBanDTO> list = new List<PhongBanDTO>();
            string query = "SELECT MaPB, TenPB, Mota FROM PhongBan";

            using (SqlConnection conn = DBConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PhongBanDTO
                        {
                            MaPB = (int)reader["MaPB"],
                            TenPB = reader["TenPB"].ToString(),
                            Mota = reader["Mota"]?.ToString()
                        });
                    }
                }
            }
            return list;
        }
    }
}