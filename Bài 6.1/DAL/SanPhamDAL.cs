using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using SalesManagement.DTO;

namespace SalesManagement.DAL
{
    public class SanPhamDAL
    {
        public List<SanPhamDTO> GetAll()
        {
            var list = new List<SanPhamDTO>();
            using (SqlConnection conn = DBConnect.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM SanPham", conn);
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(new SanPhamDTO
                        {
                            MaSP = r["MaSP"].ToString(),
                            TenSP = r["TenSP"].ToString(),
                            GiaBan = (decimal)r["GiaBan"],
                            SoLuongTon = (int)r["SoLuongTon"]
                        });
                    }
                }
            }
            return list;
        }

        public List<SanPhamDTO> Search(string keyword)
        {
            var list = new List<SanPhamDTO>();
            using (SqlConnection conn = DBConnect.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM SanPham WHERE MaSP LIKE @kw OR TenSP LIKE @kw";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(new SanPhamDTO
                        {
                            MaSP = r["MaSP"].ToString(),
                            TenSP = r["TenSP"].ToString(),
                            GiaBan = (decimal)r["GiaBan"],
                            SoLuongTon = (int)r["SoLuongTon"]
                        });
                    }
                }
            }
            return list;
        }

        public bool Insert(SanPhamDTO sp)
        {
            using (SqlConnection conn = DBConnect.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO SanPham VALUES (@ma, @ten, @gia, @ton)", conn);
                cmd.Parameters.AddWithValue("@ma", sp.MaSP);
                cmd.Parameters.AddWithValue("@ten", sp.TenSP);
                cmd.Parameters.AddWithValue("@gia", sp.GiaBan);
                cmd.Parameters.AddWithValue("@ton", sp.SoLuongTon);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Update(SanPhamDTO sp)
        {
            using (SqlConnection conn = DBConnect.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE SanPham SET TenSP=@ten, GiaBan=@gia, SoLuongTon=@ton WHERE MaSP=@ma", conn);
                cmd.Parameters.AddWithValue("@ma", sp.MaSP);
                cmd.Parameters.AddWithValue("@ten", sp.TenSP);
                cmd.Parameters.AddWithValue("@gia", sp.GiaBan);
                cmd.Parameters.AddWithValue("@ton", sp.SoLuongTon);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(string maSP)
        {
            using (SqlConnection conn = DBConnect.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM SanPham WHERE MaSP=@ma", conn);
                cmd.Parameters.AddWithValue("@ma", maSP);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}