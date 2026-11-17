namespace SalesManagement.GUI
{
    partial class FormBanHang
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabBanHang = new System.Windows.Forms.TabPage();
            this.tabSanPham = new System.Windows.Forms.TabPage();
            this.tabThongKe = new System.Windows.Forms.TabPage();

            this.txtTimSP = new System.Windows.Forms.TextBox();
            this.btnTimSP = new System.Windows.Forms.Button();
            this.dgvSanPhamChon = new System.Windows.Forms.DataGridView();
            this.numSoLuong = new System.Windows.Forms.NumericUpDown();
            this.btnThemGio = new System.Windows.Forms.Button();
            this.dgvGioHang = new System.Windows.Forms.DataGridView();
            this.txtMaKH = new System.Windows.Forms.TextBox();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.lblDiemCong = new System.Windows.Forms.Label();
            this.btnThanhToan = new System.Windows.Forms.Button();

            this.txtMaSP = new System.Windows.Forms.TextBox();
            this.txtTenSP = new System.Windows.Forms.TextBox();
            this.txtGiaBan = new System.Windows.Forms.TextBox();
            this.txtTonKho = new System.Windows.Forms.TextBox();
            this.btnThemSP = new System.Windows.Forms.Button();
            this.btnSuaSP = new System.Windows.Forms.Button();
            this.btnXoaSP = new System.Windows.Forms.Button();
            this.dgvQLSanPham = new System.Windows.Forms.DataGridView();

            this.btnXemBaoCao = new System.Windows.Forms.Button();
            this.lblDoanhThu = new System.Windows.Forms.Label();
            this.dgvTop5 = new System.Windows.Forms.DataGridView();

            this.SuspendLayout();

            this.tabControl1.Controls.Add(this.tabBanHang);
            this.tabControl1.Controls.Add(this.tabSanPham);
            this.tabControl1.Controls.Add(this.tabThongKe);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Size = new System.Drawing.Size(900, 550);

            this.tabBanHang.Text = "Bán Hàng & Lập Hóa Đơn";
            this.txtTimSP.Location = new System.Drawing.Point(20, 20);
            this.txtTimSP.Size = new System.Drawing.Size(220, 23);
            this.btnTimSP.Text = "Tìm SP";
            this.btnTimSP.Location = new System.Drawing.Point(250, 19);
            this.btnTimSP.Size = new System.Drawing.Size(80, 28);
            this.btnTimSP.Click += new System.EventHandler(this.btnTimSP_Click);

            this.dgvSanPhamChon.Location = new System.Drawing.Point(20, 55);
            this.dgvSanPhamChon.Size = new System.Drawing.Size(400, 350);
            this.dgvSanPhamChon.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSanPhamChon.ReadOnly = true;

            this.numSoLuong.Location = new System.Drawing.Point(20, 420);
            this.numSoLuong.Minimum = 1;
            this.numSoLuong.Maximum = 1000;
            this.numSoLuong.Value = 1;

            this.btnThemGio.Text = "Thêm vào giỏ >>";
            this.btnThemGio.Location = new System.Drawing.Point(160, 418);
            this.btnThemGio.Size = new System.Drawing.Size(130, 28);
            this.btnThemGio.Click += new System.EventHandler(this.btnThemGio_Click);

            this.dgvGioHang.Location = new System.Drawing.Point(450, 55);
            this.dgvGioHang.Size = new System.Drawing.Size(410, 280);
            this.dgvGioHang.ReadOnly = true;

            var lblKH = new System.Windows.Forms.Label() { Text = "Mã KH:", Location = new System.Drawing.Point(450, 350), AutoSize = true };
            this.txtMaKH.Location = new System.Drawing.Point(630, 347);
            this.txtMaKH.Size = new System.Drawing.Size(100, 23);

            this.lblTongTien.Text = "Tổng tiền: 0 VNĐ";
            this.lblTongTien.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTongTien.Location = new System.Drawing.Point(450, 385);
            this.lblTongTien.AutoSize = true;

            this.lblDiemCong.Text = "Điểm tích lũy cộng thêm: 0";
            this.lblDiemCong.Location = new System.Drawing.Point(450, 415);
            this.lblDiemCong.AutoSize = true;

            this.btnThanhToan.Text = "THANH TOÁN";
            this.btnThanhToan.BackColor = System.Drawing.Color.LightGreen;
            this.btnThanhToan.Location = new System.Drawing.Point(710, 390);
            this.btnThanhToan.Size = new System.Drawing.Size(150, 45);
            this.btnThanhToan.Click += new System.EventHandler(this.btnThanhToan_Click);

            this.tabBanHang.Controls.AddRange(new System.Windows.Forms.Control[] {
                txtTimSP, btnTimSP, dgvSanPhamChon, numSoLuong, btnThemGio,
                dgvGioHang, lblKH, txtMaKH, lblTongTien, lblDiemCong, btnThanhToan
            });

            this.tabSanPham.Text = "Quản Lý Sản Phẩm";
            var l1 = new System.Windows.Forms.Label() { Text = "Mã SP:", Location = new System.Drawing.Point(10, 20) };
            this.txtMaSP.Location = new System.Drawing.Point(110, 20);
            this.txtMaSP.Size = new System.Drawing.Size(200, 23);
            var l2 = new System.Windows.Forms.Label() { Text = "Tên SP:", Location = new System.Drawing.Point(10, 55) };
            this.txtTenSP.Location = new System.Drawing.Point(110, 52);
            this.txtTenSP.Size = new System.Drawing.Size(200, 23);
            var l3 = new System.Windows.Forms.Label() { Text = "Giá bán:", Location = new System.Drawing.Point(340, 20) };
            this.txtGiaBan.Location = new System.Drawing.Point(440, 17);
            var l4 = new System.Windows.Forms.Label() { Text = "Tồn kho:", Location = new System.Drawing.Point(340, 55) };
            this.txtTonKho.Location = new System.Drawing.Point(440, 52);

            this.btnThemSP.Text = "Thêm";
            this.btnThemSP.Location = new System.Drawing.Point(600, 17);
            this.btnThemSP.Size = new System.Drawing.Size(60, 28);
            this.btnThemSP.Click += new System.EventHandler(this.btnThemSP_Click);

            this.btnSuaSP.Text = "Sửa";
            this.btnSuaSP.Location = new System.Drawing.Point(685, 17);
            this.btnSuaSP.Size = new System.Drawing.Size(60, 28);
            this.btnSuaSP.Click += new System.EventHandler(this.btnSuaSP_Click);

            this.btnXoaSP.Text = "Xóa";
            this.btnXoaSP.Location = new System.Drawing.Point(770, 17);
            this.btnXoaSP.Size = new System.Drawing.Size(60, 28);
            this.btnXoaSP.Click += new System.EventHandler(this.btnXoaSP_Click);

            this.dgvQLSanPham.Location = new System.Drawing.Point(20, 100);
            this.dgvQLSanPham.Size = new System.Drawing.Size(840, 380);
            this.dgvQLSanPham.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvQLSanPham_CellClick);

            this.tabSanPham.Controls.AddRange(new System.Windows.Forms.Control[] {
                l1, txtMaSP, l2, txtTenSP, l3, txtGiaBan, l4, txtTonKho, btnThemSP, btnSuaSP, btnXoaSP, dgvQLSanPham
            });

            this.tabThongKe.Text = "Báo Cáo - Thống Kê";
            this.btnXemBaoCao.Text = "Cập nhật Thống kê";
            this.btnXemBaoCao.Location = new System.Drawing.Point(20, 20);
            this.btnXemBaoCao.Size = new System.Drawing.Size(150, 30);
            this.btnXemBaoCao.Click += new System.EventHandler(this.btnXemBaoCao_Click);

            this.lblDoanhThu.Text = "Doanh thu hôm nay: 0 VNĐ";
            this.lblDoanhThu.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblDoanhThu.Location = new System.Drawing.Point(200, 23);
            this.lblDoanhThu.AutoSize = true;

            this.dgvTop5.Location = new System.Drawing.Point(20, 70);
            this.dgvTop5.Size = new System.Drawing.Size(840, 400);

            this.tabThongKe.Controls.AddRange(new System.Windows.Forms.Control[] { btnXemBaoCao, lblDoanhThu, dgvTop5 });

            this.ClientSize = new System.Drawing.Size(900, 550);
            this.Controls.Add(this.tabControl1);
            this.Text = "Bài 6.1 - Hệ Thống Quản Lý Bán Hàng (3-Tier + SqlTransaction)";
            this.Load += new System.EventHandler(this.FormBanHang_Load);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabBanHang, tabSanPham, tabThongKe;
        private System.Windows.Forms.TextBox txtTimSP, txtMaKH, txtMaSP, txtTenSP, txtGiaBan, txtTonKho;
        private System.Windows.Forms.Button btnTimSP, btnThemGio, btnThanhToan, btnThemSP, btnSuaSP, btnXoaSP, btnXemBaoCao;
        private System.Windows.Forms.DataGridView dgvSanPhamChon, dgvGioHang, dgvQLSanPham, dgvTop5;
        private System.Windows.Forms.NumericUpDown numSoLuong;
        private System.Windows.Forms.Label lblTongTien, lblDiemCong, lblDoanhThu;
    }
}