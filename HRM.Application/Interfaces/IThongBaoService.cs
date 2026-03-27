using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Application.Common;
using HRM.Application.DTOs;
namespace HRM.Application.Interfaces
{
    public interface IThongBaoService : IGenericService<ThongBaoDto, CreateThongBaoDto, UpdateThongBaoDto>
    {
        Task<PagedResult<ThongBaoDto>> GetPagedAsync(ThongBaoPaginationParams p);
        Task DanhDauDaDocAsync(DanhDauDocDto dto);
        Task<int> DemChuaDocAsync(Guid nguoiNhanId);
    }
}
