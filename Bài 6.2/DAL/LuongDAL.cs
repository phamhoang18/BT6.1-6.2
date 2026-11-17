using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HRM_Payroll_System.DTO;

namespace HRM_Payroll_System.DAL
{
    public class LuongDAL
    {
        public List<BangLuongDTO> TinhLuongDuKien(string thangNam)
        {
            List<BangLuongDTO> list = new List<BangLuongDTO>();
            string query = @"
                SELECT nv.MaNV, nv.HoTen, pb.TenPB, cc.ThangNam,
                       nv.LuongCoBan, nv.HeSoLuong, cc.SoNgayCong, cc.SoGioOT,
                       CAST(((nv.LuongCoBan * CAST(nv.HeSoLuong AS DECIMAL(18,4)) / 22.0) * CAST(cc.SoNgayCong AS DECIMAL(18,4)) 
                             + (CAST(cc.SoGioOT AS DECIMAL(18,4)) * 50000)) AS DECIMAL(18,2)) AS ThucLinh
                FROM NhanVien nv
                INNER JOIN PhongBan pb ON nv.MaPB = pb.MaPB
                INNER JOIN BangChamCong cc ON nv.MaNV = cc.MaNV
                WHERE cc.ThangNam = @ThangNam";

            using (SqlConnection conn = DBConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.Add("@ThangNam", SqlDbType.VarChar, 7).Value = thangNam;
                conn.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(new BangLuongDTO
                        {
                            MaNV = (int)r["MaNV"],
                            HoTen = r["HoTen"].ToString(),
                            TenPB = r["TenPB"].ToString(),
                            ThangNam = r["ThangNam"].ToString(),
                            LuongCoBan = Convert.ToDecimal(r["LuongCoBan"]),
                            HeSoLuong = Convert.ToDouble(r["HeSoLuong"]),
                            SoNgayCong = Convert.ToDouble(r["SoNgayCong"]),
                            SoGioOT = Convert.ToDouble(r["SoGioOT"]),
                            ThucLinh = Convert.ToDecimal(r["ThucLinh"])
                        });
                    }
                }
            }
            return list;
        }

        public List<BangLuongDTO> GetBangLuongDaChot(string thangNam)
        {
            List<BangLuongDTO> list = new List<BangLuongDTO>();
            string query = @"
                SELECT bl.MaLuong, bl.MaNV, nv.HoTen, pb.TenPB, bl.ThangNam,
                       nv.LuongCoBan, nv.HeSoLuong, cc.SoNgayCong, cc.SoGioOT, bl.ThucLinh
                FROM BangLuong bl
                INNER JOIN NhanVien nv ON bl.MaNV = nv.MaNV
                INNER JOIN PhongBan pb ON nv.MaPB = pb.MaPB
                INNER JOIN BangChamCong cc ON bl.MaNV = cc.MaNV AND bl.ThangNam = cc.ThangNam
                WHERE bl.ThangNam = @ThangNam";

            using (SqlConnection conn = DBConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.Add("@ThangNam", SqlDbType.VarChar, 7).Value = thangNam;
                conn.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(new BangLuongDTO
                        {
                            MaLuong = (int)r["MaLuong"],
                            MaNV = (int)r["MaNV"],
                            HoTen = r["HoTen"].ToString(),
                            TenPB = r["TenPB"].ToString(),
                            ThangNam = r["ThangNam"].ToString(),
                            LuongCoBan = Convert.ToDecimal(r["LuongCoBan"]),
                            HeSoLuong = Convert.ToDouble(r["HeSoLuong"]),
                            SoNgayCong = Convert.ToDouble(r["SoNgayCong"]),
                            SoGioOT = Convert.ToDouble(r["SoGioOT"]),
                            ThucLinh = Convert.ToDecimal(r["ThucLinh"])
                        });
                    }
                }
            }
            return list;
        }

        public bool ChotLuongThang(string thangNam, List<BangLuongDTO> danhSachLuong)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    string checkQuery = "SELECT COUNT(*) FROM BangLuong WHERE ThangNam = @ThangNam";
                    using (SqlCommand cmdCheck = new SqlCommand(checkQuery, conn, transaction))
                    {
                        cmdCheck.Parameters.Add("@ThangNam", SqlDbType.VarChar, 7).Value = thangNam;
                        int count = (int)cmdCheck.ExecuteScalar();
                        if (count > 0)
                        {
                            throw new Exception($"Bảng lương tháng {thangNam} đã được chốt trước đó!");
                        }
                    }

                    string insertLuongQuery = @"
                        INSERT INTO BangLuong (MaNV, ThangNam, ThucLinh) 
                        VALUES (@MaNV, @ThangNam, @ThucLinh)";

                    foreach (var item in danhSachLuong)
                    {
                        using (SqlCommand cmdInsert = new SqlCommand(insertLuongQuery, conn, transaction))
                        {
                            cmdInsert.Parameters.Add("@MaNV", SqlDbType.Int).Value = item.MaNV;
                            cmdInsert.Parameters.Add("@ThangNam", SqlDbType.VarChar, 7).Value = thangNam;
                            cmdInsert.Parameters.Add("@ThucLinh", SqlDbType.Decimal).Value = item.ThucLinh;
                            cmdInsert.ExecuteNonQuery();
                        }
                    }

                    string lockChamCongQuery = @"
                        UPDATE BangChamCong 
                        SET DaChot = 1 
                        WHERE ThangNam = @ThangNam";

                    using (SqlCommand cmdLock = new SqlCommand(lockChamCongQuery, conn, transaction))
                    {
                        cmdLock.Parameters.Add("@ThangNam", SqlDbType.VarChar, 7).Value = thangNam;
                        cmdLock.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
