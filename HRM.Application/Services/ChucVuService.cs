using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HRM.Application.DTOs;
using HRM.Application.Interfaces;
using HRM.Domain.Entities;
using HRM.Domain.Interfaces;
namespace HRM.Application.Services
{
    public class ChucVuService : IChucVuService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ChucVuService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        // ── GET ALL ─────────────────────────────────────
        public async Task<IEnumerable<ChucVuDto>> GetAllAsync()
        {
            var list = await _uow.Repository<ChucVu>().GetAllAsync();
            return _mapper.Map<IEnumerable<ChucVuDto>>(list);
        }

        // ── GET BY ID ───────────────────────────────────
        public async Task<ChucVuDto?> GetByIdAsync(Guid id)
        {
            var entity = await _uow.Repository<ChucVu>().GetByIdAsync(id);
            return entity is null ? null : _mapper.Map<ChucVuDto>(entity);
        }

        // ── CREATE ──────────────────────────────────────
        public async Task<ChucVuDto> CreateAsync(CreateChucVuDto request)
        {
            // Validate tên trùng
            var exists = await _uow.Repository<ChucVu>()
                .FindAsync(x => x.TenChucVu.ToLower() == request.TenChucVu.ToLower());
            if (exists.Any())
                throw new InvalidOperationException(
                    $"Chức vụ '{request.TenChucVu}' đã tồn tại.");

            var entity = _mapper.Map<ChucVu>(request);
            await _uow.Repository<ChucVu>().AddAsync(entity);
            await _uow.SaveChangesAsync();

            return _mapper.Map<ChucVuDto>(entity);
        }

        // ── UPDATE ──────────────────────────────────────
        public async Task<ChucVuDto> UpdateAsync(Guid id, UpdateChucVuDto request)
        {
            var entity = await _uow.Repository<ChucVu>().GetByIdAsync(id)
                ?? throw new KeyNotFoundException(
                    $"Không tìm thấy chức vụ với Id: {id}");

            // Validate tên trùng với chức vụ khác
            var duplicate = await _uow.Repository<ChucVu>()
                .FindAsync(x => x.TenChucVu.ToLower() == request.TenChucVu.ToLower()
                             && x.Id != id);
            if (duplicate.Any())
                throw new InvalidOperationException(
                    $"Chức vụ '{request.TenChucVu}' đã tồn tại.");

            _mapper.Map(request, entity);
            _uow.Repository<ChucVu>().Update(entity);
            await _uow.SaveChangesAsync();

            return _mapper.Map<ChucVuDto>(entity);
        }

        // ── DELETE (soft delete) ─────────────────────────
        public async Task DeleteAsync(Guid id)
        {
            var entity = await _uow.Repository<ChucVu>().GetByIdAsync(id)
                ?? throw new KeyNotFoundException(
                    $"Không tìm thấy chức vụ với Id: {id}");

            _uow.Repository<ChucVu>().Delete(entity);
            await _uow.SaveChangesAsync();
        }
    }
}
