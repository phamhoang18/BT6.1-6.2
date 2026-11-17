using System;
using System.Data;
using HRM_Payroll_System.DAL;

namespace HRM_Payroll_System.BUS
{
    public class ChamCongBUS
    {
        private readonly ChamCongDAL _dal = new ChamCongDAL();

        public DataTable GetChamCongByThang(string thangNam)
        {
            return _dal.GetChamCongByThang(thangNam);
        }

        public int LuuChamCong(DataTable dtChamCong)
        {
            // Kiểm tra null trước khi sử dụng
            if (dtChamCong == null)
            {
                throw new Exception("Vui lòng tải dữ liệu chấm công trước khi lưu!");
            }

            // Kiểm tra ràng buộc nghiệp vụ trước khi đẩy xuống DAL
            foreach (DataRow row in dtChamCong.Rows)
            {
                if (row.RowState == DataRowState.Deleted) continue;

                double ngayCong = row["SoNgayCong"] == DBNull.Value ? 0 : Convert.ToDouble(row["SoNgayCong"]);
                double gioOT = row["SoGioOT"] == DBNull.Value ? 0 : Convert.ToDouble(row["SoGioOT"]);

                if (ngayCong < 0 || ngayCong > 31)
                    throw new Exception($"Nhân viên [{row["HoTen"]}]: Số ngày công phải nằm trong khoảng 0 - 31.");
                if (gioOT < 0)
                    throw new Exception($"Nhân viên [{row["HoTen"]}]: Số giờ OT không được âm.");
            }

            return _dal.LuuChamCongDisconnected();
        }
    }
}