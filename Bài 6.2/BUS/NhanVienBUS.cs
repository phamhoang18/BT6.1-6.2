using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using HRM_Payroll_System.DAL;
using HRM_Payroll_System.DTO;

namespace HRM_Payroll_System.BUS
{
    public class NhanVienBUS
    {
        private readonly NhanVienDAL _dal = new NhanVienDAL();

        public List<NhanVienDTO> GetAll() => _dal.GetAll();
        public List<NhanVienDTO> Search(string keyword, int maPB) => _dal.Search(keyword.Trim(), maPB);

        private void Validate(NhanVienDTO nv)
        {
            if (string.IsNullOrWhiteSpace(nv.HoTen))
                throw new Exception("Họ tên nhân viên không được để trống.");
            if (string.IsNullOrWhiteSpace(nv.CCCD) || !Regex.IsMatch(nv.CCCD, @"^\d{9,12}$"))
                throw new Exception("CCCD phải là chuỗi số từ 9 đến 12 ký tự.");
            if (nv.LuongCoBan <= 0)
                throw new Exception("Lương cơ bản phải lớn hơn 0.");
            if (nv.HeSoLuong <= 0)
                throw new Exception("Hệ số lương phải lớn hơn 0.");
            if (nv.MaPB <= 0)
                throw new Exception("Vui lòng chọn phòng ban hợp lệ.");
        }

        public bool Insert(NhanVienDTO nv)
        {
            Validate(nv);
            return _dal.Insert(nv);
        }

        public bool Update(NhanVienDTO nv)
        {
            if (nv.MaNV <= 0) throw new Exception("Chưa chọn nhân viên cần cập nhật.");
            Validate(nv);
            return _dal.Update(nv);
        }

        public bool ChuyenPhongBan(int maNV, int maPBMoi)
        {
            if (maNV <= 0 || maPBMoi <= 0) throw new Exception("Thông tin chuyển phòng ban không hợp lệ.");
            return _dal.ChuyenPhongBan(maNV, maPBMoi);
        }

        public bool Delete(int maNV)
        {
            if (maNV <= 0) throw new Exception("Chưa chọn nhân viên cần xóa.");
            return _dal.Delete(maNV);
        }
    }
}