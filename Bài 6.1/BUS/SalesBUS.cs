using System.Collections.Generic;
using System.Data;
using SalesManagement.DAL;
using SalesManagement.DTO;

namespace SalesManagement.BUS
{
    public class SalesBUS
    {
        private readonly SanPhamDAL spDAL = new SanPhamDAL();
        private readonly HoaDonDAL hdDAL = new HoaDonDAL();

        public List<SanPhamDTO> LayDanhSachSanPham() => spDAL.GetAll();
        public List<SanPhamDTO> TimKiemSanPham(string kw) => spDAL.Search(kw);
        public bool ThemSanPham(SanPhamDTO sp) => spDAL.Insert(sp);
        public bool SuaSanPham(SanPhamDTO sp) => spDAL.Update(sp);
        public bool XoaSanPham(string maSP) => spDAL.Delete(maSP);

        public bool ThanhToan(HoaDonDTO hd, List<ChiTietHoaDonDTO> gioHang, out string msg)
        {
            if (gioHang == null || gioHang.Count == 0)
            {
                msg = "Giỏ hàng đang trống!";
                return false;
            }
            return hdDAL.ThanhToanTransaction(hd, gioHang, out msg);
        }

        public decimal LayDoanhThuHomNay() => hdDAL.GetDoanhThuHomNay();
        public DataTable LayTop5BanChay() => hdDAL.GetTop5SanPham();
    }
}