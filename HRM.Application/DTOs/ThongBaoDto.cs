using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Domain.Enums;
using HRM.Application.Common;
namespace HRM.Application.DTOs
{
    public class ThongBaoDto
    {
        public Guid Id { get; set; }
        public string TieuDe { get; set; } = string.Empty;
        public string NoiDung { get; set; } = string.Empty;
        public LoaiThongBao LoaiThongBao { get; set; }
        public string TenLoai { get; set; } = string.Empty;
        public MucDoUuTien MucDoUuTien { get; set; }
        public string TenMucDo { get; set; } = string.Empty;
        public Guid NguoiGuiId { get; set; }
        public Guid? NguoiNhanId { get; set; }
        public bool DaDoc { get; set; }
        public DateTime? NgayDoc { get; set; }
        public DateTime? NgayHetHan { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateThongBaoDto
    {
        public string TieuDe { get; set; } = string.Empty;
        public string NoiDung { get; set; } = string.Empty;
        public LoaiThongBao LoaiThongBao { get; set; } = LoaiThongBao.HeThong;
        public MucDoUuTien MucDoUuTien { get; set; } = MucDoUuTien.TrungBinh;
        public Guid NguoiGuiId { get; set; }
        public Guid? NguoiNhanId { get; set; }
        public DateTime? NgayHetHan { get; set; }
    }

    public class UpdateThongBaoDto
    {
        public string TieuDe { get; set; } = string.Empty;
        public string NoiDung { get; set; } = string.Empty;
        public MucDoUuTien MucDoUuTien { get; set; }
        public DateTime? NgayHetHan { get; set; }
    }

    public class DanhDauDocDto
    {
        public List<Guid> ThongBaoIds { get; set; } = [];
    }
}
