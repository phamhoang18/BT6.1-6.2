using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SalesManagement.BUS;
using SalesManagement.DTO;

namespace SalesManagement.GUI
{
    public partial class FormBanHang : Form
    {
        private readonly SalesBUS bus = new SalesBUS();
        private List<ChiTietHoaDonDTO> gioHang = new List<ChiTietHoaDonDTO>();

        public FormBanHang()
        {
            InitializeComponent();
        }

        private void FormBanHang_Load(object sender, EventArgs e)
        {
            LoadDanhSachSanPham();
        }

        private void LoadDanhSachSanPham()
        {
            var list = bus.LayDanhSachSanPham();
            dgvSanPhamChon.DataSource = null;
            dgvSanPhamChon.DataSource = list;

            dgvQLSanPham.DataSource = null;
            dgvQLSanPham.DataSource = list;
        }

        private void btnTimSP_Click(object sender, EventArgs e)
        {
            dgvSanPhamChon.DataSource = bus.TimKiemSanPham(txtTimSP.Text.Trim());
        }

        private void btnThemGio_Click(object sender, EventArgs e)
        {
            if (dgvSanPhamChon.CurrentRow == null) return;
            var sp = (SanPhamDTO)dgvSanPhamChon.CurrentRow.DataBoundItem;
            int slMua = (int)numSoLuong.Value;

            var itemTonTai = gioHang.FirstOrDefault(x => x.MaSP == sp.MaSP);
            if (itemTonTai != null)
            {
                itemTonTai.SoLuong += slMua;
            }
            else
            {
                gioHang.Add(new ChiTietHoaDonDTO
                {
                    MaSP = sp.MaSP,
                    TenSP = sp.TenSP,
                    DonGia = sp.GiaBan,
                    SoLuong = slMua
                });
            }
            CapNhatGioHangUI();
        }

        private void CapNhatGioHangUI()
        {
            dgvGioHang.DataSource = null;
            dgvGioHang.DataSource = gioHang;

            decimal tong = gioHang.Sum(x => x.ThanhTien);
            int diem = (int)(tong / 100000);
            lblTongTien.Text = $"Tổng tiền: {tong:N0} VNĐ";
            lblDiemCong.Text = $"Điểm tích lũy cộng thêm: {diem}";
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            int? maKH = null;
            if (!string.IsNullOrWhiteSpace(txtMaKH.Text))
            {
                if (int.TryParse(txtMaKH.Text.Trim(), out int parsedKH))
                    maKH = parsedKH;
                else
                {
                    MessageBox.Show("Mã khách hàng phải là số nguyên!");
                    return;
                }
            }

            HoaDonDTO hd = new HoaDonDTO
            {
                MaNV = 1, 
                MaKH = maKH,
                TongTien = gioHang.Sum(x => x.ThanhTien)
            };

            if (bus.ThanhToan(hd, gioHang, out string msg))
            {
                MessageBox.Show("Thanh toán thành công! Đã cập nhật kho và điểm tích lũy.", "Thông báo");
                gioHang.Clear();
                CapNhatGioHangUI();
                LoadDanhSachSanPham();
            }
            else
            {
                MessageBox.Show("Giao dịch thất bại (Đã Rollback):\n" + msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThemSP_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSP.Text) ||
                string.IsNullOrWhiteSpace(txtTenSP.Text) ||
                string.IsNullOrWhiteSpace(txtGiaBan.Text) ||
                string.IsNullOrWhiteSpace(txtTonKho.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin sản phẩm (Mã SP, Tên SP, Giá bán, Tồn kho)!",
                                "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }

            try
            {
                var sp = new SanPhamDTO
                {
                    MaSP = txtMaSP.Text.Trim(),
                    TenSP = txtTenSP.Text.Trim(),
                    GiaBan = decimal.Parse(txtGiaBan.Text),
                    SoLuongTon = int.Parse(txtTonKho.Text)
                };

                if (bus.ThemSanPham(sp))
                {
                    MessageBox.Show("Thêm sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachSanPham();

                    txtMaSP.Clear();
                    txtTenSP.Clear();
                    txtGiaBan.Clear();
                    txtTonKho.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi định dạng dữ liệu (Giá bán hoặc Tồn kho phải là số):\n" + ex.Message,
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSuaSP_Click(object sender, EventArgs e)
        {
            var sp = new SanPhamDTO
            {
                MaSP = txtMaSP.Text.Trim(),
                TenSP = txtTenSP.Text.Trim(),
                GiaBan = decimal.Parse(txtGiaBan.Text),
                SoLuongTon = int.Parse(txtTonKho.Text)
            };
            if (bus.SuaSanPham(sp))
            {
                MessageBox.Show("Cập nhật thành công!");
                LoadDanhSachSanPham();
            }
        }

        private void btnXoaSP_Click(object sender, EventArgs e)
        {
            if (bus.XoaSanPham(txtMaSP.Text.Trim()))
            {
                MessageBox.Show("Xóa thành công!");
                LoadDanhSachSanPham();
            }
        }

        private void dgvQLSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var sp = (SanPhamDTO)dgvQLSanPham.Rows[e.RowIndex].DataBoundItem;
                txtMaSP.Text = sp.MaSP;
                txtTenSP.Text = sp.TenSP;
                txtGiaBan.Text = sp.GiaBan.ToString();
                txtTonKho.Text = sp.SoLuongTon.ToString();
            }
        }

        private void btnXemBaoCao_Click(object sender, EventArgs e)
        {
            decimal dt = bus.LayDoanhThuHomNay();
            lblDoanhThu.Text = $"Doanh thu hôm nay: {dt:N0} VNĐ";
            dgvTop5.DataSource = bus.LayTop5BanChay();
        }
    }
}