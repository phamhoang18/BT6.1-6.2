using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HRM_Payroll_System.BUS;
using HRM_Payroll_System.DTO;

namespace HRM_Payroll_System.GUI
{
    public partial class frmMain : Form
    {
        private readonly PhongBanBUS _phongBanBUS = new PhongBanBUS();
        private readonly NhanVienBUS _nhanVienBUS = new NhanVienBUS();
        private readonly ChamCongBUS _chamCongBUS = new ChamCongBUS();
        private readonly LuongBUS _luongBUS = new LuongBUS();

        private DataTable _dtChamCongHienTai;

        public frmMain()
        {
            InitializeComponent();
            RegisterEvents();
        }

        private void RegisterEvents()
        {
            this.Load += FrmMain_Load;

            // Tab 1: Nhân sự
            btnThem.Click += BtnThem_Click;
            btnSua.Click += BtnSua_Click;
            btnChuyenPB.Click += BtnChuyenPB_Click;
            btnXoa.Click += BtnXoa_Click;
            btnLamMoi.Click += (s, e) => ClearFormNV();
            btnTimKiem.Click += BtnTimKiem_Click;
            dgvNhanVien.CellClick += DgvNhanVien_CellClick;

            // Tab 2: Chấm công (Disconnected Model)
            btnTaiChamCong.Click += BtnTaiChamCong_Click;
            btnLuuChamCong.Click += BtnLuuChamCong_Click;

            // Tab 3: Tính lương & Chốt lương (SqlTransaction)
            btnTinhLuong.Click += BtnTinhLuong_Click;
            btnChotLuong.Click += BtnChotLuong_Click;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            LoadPhongBan();
            LoadDanhSachNhanVien();
            LoadChamCong();
            LoadBangLuong();
        }

        #region 1. QUẢN LÝ HỒ SƠ NHÂN SỰ (3-TIER CRUD)

        private void LoadPhongBan()
        {
            var listPB = _phongBanBUS.GetAll();

            // ComboBox nhập liệu
            cboPhongBan.DataSource = new List<PhongBanDTO>(listPB);
            cboPhongBan.DisplayMember = "TenPB";
            cboPhongBan.ValueMember = "MaPB";

            // ComboBox tìm kiếm (thêm lựa chọn "Tất cả phòng ban")
            var listSearch = new List<PhongBanDTO> { new PhongBanDTO { MaPB = 0, TenPB = "-- Tất cả phòng ban --" } };
            listSearch.AddRange(listPB);
            cboSearchPB.DataSource = listSearch;
            cboSearchPB.DisplayMember = "TenPB";
            cboSearchPB.ValueMember = "MaPB";
        }

        private void LoadDanhSachNhanVien()
        {
            dgvNhanVien.DataSource = _nhanVienBUS.GetAll();
            FormatGridNhanVien();
        }

        private void FormatGridNhanVien()
        {
            if (dgvNhanVien.Columns["MaPB"] != null) dgvNhanVien.Columns["MaPB"].Visible = false;
            if (dgvNhanVien.Columns["MaNV"] != null) dgvNhanVien.Columns["MaNV"].HeaderText = "Mã NV";
            if (dgvNhanVien.Columns["HoTen"] != null) dgvNhanVien.Columns["HoTen"].HeaderText = "Họ và Tên";
            if (dgvNhanVien.Columns["NgaySinh"] != null)
            {
                dgvNhanVien.Columns["NgaySinh"].HeaderText = "Ngày sinh";
                dgvNhanVien.Columns["NgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
            if (dgvNhanVien.Columns["TenPB"] != null) dgvNhanVien.Columns["TenPB"].HeaderText = "Phòng ban";
            if (dgvNhanVien.Columns["LuongCoBan"] != null)
            {
                dgvNhanVien.Columns["LuongCoBan"].HeaderText = "Lương cơ bản";
                dgvNhanVien.Columns["LuongCoBan"].DefaultCellStyle.Format = "N0";
            }
            if (dgvNhanVien.Columns["HeSoLuong"] != null) dgvNhanVien.Columns["HeSoLuong"].HeaderText = "Hệ số";
        }

        private NhanVienDTO GetNhanVienFromForm()
        {
            if (!decimal.TryParse(txtLuongCB.Text.Trim(), out decimal luongCB))
                throw new Exception("Lương cơ bản phải là số hợp lệ.");
            if (!double.TryParse(txtHeSo.Text.Trim(), out double heSo))
                throw new Exception("Hệ số lương phải là số hợp lệ.");

            return new NhanVienDTO
            {
                MaNV = string.IsNullOrEmpty(txtMaNV.Text) ? 0 : int.Parse(txtMaNV.Text),
                HoTen = txtHoTen.Text.Trim(),
                NgaySinh = dtpNgaySinh.Value.Date,
                CCCD = txtCCCD.Text.Trim(),
                MaPB = (int)cboPhongBan.SelectedValue,
                LuongCoBan = luongCB,
                HeSoLuong = heSo
            };
        }

        private void ClearFormNV()
        {
            txtMaNV.Clear();
            txtHoTen.Clear();
            txtCCCD.Clear();
            txtLuongCB.Clear();
            txtHeSo.Text = "1.0";
            dtpNgaySinh.Value = DateTime.Now.AddYears(-22);
            if (cboPhongBan.Items.Count > 0) cboPhongBan.SelectedIndex = 0;
        }

        private void DgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvNhanVien.Rows[e.RowIndex];
            txtMaNV.Text = row.Cells["MaNV"].Value.ToString();
            txtHoTen.Text = row.Cells["HoTen"].Value.ToString();
            dtpNgaySinh.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value);
            txtCCCD.Text = row.Cells["CCCD"].Value.ToString();
            cboPhongBan.SelectedValue = (int)row.Cells["MaPB"].Value;
            txtLuongCB.Text = Convert.ToDecimal(row.Cells["LuongCoBan"].Value).ToString("0");
            txtHeSo.Text = row.Cells["HeSoLuong"].Value.ToString();
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            try
            {
                var nv = GetNhanVienFromForm();
                if (_nhanVienBUS.Insert(nv))
                {
                    MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachNhanVien();
                    ClearFormNV();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            try
            {
                var nv = GetNhanVienFromForm();
                if (_nhanVienBUS.Update(nv))
                {
                    MessageBox.Show("Cập nhật thông tin nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachNhanVien();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnChuyenPB_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMaNV.Text))
                    throw new Exception("Vui lòng chọn nhân viên trên danh sách trước khi chuyển phòng ban.");

                int maNV = int.Parse(txtMaNV.Text);
                int maPBMoi = (int)cboPhongBan.SelectedValue;
                string tenPBMoi = cboPhongBan.Text;

                if (_nhanVienBUS.ChuyenPhongBan(maNV, maPBMoi))
                {
                    MessageBox.Show($"Đã điều chuyển nhân viên sang [{tenPBMoi}]!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachNhanVien();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMaNV.Text))
                    throw new Exception("Vui lòng chọn nhân viên cần xóa.");

                int maNV = int.Parse(txtMaNV.Text);
                if (MessageBox.Show("Bạn có chắc muốn xóa nhân viên này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (_nhanVienBUS.Delete(maNV))
                    {
                        MessageBox.Show("Xóa thành công!", "Thông báo");
                        LoadDanhSachNhanVien();
                        ClearFormNV();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể xóa nhân viên (có thể đã phát sinh dữ liệu chấm công/lương): " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtSearchKey.Text;
            int maPB = (int)cboSearchPB.SelectedValue;
            dgvNhanVien.DataSource = _nhanVienBUS.Search(keyword, maPB);
            FormatGridNhanVien();
        }

        #endregion

        #region 2. CHẤM CÔNG NGẮT KẾT NỐI (DISCONNECTED MODEL)

        private void BtnTaiChamCong_Click(object sender, EventArgs e) => LoadChamCong();

        private void LoadChamCong()
        {
            try
            {
                string thangNam = dtpThangCC.Value.ToString("MM/yyyy");
                _dtChamCongHienTai = _chamCongBUS.GetChamCongByThang(thangNam);
                dgvChamCong.DataSource = _dtChamCongHienTai;

                // Cấu hình hiển thị & quyền chỉnh sửa trên DataGridView
                dgvChamCong.Columns["MaCong"].Visible = false;
                dgvChamCong.Columns["DaChot"].Visible = false;

                dgvChamCong.Columns["MaNV"].HeaderText = "Mã NV";
                dgvChamCong.Columns["MaNV"].ReadOnly = true;
                dgvChamCong.Columns["MaNV"].DisplayIndex = 0;

                dgvChamCong.Columns["HoTen"].HeaderText = "Họ và Tên";
                dgvChamCong.Columns["HoTen"].ReadOnly = true;
                dgvChamCong.Columns["HoTen"].DisplayIndex = 1;

                dgvChamCong.Columns["ThangNam"].HeaderText = "Tháng/Năm";
                dgvChamCong.Columns["ThangNam"].ReadOnly = true;
                dgvChamCong.Columns["ThangNam"].DisplayIndex = 2;

                dgvChamCong.Columns["SoNgayCong"].HeaderText = "Số ngày công (<=31)";
                dgvChamCong.Columns["SoNgayCong"].DisplayIndex = 3;

                dgvChamCong.Columns["SoGioOT"].HeaderText = "Số giờ OT";
                dgvChamCong.Columns["SoGioOT"].DisplayIndex = 4;

                // Kiểm tra trạng thái đã chốt lương hay chưa để khóa lưới
                bool daChot = _dtChamCongHienTai.Rows.Count > 0 && Convert.ToBoolean(_dtChamCongHienTai.Rows[0]["DaChot"]);
                if (daChot)
                {
                    dgvChamCong.ReadOnly = true;
                    btnLuuChamCong.Enabled = false;
                    lblTrangThaiCC.Text = $"Trạng thái tháng {thangNam}: ĐÃ CHỐT LƯƠNG (KHÓA SỔ)";
                    lblTrangThaiCC.ForeColor = Color.Red;
                }
                else
                {
                    dgvChamCong.ReadOnly = false;
                    dgvChamCong.Columns["MaNV"].ReadOnly = true;
                    dgvChamCong.Columns["HoTen"].ReadOnly = true;
                    dgvChamCong.Columns["ThangNam"].ReadOnly = true;
                    btnLuuChamCong.Enabled = true;
                    lblTrangThaiCC.Text = $"Trạng thái tháng {thangNam}: Đang mở chấm công";
                    lblTrangThaiCC.ForeColor = Color.DarkGreen;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp bảng chấm công: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLuuChamCong_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra dữ liệu có được tải chưa
                if (_dtChamCongHienTai == null || _dtChamCongHienTai.Rows.Count == 0)
                {
                    MessageBox.Show("Vui lòng tải dữ liệu chấm công trước khi lưu!", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kết thúc chỉnh sửa ô hiện tại trên lưới để đẩy dữ liệu vào DataTable
                dgvChamCong.EndEdit();

                int rowsAffected = _chamCongBUS.LuuChamCong(_dtChamCongHienTai);
                MessageBox.Show($"Đã đồng bộ dữ liệu chấm công về SQL Server thành công! ({rowsAffected} dòng cập nhật)",
                    "Disconnected Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadChamCong();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu chấm công: " + ex.Message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion

        #region 3. TÍNH LƯƠNG & CHỐT LƯƠNG (SQLTRANSACTION)

        private void BtnTinhLuong_Click(object sender, EventArgs e) => LoadBangLuong();

        private void LoadBangLuong()
        {
            try
            {
                string thangNam = dtpThangLuong.Value.ToString("MM/yyyy");

                // Ưu tiên lấy bảng lương đã chốt; nếu chưa chốt thì tính dự kiến
                var listDaChot = _luongBUS.GetBangLuongDaChot(thangNam);
                List<BangLuongDTO> dataHienThi;

                if (listDaChot.Count > 0)
                {
                    dataHienThi = listDaChot;
                    btnChotLuong.Enabled = false;
                    btnChotLuong.Text = "Đã chốt lương";
                }
                else
                {
                    dataHienThi = _luongBUS.TinhLuongDuKien(thangNam);
                    btnChotLuong.Enabled = true;
                    btnChotLuong.Text = "Chốt lương tháng";
                }

                dgvBangLuong.DataSource = dataHienThi;
                FormatGridBangLuong();

                decimal tongQuy = dataHienThi.Sum(x => x.ThucLinh);
                lblTongQuyLuong.Text = $"Tổng quỹ lương ({thangNam}): {tongQuy:N0} VNĐ";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tính lương: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatGridBangLuong()
        {
            if (dgvBangLuong.Columns["MaLuong"] != null) dgvBangLuong.Columns["MaLuong"].Visible = false;
            if (dgvBangLuong.Columns["MaNV"] != null) dgvBangLuong.Columns["MaNV"].HeaderText = "Mã NV";
            if (dgvBangLuong.Columns["HoTen"] != null) dgvBangLuong.Columns["HoTen"].HeaderText = "Họ và Tên";
            if (dgvBangLuong.Columns["TenPB"] != null) dgvBangLuong.Columns["TenPB"].HeaderText = "Phòng ban";
            if (dgvBangLuong.Columns["ThangNam"] != null) dgvBangLuong.Columns["ThangNam"].HeaderText = "Tháng";
            if (dgvBangLuong.Columns["LuongCoBan"] != null)
            {
                dgvBangLuong.Columns["LuongCoBan"].HeaderText = "Lương cơ bản";
                dgvBangLuong.Columns["LuongCoBan"].DefaultCellStyle.Format = "N0";
            }
            if (dgvBangLuong.Columns["HeSoLuong"] != null) dgvBangLuong.Columns["HeSoLuong"].HeaderText = "Hệ số";
            if (dgvBangLuong.Columns["SoNgayCong"] != null) dgvBangLuong.Columns["SoNgayCong"].HeaderText = "Ngày công";
            if (dgvBangLuong.Columns["SoGioOT"] != null) dgvBangLuong.Columns["SoGioOT"].HeaderText = "Giờ OT";
            if (dgvBangLuong.Columns["ThucLinh"] != null)
            {
                dgvBangLuong.Columns["ThucLinh"].HeaderText = "Thực lĩnh (VNĐ)";
                dgvBangLuong.Columns["ThucLinh"].DefaultCellStyle.Format = "N0";
                dgvBangLuong.Columns["ThucLinh"].DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            }
        }

        private void BtnChotLuong_Click(object sender, EventArgs e)
        {
            string thangNam = dtpThangLuong.Value.ToString("MM/yyyy");
            var confirm = MessageBox.Show(
                $"Bạn có chắc chắn muốn CHỐT LƯƠNG tháng {thangNam}?\nSau khi chốt, bảng chấm công tháng này sẽ bị khóa và không thể chỉnh sửa.",
                "Xác nhận chốt lương (SqlTransaction)", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                if (_luongBUS.ChotLuongThang(thangNam))
                {
                    MessageBox.Show($"Chốt lương tháng {thangNam} thành công! Dữ liệu chấm công đã được khóa.",
                        "Hoàn tất giao dịch", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadBangLuong();
                    LoadChamCong(); // Đồng bộ lại trạng thái khóa bên tab chấm công
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Giao dịch thất bại (Đã Rollback): " + ex.Message, "Lỗi Transaction", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}