using AutoMapper;
using AutoMapper.QueryableExtensions;
using HRM.Application.Common;
using HRM.Application.DTOs;
using HRM.Application.Interfaces;
using HRM.Domain.Entities;
using HRM.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

public class ThongBaoService : IThongBaoService
{
    private readonly IThongBaoRepository _repo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ThongBaoService(
        IThongBaoRepository repo,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _repo = repo;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ThongBaoDto>> GetAllAsync()
    {
        return await _repo.Query()
            .ProjectTo<ThongBaoDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<ThongBaoDto?> GetByIdAsync(Guid id)
    {
        return await _repo.Query()
            .Where(x => x.Id == id)
            .ProjectTo<ThongBaoDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }

    public async Task<ThongBaoDto> CreateAsync(CreateThongBaoDto dto)
    {
        var entity = _mapper.Map<ThongBao>(dto);
        await _repo.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<ThongBaoDto>(entity);
    }

    public async Task<ThongBaoDto> UpdateAsync(Guid id, UpdateThongBaoDto dto)
    {
        var entity = await _repo.GetByIdAsync(id)
                     ?? throw new KeyNotFoundException($"Không tìm thấy Id: {id}");

        _mapper.Map(dto, entity);
        entity.UpdatedAt = DateTime.UtcNow;

        _repo.Update(entity);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<ThongBaoDto>(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _repo.GetByIdAsync(id)
                     ?? throw new KeyNotFoundException($"Không tìm thấy Id: {id}");

        _repo.Delete(entity);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DanhDauDaDocAsync(DanhDauDocDto dto)
    {
        if (dto.ThongBaoIds == null || !dto.ThongBaoIds.Any()) return;

        // Load các bản ghi cần sửa về (chỉ lấy những gì cần)
        var entities = await _repo.Query()
            .Where(x => dto.ThongBaoIds.Contains(x.Id) && !x.DaDoc)
            .ToListAsync();

        if (entities.Any())
        {
            foreach (var entity in entities)
            {
                entity.DaDoc = true;
                entity.NgayDoc = DateTime.UtcNow;
                _repo.Update(entity);
            }
            await _unitOfWork.SaveChangesAsync();
        }
    }

    public async Task<int> DemChuaDocAsync(Guid nguoiNhanId)
    {
        return await _repo.Query()
            .CountAsync(x => (x.NguoiNhanId == nguoiNhanId || x.NguoiNhanId == null)
                             && !x.DaDoc
                             && (x.NgayHetHan == null || x.NgayHetHan > DateTime.UtcNow));
    }

    // Đảm bảo tên tham số p ở đây khớp với class ThongBaoQueryParameters
    public async Task<PagedResult<ThongBaoDto>> GetPagedAsync(ThongBaoQueryParameters p)
    {
        var query = _repo.Query();

        if (p.NguoiNhanId.HasValue)
            query = query.Where(x => x.NguoiNhanId == p.NguoiNhanId || x.NguoiNhanId == null);

        if (p.DaDoc.HasValue)
            query = query.Where(x => x.DaDoc == p.DaDoc);

        if (p.LoaiThongBao.HasValue)
            query = query.Where(x => (int)x.LoaiThongBao == p.LoaiThongBao);

        if (p.MucDoUuTien.HasValue)
            query = query.Where(x => (int)x.MucDoUuTien == p.MucDoUuTien);

        if (!string.IsNullOrWhiteSpace(p.Search))
            query = query.Where(x => x.TieuDe.Contains(p.Search) || x.NoiDung.Contains(p.Search));

        query = query.Where(x => x.NgayHetHan == null || x.NgayHetHan > DateTime.UtcNow);

        var totalCount = await query.CountAsync();

        query = p.SortBy.ToLower() switch
        {
            "mucdo" => p.SortDesc ? query.OrderByDescending(x => x.MucDoUuTien) : query.OrderBy(x => x.MucDoUuTien),
            _ => p.SortDesc ? query.OrderByDescending(x => x.CreatedAt) : query.OrderBy(x => x.CreatedAt)
        };

        var data = await query
            .Skip((p.Page - 1) * p.PageSize)
            .Take(p.PageSize)
            .ProjectTo<ThongBaoDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return new PagedResult<ThongBaoDto>
        {
            Data = data,
            Page = p.Page,
            PageSize = p.PageSize,
            TotalCount = totalCount
        };
    }
}