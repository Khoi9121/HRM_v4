using HRM.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Application.Interfaces
{
    public interface IPhongBanService : IGenericService<PhongBanDto, CreatePhongBanDto, UpdatePhongBanDto>
    {
        // Ví dụ method đặc thù nếu cần:
        // Task<IEnumerable<PhongBanDto>> TimKiemAsync(string keyword);
    }
}
