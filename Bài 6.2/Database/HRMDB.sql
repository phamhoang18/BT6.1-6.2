USE master;
GO

IF EXISTS (SELECT * FROM sys.databases WHERE name = 'HRMDB')
BEGIN
    ALTER DATABASE HRMDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE HRMDB;
END
GO

CREATE DATABASE HRMDB;
GO
USE HRMDB;
GO

-- 1. Bảng Phòng Ban
CREATE TABLE PhongBan (
    MaPB INT PRIMARY KEY IDENTITY(1,1),
    TenPB NVARCHAR(100) NOT NULL,
    Mota NVARCHAR(255)
);

-- 2. Bảng Nhân Viên
CREATE TABLE NhanVien (
    MaNV INT PRIMARY KEY IDENTITY(1,1),
    HoTen NVARCHAR(100) NOT NULL,
    NgaySinh DATETIME,
    CCCD VARCHAR(20) UNIQUE NOT NULL,
    MaPB INT FOREIGN KEY REFERENCES PhongBan(MaPB),
    LuongCoBan DECIMAL(18,2) CHECK (LuongCoBan > 0),
    HeSoLuong FLOAT DEFAULT 1.0
);

-- 3. Bảng Chấm Công (Thêm DaChot để khóa dữ liệu sau khi chốt lương)
CREATE TABLE BangChamCong (
    MaCong INT IDENTITY(1,1) UNIQUE,
    MaNV INT FOREIGN KEY REFERENCES NhanVien(MaNV),
    ThangNam VARCHAR(7) NOT NULL, -- VD: "11/2026"
    SoNgayCong FLOAT DEFAULT 0 CHECK (SoNgayCong >= 0 AND SoNgayCong <= 31),
    SoGioOT FLOAT DEFAULT 0 CHECK (SoGioOT >= 0),
    DaChot BIT DEFAULT 0,
    CONSTRAINT PK_BangChamCong PRIMARY KEY (MaNV, ThangNam)
);

-- 4. Bảng Lương Chi Tiết
CREATE TABLE BangLuong (
    MaLuong INT IDENTITY(1,1),
    MaNV INT FOREIGN KEY REFERENCES NhanVien(MaNV),
    ThangNam VARCHAR(7) NOT NULL,
    ThucLinh DECIMAL(18,2) NOT NULL,
    CONSTRAINT PK_BangLuong PRIMARY KEY (MaNV, ThangNam)
);
GO

-- Dữ liệu mẫu khởi tạo
INSERT INTO PhongBan (TenPB, Mota) VALUES 
(N'Phòng Kỹ Thuật', N'Phát triển phần mềm và hạ tầng'),
(N'Phòng Nhân Sự', N'Quản lý tuyển dụng và C&B'),
(N'Phòng Kế Toán', N'Quản lý tài chính và tiền lương');

INSERT INTO NhanVien (HoTen, NgaySinh, CCCD, MaPB, LuongCoBan, HeSoLuong) VALUES
(N'Nguyễn Văn An', '1998-05-12', '001098001122', 1, 12000000, 1.5),
(N'Trần Thị Bình', '2000-08-20', '001200003344', 2, 9500000, 1.2),
(N'Lê Hoàng Cường', '1995-11-02', '001095005566', 3, 15000000, 1.8);
GO