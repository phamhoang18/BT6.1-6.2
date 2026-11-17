using System;

namespace HRM_Payroll_System.DTO
{
    public class NhanVienDTO
    {
        public int MaNV { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public string CCCD { get; set; }
        public int MaPB { get; set; }
        public string TenPB { get; set; } // Dùng để hiển thị trên lưới
        public decimal LuongCoBan { get; set; }
        public double HeSoLuong { get; set; }
    }
}