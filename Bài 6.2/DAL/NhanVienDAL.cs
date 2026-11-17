using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HRM_Payroll_System.DTO;

namespace HRM_Payroll_System.DAL
{
    public class NhanVienDAL
    {
        public List<NhanVienDTO> GetAll()
        {
            return Search("", 0);
        }

        public List<NhanVienDTO> Search(string keyword, int maPB)
        {
            List<NhanVienDTO> list = new List<NhanVienDTO>();
            string query = @"
                SELECT nv.MaNV, nv.HoTen, nv.NgaySinh, nv.CCCD, nv.MaPB, pb.TenPB, nv.LuongCoBan, nv.HeSoLuong
                FROM NhanVien nv
                INNER JOIN PhongBan pb ON nv.MaPB = pb.MaPB
                WHERE (@Keyword = '' OR nv.HoTen LIKE @KeywordPattern OR nv.CCCD LIKE @KeywordPattern)
                  AND (@MaPB = 0 OR nv.MaPB = @MaPB)";

            using (SqlConnection conn = DBConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.Add("@Keyword", SqlDbType.NVarChar, 100).Value = keyword ?? "";
                cmd.Parameters.Add("@KeywordPattern", SqlDbType.NVarChar, 105).Value = $"%{keyword ?? ""}%";
                cmd.Parameters.Add("@MaPB", SqlDbType.Int).Value = maPB;

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new NhanVienDTO
                        {
                            MaNV = (int)reader["MaNV"],
                            HoTen = reader["HoTen"].ToString(),
                            NgaySinh = Convert.ToDateTime(reader["NgaySinh"]),
                            CCCD = reader["CCCD"].ToString(),
                            MaPB = (int)reader["MaPB"],
                            TenPB = reader["TenPB"].ToString(),
                            LuongCoBan = Convert.ToDecimal(reader["LuongCoBan"]),
                            HeSoLuong = Convert.ToDouble(reader["HeSoLuong"])
                        });
                    }
                }
            }
            return list;
        }

        public bool Insert(NhanVienDTO nv)
        {
            string query = @"INSERT INTO NhanVien (HoTen, NgaySinh, CCCD, MaPB, LuongCoBan, HeSoLuong) 
                             VALUES (@HoTen, @NgaySinh, @CCCD, @MaPB, @LuongCoBan, @HeSoLuong)";

            using (SqlConnection conn = DBConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.Add("@HoTen", SqlDbType.NVarChar, 100).Value = nv.HoTen;
                cmd.Parameters.Add("@NgaySinh", SqlDbType.DateTime).Value = nv.NgaySinh;
                cmd.Parameters.Add("@CCCD", SqlDbType.VarChar, 20).Value = nv.CCCD;
                cmd.Parameters.Add("@MaPB", SqlDbType.Int).Value = nv.MaPB;
                cmd.Parameters.Add("@LuongCoBan", SqlDbType.Decimal).Value = nv.LuongCoBan;
                cmd.Parameters.Add("@HeSoLuong", SqlDbType.Float).Value = nv.HeSoLuong;

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Update(NhanVienDTO nv)
        {
            string query = @"UPDATE NhanVien 
                             SET HoTen = @HoTen, NgaySinh = @NgaySinh, CCCD = @CCCD, 
                                 MaPB = @MaPB, LuongCoBan = @LuongCoBan, HeSoLuong = @HeSoLuong 
                             WHERE MaNV = @MaNV";

            using (SqlConnection conn = DBConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.Add("@MaNV", SqlDbType.Int).Value = nv.MaNV;
                cmd.Parameters.Add("@HoTen", SqlDbType.NVarChar, 100).Value = nv.HoTen;
                cmd.Parameters.Add("@NgaySinh", SqlDbType.DateTime).Value = nv.NgaySinh;
                cmd.Parameters.Add("@CCCD", SqlDbType.VarChar, 20).Value = nv.CCCD;
                cmd.Parameters.Add("@MaPB", SqlDbType.Int).Value = nv.MaPB;
                cmd.Parameters.Add("@LuongCoBan", SqlDbType.Decimal).Value = nv.LuongCoBan;
                cmd.Parameters.Add("@HeSoLuong", SqlDbType.Float).Value = nv.HeSoLuong;

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool ChuyenPhongBan(int maNV, int maPBMoi)
        {
            string query = "UPDATE NhanVien SET MaPB = @MaPB WHERE MaNV = @MaNV";
            using (SqlConnection conn = DBConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.Add("@MaNV", SqlDbType.Int).Value = maNV;
                cmd.Parameters.Add("@MaPB", SqlDbType.Int).Value = maPBMoi;

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(int maNV)
        {
            string query = "DELETE FROM NhanVien WHERE MaNV = @MaNV";
            using (SqlConnection conn = DBConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.Add("@MaNV", SqlDbType.Int).Value = maNV;
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}