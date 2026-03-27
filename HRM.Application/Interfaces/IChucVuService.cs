using HRM.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Application.Interfaces
{
    public interface IChucVuService : IGenericService<ChucVuDto, CreateChucVuDto, UpdateChucVuDto>
    {
        // Thêm method đặc thù nếu cần, ví dụ:
        // Task<bool> TenDaTonTaiAsync(string tenChucVu);
    }
}
