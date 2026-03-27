using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Domain.Entities;


namespace HRM.Domain.Interfaces
{
    public class ThongBaoQueryParameters
    {
        public Guid? NguoiNhanId { get; set; }
        public bool? DaDoc { get; set; }
        public int? LoaiThongBao { get; set; }
        public int? MucDoUuTien { get; set; }
        public string? Search { get; set; }
        public string SortBy { get; set; } = "createdAt";
        public bool SortDesc { get; set; } = true;
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }

    public interface IThongBaoRepository : IGenericRepository<ThongBao>
    {
        Task<(IEnumerable<ThongBao> Data, int TotalCount)> GetPagedAsync(ThongBaoQueryParameters p);
        Task<int> DemChuaDocAsync(Guid nguoiNhanId);
    }
}
