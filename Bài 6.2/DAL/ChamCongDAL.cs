using System;
using System.Data;
using System.Data.SqlClient;

namespace HRM_Payroll_System.DAL
{
    public class ChamCongDAL
    {
        private SqlDataAdapter _adapter;
        private SqlCommandBuilder _cmdBuilder;
        private DataTable _dtChamCong;

        public void KhoiTaoChamCongThang(string thangNam)
        {
            string query = @"
                INSERT INTO BangChamCong (MaNV, ThangNam, SoNgayCong, SoGioOT, DaChot)
                SELECT MaNV, @ThangNam, 0, 0, 0
                FROM NhanVien
                WHERE MaNV NOT IN (SELECT MaNV FROM BangChamCong WHERE ThangNam = @ThangNam)";

            using (SqlConnection conn = DBConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.Add("@ThangNam", SqlDbType.VarChar, 7).Value = thangNam;
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public DataTable GetChamCongByThang(string thangNam)
        {
            KhoiTaoChamCongThang(thangNam);

            string selectQuery = @"
                SELECT MaCong, MaNV, ThangNam, SoNgayCong, SoGioOT, DaChot 
                FROM BangChamCong 
                WHERE ThangNam = @ThangNam";

            SqlConnection conn = DBConnection.GetConnection();
            SqlCommand selectCmd = new SqlCommand(selectQuery, conn);
            selectCmd.Parameters.Add("@ThangNam", SqlDbType.VarChar, 7).Value = thangNam;

            _adapter = new SqlDataAdapter(selectCmd);
            _cmdBuilder = new SqlCommandBuilder(_adapter); 

            _dtChamCong = new DataTable();
            _adapter.Fill(_dtChamCong);

            _dtChamCong.Columns.Add("HoTen", typeof(string));
            using (SqlCommand cmdName = new SqlCommand("SELECT MaNV, HoTen FROM NhanVien", conn))
            {
                conn.Open();
                using (SqlDataReader r = cmdName.ExecuteReader())
                {
                    while (r.Read())
                    {
                        int maNV = (int)r["MaNV"];
                        string hoTen = r["HoTen"].ToString();
                        foreach (DataRow row in _dtChamCong.Rows)
                        {
                            if ((int)row["MaNV"] == maNV)
                                row["HoTen"] = hoTen;
                        }
                    }
                }
                conn.Close();
            }

            _dtChamCong.AcceptChanges();
            return _dtChamCong;
        }

        public int LuuChamCongDisconnected()
        {
            if (_adapter == null || _dtChamCong == null) return 0;

            // Đảm bảo Connection của adapter được mở/đóng an toàn
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                _adapter.SelectCommand.Connection = conn;
                _cmdBuilder.RefreshSchema();
                return _adapter.Update(_dtChamCong);
            }
        }
    }
}
