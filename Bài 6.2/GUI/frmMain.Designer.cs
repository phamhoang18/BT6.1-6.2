using System.Drawing;
using System.Windows.Forms;

namespace HRM_Payroll_System.GUI
{
    partial class frmMain
    {
        private System.ComponentModel.IContainer components = null;

        private TabControl tabMain;
        private TabPage tabNhanSu;
        private TabPage tabChamCong;
        private TabPage tabTinhLuong;

        private GroupBox grpThongTinNV;
        private Label lblMaNV, lblHoTen, lblNgaySinh, lblCCCD, lblPhongBan, lblLuongCB, lblHeSo;
        private TextBox txtMaNV, txtHoTen, txtCCCD, txtLuongCB, txtHeSo;
        private DateTimePicker dtpNgaySinh;
        private ComboBox cboPhongBan;
        private Button btnThem, btnSua, btnXoa, btnLamMoi, btnChuyenPB;

        private GroupBox grpTimKiem;
        private Label lblSearchKey, lblSearchPB;
        private TextBox txtSearchKey;
        private ComboBox cboSearchPB;
        private Button btnTimKiem;
        private DataGridView dgvNhanVien;

        private Panel pnlTopCC;
        private Label lblThangCC, lblTrangThaiCC;
        private DateTimePicker dtpThangCC;
        private Button btnTaiChamCong, btnLuuChamCong;
        private DataGridView dgvChamCong;

        private Panel pnlTopLuong;
        private Label lblThangLuong, lblTongQuyLuong;
        private DateTimePicker dtpThangLuong;
        private Button btnTinhLuong, btnChotLuong;
        private DataGridView dgvBangLuong;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tabMain = new TabControl();
            this.tabNhanSu = new TabPage();
            this.tabChamCong = new TabPage();
            this.tabTinhLuong = new TabPage();

            this.grpThongTinNV = new GroupBox();
            this.lblMaNV = new Label(); this.txtMaNV = new TextBox();
            this.lblHoTen = new Label(); this.txtHoTen = new TextBox();
            this.lblNgaySinh = new Label(); this.dtpNgaySinh = new DateTimePicker();
            this.lblCCCD = new Label(); this.txtCCCD = new TextBox();
            this.lblPhongBan = new Label(); this.cboPhongBan = new ComboBox();
            this.lblLuongCB = new Label(); this.txtLuongCB = new TextBox();
            this.lblHeSo = new Label(); this.txtHeSo = new TextBox();
            this.btnThem = new Button(); this.btnSua = new Button();
            this.btnXoa = new Button(); this.btnLamMoi = new Button();
            this.btnChuyenPB = new Button();

            this.grpTimKiem = new GroupBox();
            this.lblSearchKey = new Label(); this.txtSearchKey = new TextBox();
            this.lblSearchPB = new Label(); this.cboSearchPB = new ComboBox();
            this.btnTimKiem = new Button();
            this.dgvNhanVien = new DataGridView();

            this.grpThongTinNV.Text = "Thông tin hồ sơ nhân viên";
            this.grpThongTinNV.Location = new Point(12, 10);
            this.grpThongTinNV.Size = new Size(960, 165);

            lblMaNV.Text = "Mã NV:"; lblMaNV.Location = new Point(20, 30); lblMaNV.AutoSize = true;
            txtMaNV.Location = new Point(100, 27); txtMaNV.Size = new Size(100, 23); txtMaNV.ReadOnly = true;

            lblHoTen.Text = "Họ tên:"; lblHoTen.Location = new Point(230, 30); lblHoTen.AutoSize = true;
            txtHoTen.Location = new Point(300, 27); txtHoTen.Size = new Size(220, 23);

            lblNgaySinh.Text = "Ngày sinh:"; lblNgaySinh.Location = new Point(550, 30); lblNgaySinh.AutoSize = true;
            dtpNgaySinh.Location = new Point(630, 27); dtpNgaySinh.Size = new Size(140, 23); dtpNgaySinh.Format = DateTimePickerFormat.Short;

            lblCCCD.Text = "CCCD:"; lblCCCD.Location = new Point(20, 70); lblCCCD.AutoSize = true;
            txtCCCD.Location = new Point(100, 67); txtCCCD.Size = new Size(150, 23);

            lblPhongBan.Text = "Phòng ban:"; lblPhongBan.Location = new Point(270, 70); lblPhongBan.AutoSize = true;
            cboPhongBan.Location = new Point(350, 67); cboPhongBan.Size = new Size(170, 23); cboPhongBan.DropDownStyle = ComboBoxStyle.DropDownList;

            lblLuongCB.Text = "Lương CB:"; lblLuongCB.Location = new Point(550, 70); lblLuongCB.AutoSize = true;
            txtLuongCB.Location = new Point(630, 67); txtLuongCB.Size = new Size(140, 23);

            lblHeSo.Text = "Hệ số:"; lblHeSo.Location = new Point(790, 70); lblHeSo.AutoSize = true;
            txtHeSo.Location = new Point(840, 67); txtHeSo.Size = new Size(80, 23); txtHeSo.Text = "1.0";

            btnThem.Text = "Thêm mới"; btnThem.Location = new Point(100, 115); btnThem.Size = new Size(100, 32);
            btnSua.Text = "Cập nhật"; btnSua.Location = new Point(215, 115); btnSua.Size = new Size(100, 32);
            btnChuyenPB.Text = "Chuyển Phòng"; btnChuyenPB.Location = new Point(330, 115); btnChuyenPB.Size = new Size(120, 32);
            btnXoa.Text = "Xóa NV"; btnXoa.Location = new Point(465, 115); btnXoa.Size = new Size(100, 32);
            btnLamMoi.Text = "Làm mới"; btnLamMoi.Location = new Point(580, 115); btnLamMoi.Size = new Size(100, 32);

            this.grpThongTinNV.Controls.AddRange(new Control[] {
                lblMaNV, txtMaNV, lblHoTen, txtHoTen, lblNgaySinh, dtpNgaySinh,
                lblCCCD, txtCCCD, lblPhongBan, cboPhongBan, lblLuongCB, txtLuongCB,
                lblHeSo, txtHeSo, btnThem, btnSua, btnChuyenPB, btnXoa, btnLamMoi
            });

            this.grpTimKiem.Text = "Tra cứu nhân sự";
            this.grpTimKiem.Location = new Point(12, 185);
            this.grpTimKiem.Size = new Size(960, 65);

            lblSearchKey.Text = "Tên / CCCD:"; lblSearchKey.Location = new Point(20, 28); lblSearchKey.AutoSize = true;
            txtSearchKey.Location = new Point(105, 25); txtSearchKey.Size = new Size(250, 23);

            lblSearchPB.Text = "Phòng ban:"; lblSearchPB.Location = new Point(380, 28); lblSearchPB.AutoSize = true;
            cboSearchPB.Location = new Point(460, 25); cboSearchPB.Size = new Size(200, 23); cboSearchPB.DropDownStyle = ComboBoxStyle.DropDownList;

            btnTimKiem.Text = "Tìm kiếm"; btnTimKiem.Location = new Point(685, 23); btnTimKiem.Size = new Size(110, 28);

            this.grpTimKiem.Controls.AddRange(new Control[] { lblSearchKey, txtSearchKey, lblSearchPB, cboSearchPB, btnTimKiem });

            this.dgvNhanVien.Location = new Point(12, 260);
            this.dgvNhanVien.Size = new Size(960, 320);
            this.dgvNhanVien.AllowUserToAddRows = false;
            this.dgvNhanVien.ReadOnly = true;
            this.dgvNhanVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.tabNhanSu.Text = "1. Quản lý Hồ sơ Nhân sự";
            this.tabNhanSu.Controls.AddRange(new Control[] { grpThongTinNV, grpTimKiem, dgvNhanVien });

            this.pnlTopCC = new Panel();
            this.lblThangCC = new Label();
            this.dtpThangCC = new DateTimePicker();
            this.btnTaiChamCong = new Button();
            this.btnLuuChamCong = new Button();
            this.lblTrangThaiCC = new Label();
            this.dgvChamCong = new DataGridView();

            this.pnlTopCC.Dock = DockStyle.Top;
            this.pnlTopCC.Height = 65;

            lblThangCC.Text = "Chọn Tháng/Năm:"; lblThangCC.Location = new Point(15, 22); lblThangCC.AutoSize = true;
            dtpThangCC.Location = new Point(130, 18); dtpThangCC.Size = new Size(100, 23);
            dtpThangCC.Format = DateTimePickerFormat.Custom; dtpThangCC.CustomFormat = "MM/yyyy"; dtpThangCC.ShowUpDown = true;

            btnTaiChamCong.Text = "Nạp bảng chấm công"; btnTaiChamCong.Location = new Point(250, 15); btnTaiChamCong.Size = new Size(150, 30);
            btnLuuChamCong.Text = "Lưu chấm công"; btnLuuChamCong.Location = new Point(415, 15); btnLuuChamCong.Size = new Size(140, 30);
            btnLuuChamCong.BackColor = Color.LightSkyBlue;

            lblTrangThaiCC.Text = "Trạng thái: Đang mở"; lblTrangThaiCC.Location = new Point(580, 22); lblTrangThaiCC.AutoSize = true;
            lblTrangThaiCC.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            this.pnlTopCC.Controls.AddRange(new Control[] { lblThangCC, dtpThangCC, btnTaiChamCong, btnLuuChamCong, lblTrangThaiCC });

            this.dgvChamCong.Dock = DockStyle.Fill;
            this.dgvChamCong.AllowUserToAddRows = false;
            this.dgvChamCong.AllowUserToDeleteRows = false;
            this.dgvChamCong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.tabChamCong.Text = "2. Chấm công Tháng (Disconnected)";
            this.tabChamCong.Controls.Add(dgvChamCong);
            this.tabChamCong.Controls.Add(pnlTopCC);

            this.pnlTopLuong = new Panel();
            this.lblThangLuong = new Label();
            this.dtpThangLuong = new DateTimePicker();
            this.btnTinhLuong = new Button();
            this.btnChotLuong = new Button();
            this.lblTongQuyLuong = new Label();
            this.dgvBangLuong = new DataGridView();

            this.pnlTopLuong.Dock = DockStyle.Top;
            this.pnlTopLuong.Height = 65;

            lblThangLuong.Text = "Chọn Tháng/Năm:"; lblThangLuong.Location = new Point(0, 22); lblThangLuong.AutoSize = true;
            dtpThangLuong.Location = new Point(130, 18); dtpThangLuong.Size = new Size(100, 23);
            dtpThangLuong.Format = DateTimePickerFormat.Custom; dtpThangLuong.CustomFormat = "MM/yyyy"; dtpThangLuong.ShowUpDown = true;

            btnTinhLuong.Text = "Tính / Xem bảng lương"; btnTinhLuong.Location = new Point(250, 15); btnTinhLuong.Size = new Size(160, 30);
            btnChotLuong.Text = "Chốt lương tháng"; btnChotLuong.Location = new Point(425, 15); btnChotLuong.Size = new Size(150, 30);
            btnChotLuong.BackColor = Color.LightCoral;

            lblTongQuyLuong.Text = "Tổng quỹ lương: 0 VNĐ"; lblTongQuyLuong.Location = new Point(600, 22); lblTongQuyLuong.AutoSize = true;
            lblTongQuyLuong.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTongQuyLuong.ForeColor = Color.DarkGreen;

            this.pnlTopLuong.Controls.AddRange(new Control[] { lblThangLuong, dtpThangLuong, btnTinhLuong, btnChotLuong, lblTongQuyLuong });

            this.dgvBangLuong.Dock = DockStyle.Fill;
            this.dgvBangLuong.ReadOnly = true;
            this.dgvBangLuong.AllowUserToAddRows = false;
            this.dgvBangLuong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.tabTinhLuong.Text = "3. Tính Lương & Chốt Lương";
            this.tabTinhLuong.Controls.Add(dgvBangLuong);
            this.tabTinhLuong.Controls.Add(pnlTopLuong);

            this.tabMain.Dock = DockStyle.Fill;
            this.tabMain.Controls.AddRange(new Control[] { tabNhanSu, tabChamCong, tabTinhLuong });

            this.ClientSize = new Size(1000, 630);
            this.Controls.Add(tabMain);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Hệ Thống Quản Lý Nhân Sự & Tính Lương (HRM & Payroll System)";
        }
    }
}
