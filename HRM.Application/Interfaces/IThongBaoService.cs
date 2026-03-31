using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Application.Common;
using HRM.Application.DTOs;
using HRM.Domain.Interfaces;
namespace HRM.Application.Interfaces
{
    public interface IThongBaoService : IGenericService<ThongBaoDto, CreateThongBaoDto, UpdateThongBaoDto>
    {
        // Đổi tên class tham số ở đây cho khớp với Service
        Task<PagedResult<ThongBaoDto>> GetPagedAsync(ThongBaoQueryParameters p);
        Task DanhDauDaDocAsync(DanhDauDocDto dto);
        Task<int> DemChuaDocAsync(Guid nguoiNhanId);
    }
}
