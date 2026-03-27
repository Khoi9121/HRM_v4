using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Application.DTOs;
namespace HRM.Application.Interfaces
{
    // Kế thừa Generic — thêm method đặc thù nếu cần
    public interface INhanVienService : IGenericService<NhanVienDto, CreateNhanVienDto, UpdateNhanVienDto>
    {
        // Ví dụ method đặc thù:
        // Task<IEnumerable<NhanVienDto>> GetByChucVuAsync(Guid chucVuId);
    }
}
