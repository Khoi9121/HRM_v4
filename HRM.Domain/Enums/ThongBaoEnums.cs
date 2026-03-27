using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Domain.Enums
{
    public enum LoaiThongBao
    {
        HeThong = 1, // Thông báo từ hệ thống
        CaNhan = 2, // Gửi cho 1 người
        NhomNguoiDung = 3  // Gửi cho nhóm
    }

    public enum MucDoUuTien
    {
        Thap = 1,
        TrungBinh = 2,
        Cao = 3,
        KhanCap = 4 // Nổi bật, gửi email ngay
    }
}
