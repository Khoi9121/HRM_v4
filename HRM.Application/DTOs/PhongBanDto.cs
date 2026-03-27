using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Application.DTOs
{
    public class PhongBanDto
    {
        public Guid Id { get; set; }
        public string TenPhongBan { get; set; } = string.Empty;
        public string? MoTa { get; set; }
    }
    // Dùng để TẠO MỚI (POST)
    public class CreatePhongBanDto
    {
        public string TenPhongBan { get; set; } = string.Empty;
        public string? MoTa { get; set; }
    }

    // Dùng để CẬP NHẬT (PUT)
    public class UpdatePhongBanDto
    {
        public string TenPhongBan { get; set; } = string.Empty;
        public string? MoTa { get; set; }
    }
}
