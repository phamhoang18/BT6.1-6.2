using System;
using System.Collections.Generic;
using HRM_Payroll_System.DAL;
using HRM_Payroll_System.DTO;

namespace HRM_Payroll_System.BUS
{
    public class LuongBUS
    {
        private readonly LuongDAL _dal = new LuongDAL();

        public decimal TinhThucLinh(decimal luongCoBan, double heSoLuong, double soNgayCong, double soGioOT)
        {
            decimal luongNgay = (luongCoBan * (decimal)heSoLuong) / 22m;
            decimal luongChinh = luongNgay * (decimal)soNgayCong;
            decimal luongOT = (decimal)soGioOT * 50000m;
            return Math.Round(luongChinh + luongOT, 2);
        }

        public List<BangLuongDTO> TinhLuongDuKien(string thangNam)
        {
            var list = _dal.TinhLuongDuKien(thangNam);
            foreach (var item in list)
            {
                item.ThucLinh = TinhThucLinh(item.LuongCoBan, item.HeSoLuong, item.SoNgayCong, item.SoGioOT);
            }
            return list;
        }

        public List<BangLuongDTO> GetBangLuongDaChot(string thangNam)
        {
            return _dal.GetBangLuongDaChot(thangNam);
        }

        public bool ChotLuongThang(string thangNam)
        {
            var danhSach = TinhLuongDuKien(thangNam);
            if (danhSach.Count == 0)
                throw new Exception($"Không có dữ liệu chấm công cho tháng {thangNam} để chốt lương.");

            return _dal.ChotLuongThang(thangNam, danhSach);
        }
    }
}