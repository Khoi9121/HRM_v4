using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Domain.Entities;
using HRM.Domain.Interfaces;
using HRM.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace HRM.Infrastructure.Repositories
{
    public class ThongBaoRepository : GenericRepository<ThongBao>, IThongBaoRepository
    {
        private readonly AppDbContext _ctx;

        public ThongBaoRepository(AppDbContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

        public async Task<(IEnumerable<ThongBao> Data, int TotalCount)> GetPagedAsync(ThongBaoQueryParameters p)
        {
            var query = _ctx.ThongBaos.AsNoTracking().AsQueryable();

            if (p.NguoiNhanId.HasValue)
                query = query.Where(x => x.NguoiNhanId == p.NguoiNhanId || x.NguoiNhanId == null);

            if (p.DaDoc.HasValue)
                query = query.Where(x => x.DaDoc == p.DaDoc);

            if (p.LoaiThongBao.HasValue)
                query = query.Where(x => (int)x.LoaiThongBao == p.LoaiThongBao);

            if (p.MucDoUuTien.HasValue)
                query = query.Where(x => (int)x.MucDoUuTien == p.MucDoUuTien);

            if (!string.IsNullOrWhiteSpace(p.Search))
                query = query.Where(x => x.TieuDe.Contains(p.Search));

            query = query.Where(x => x.NgayHetHan == null || x.NgayHetHan > DateTime.UtcNow);

            var totalCount = await query.CountAsync();

            query = p.SortBy.ToLower() switch
            {
                "mucdo" => p.SortDesc
                            ? query.OrderByDescending(x => x.MucDoUuTien)
                            : query.OrderBy(x => x.MucDoUuTien),
                _ => p.SortDesc
                            ? query.OrderByDescending(x => x.CreatedAt)
                            : query.OrderBy(x => x.CreatedAt)
            };

            var data = await query
                .Skip((p.Page - 1) * p.PageSize)
                .Take(p.PageSize)
                .ToListAsync();

            return (data, totalCount);
        }

        public async Task<int> DemChuaDocAsync(Guid nguoiNhanId)
        {
            return await _ctx.ThongBaos
                .CountAsync(x => (x.NguoiNhanId == nguoiNhanId || x.NguoiNhanId == null)
                              && !x.DaDoc);
        }
    }
}
