using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Application.DTOs
{
    // ── Trả về dữ liệu (dùng cho GET) ──────────────────
    public class ChucVuDto
    {
        public Guid Id { get; set; }
        public string TenChucVu { get; set; } = string.Empty;
        public string? MoTa { get; set; }
        public DateTime CreatedAt { get; set; }
        
    }

    // ── Tạo mới (dùng cho POST) ─────────────────────────
    public class CreateChucVuDto
    {
        public string TenChucVu { get; set; } = string.Empty;
        public string? MoTa { get; set; }
    }

    // ── Cập nhật (dùng cho PUT) ─────────────────────────
    public class UpdateChucVuDto
    {
        public string TenChucVu { get; set; } = string.Empty;
        public string? MoTa { get; set; }
    }
}
