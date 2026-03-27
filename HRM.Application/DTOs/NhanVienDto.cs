using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Application.DTOs
{
    public class NhanVienDto
    {
        public Guid Id { get; set; }
        public string HoTen { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? SoDienThoai { get; set; }
        public DateTime NgaySinh { get; set; }
        public DateTime NgayVaoLam { get; set; }
        public decimal LuongCoBan { get; set; }
        public Guid ChucVuId { get; set; }
        public string TenChucVu { get; set; } = string.Empty;
    }
    // Tạo mới (POST)
    public class CreateNhanVienDto
    {  
        public string HoTen { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? SoDienThoai { get; set; }
        public DateTime NgaySinh { get; set; }
        public DateTime NgayVaoLam { get; set; }
        public decimal LuongCoBan { get; set; }
        public Guid ChucVuId { get; set; }
    }

    // Cập nhật (PUT)
    public class UpdateNhanVienDto
    {
        public string HoTen { get; set; } = string.Empty;
        public string? SoDienThoai { get; set; }
        public decimal LuongCoBan { get; set; }
        public Guid ChucVuId { get; set; }
    }
}
