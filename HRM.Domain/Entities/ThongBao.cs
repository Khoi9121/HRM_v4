using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Domain.Enums;
namespace HRM.Domain.Entities
{
    public class ThongBao : BaseEntity
    {
        public string TieuDe { get; set; } = string.Empty;
        public string NoiDung { get; set; } = string.Empty;
        public LoaiThongBao LoaiThongBao { get; set; } = LoaiThongBao.HeThong;
        public MucDoUuTien MucDoUuTien { get; set; } = MucDoUuTien.TrungBinh;

        public Guid NguoiGuiId { get; set; }
        public Guid? NguoiNhanId { get; set; }

        public bool DaDoc { get; set; } = false;
        public DateTime? NgayDoc { get; set; }
        public DateTime? NgayHetHan { get; set; }

        // Domain method
        public void DanhDauDaDoc()
        {
            if (!DaDoc)
            {
                DaDoc = true;
                NgayDoc = DateTime.UtcNow;
            }
        }
    }
}
