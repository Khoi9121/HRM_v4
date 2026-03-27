using AutoMapper;
using HRM.Application.Common;
using HRM.Application.DTOs;
using HRM.Application.Interfaces;
using HRM.Domain.Entities;
using HRM.Domain.Interfaces;

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
        var data = await _repo.GetAllAsync();
        return _mapper.Map<IEnumerable<ThongBaoDto>>(data);
    }

    public async Task<ThongBaoDto?> GetByIdAsync(Guid id)
    {
        var entity = await _repo.GetByIdAsync(id);
        return entity is null ? null : _mapper.Map<ThongBaoDto>(entity);
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
        var entities = await _repo.FindAsync(x => dto.ThongBaoIds.Contains(x.Id));

        foreach (var tb in entities.Where(x => !x.DaDoc))
        {
            tb.DanhDauDaDoc();
        }

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<int> DemChuaDocAsync(Guid nguoiNhanId)
    {
        return await _repo.CountAsync(x => x.NguoiNhanId == nguoiNhanId && !x.DaDoc);
    }
    public async Task<PagedResult<ThongBaoDto>> GetPagedAsync(ThongBaoPaginationParams p)
    {
        var query = await _repo.GetAllAsync();

        // Filter
        if (p.NguoiNhanId.HasValue)
            query = query.Where(x => x.NguoiNhanId == p.NguoiNhanId.Value);

        if (p.DaDoc.HasValue)
            query = query.Where(x => x.DaDoc == p.DaDoc.Value);

        if (!string.IsNullOrEmpty(p.Search))
            query = query.Where(x => x.TieuDe.Contains(p.Search));

        var totalCount = query.Count();

        var data = query
            .Skip((p.Page - 1) * p.PageSize)
            .Take(p.PageSize)
            .ToList();

        return new PagedResult<ThongBaoDto>
        {
            Data = _mapper.Map<IEnumerable<ThongBaoDto>>(data),
            Page = p.Page,
            PageSize = p.PageSize,
            TotalCount = totalCount
        };
    }
}