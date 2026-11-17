using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using SalesManagement.DTO;

namespace SalesManagement.DAL
{
    public class HoaDonDAL
    {
        public bool ThanhToanTransaction(HoaDonDTO hd, List<ChiTietHoaDonDTO> chiTiets, out string errorMsg)
        {
            errorMsg = string.Empty;
            using (SqlConnection conn = DBConnect.GetConnection())
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    string sqlHD = @"INSERT INTO HoaDon (NgayLap, MaNV, MaKH, TongTien) 
                                     OUTPUT INSERTED.MaHD 
                                     VALUES (GETDATE(), @maNV, @maKH, @tongTien)";
                    SqlCommand cmdHD = new SqlCommand(sqlHD, conn, trans);
                    cmdHD.Parameters.AddWithValue("@maNV", hd.MaNV);
                    cmdHD.Parameters.AddWithValue("@maKH", (object)hd.MaKH ?? DBNull.Value);
                    cmdHD.Parameters.AddWithValue("@tongTien", hd.TongTien);

                    int maHDMoi = (int)cmdHD.ExecuteScalar();

                    foreach (var item in chiTiets)
                    {
                        SqlCommand cmdCheck = new SqlCommand("SELECT SoLuongTon FROM SanPham WHERE MaSP = @maSP", conn, trans);
                        cmdCheck.Parameters.AddWithValue("@maSP", item.MaSP);
                        int tonHienTai = (int)cmdCheck.ExecuteScalar();

                        if (tonHienTai < item.SoLuong)
                        {
                            throw new Exception($"Sản phẩm '{item.TenSP}' không đủ tồn kho (Còn: {tonHienTai}, Mua: {item.SoLuong}).");
                        }

                        string sqlCT = "INSERT INTO ChiTietHoaDon (MaHD, MaSP, SoLuong, DonGia) VALUES (@maHD, @maSP, @sl, @dg)";
                        SqlCommand cmdCT = new SqlCommand(sqlCT, conn, trans);
                        cmdCT.Parameters.AddWithValue("@maHD", maHDMoi);
                        cmdCT.Parameters.AddWithValue("@maSP", item.MaSP);
                        cmdCT.Parameters.AddWithValue("@sl", item.SoLuong);
                        cmdCT.Parameters.AddWithValue("@dg", item.DonGia);
                        cmdCT.ExecuteNonQuery();

                        string sqlTruKho = "UPDATE SanPham SET SoLuongTon = SoLuongTon - @sl WHERE MaSP = @maSP";
                        SqlCommand cmdKho = new SqlCommand(sqlTruKho, conn, trans);
                        cmdKho.Parameters.AddWithValue("@sl", item.SoLuong);
                        cmdKho.Parameters.AddWithValue("@maSP", item.MaSP);
                        cmdKho.ExecuteNonQuery();
                    }

                    if (hd.MaKH.HasValue)
                    {
                        int diemCong = (int)(hd.TongTien / 100000);
                        if (diemCong > 0)
                        {
                            string sqlDiem = "UPDATE KhachHang SET DiemTichLuy = DiemTichLuy + @diem WHERE MaKH = @maKH";
                            SqlCommand cmdDiem = new SqlCommand(sqlDiem, conn, trans);
                            cmdDiem.Parameters.AddWithValue("@diem", diemCong);
                            cmdDiem.Parameters.AddWithValue("@maKH", hd.MaKH.Value);
                            cmdDiem.ExecuteNonQuery();
                        }
                    }

                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    errorMsg = ex.Message;
                    return false;
                }
            }
        }

        public decimal GetDoanhThuHomNay()
        {
            using (SqlConnection conn = DBConnect.GetConnection())
            {
                conn.Open();
                string sql = "SELECT ISNULL(SUM(TongTien), 0) FROM HoaDon WHERE CAST(NgayLap AS DATE) = CAST(GETDATE() AS DATE)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                return Convert.ToDecimal(cmd.ExecuteScalar());
            }
        }

        public DataTable GetTop5SanPham()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaSP");
            dt.Columns.Add("TenSP");
            dt.Columns.Add("TongSoLuongBan", typeof(int));

            using (SqlConnection conn = DBConnect.GetConnection())
            {
                conn.Open();
                string sql = @"SELECT TOP 5 sp.MaSP, sp.TenSP, SUM(ct.SoLuong) AS TongBan
                               FROM ChiTietHoaDon ct
                               JOIN SanPham sp ON ct.MaSP = sp.MaSP
                               GROUP BY sp.MaSP, sp.TenSP
                               ORDER BY TongBan DESC";
                SqlCommand cmd = new SqlCommand(sql, conn);
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        dt.Rows.Add(r["MaSP"], r["TenSP"], r["TongBan"]);
                    }
                }
            }
            return dt;
        }
    }
}