using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HRM.Application.DTOs;
using HRM.Application.Interfaces;
using HRM.Domain.Entities;
using HRM.Domain.Interfaces;
namespace HRM.Application.Services
{
    public class PhongBanService : IPhongBanService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public PhongBanService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        // GET ALL
        public async Task<IEnumerable<PhongBanDto>> GetAllAsync()
        {
            var list = await _uow.Repository<PhongBan>().GetAllAsync();
            return _mapper.Map<IEnumerable<PhongBanDto>>(list);
        }

        // GET BY ID
        public async Task<PhongBanDto?> GetByIdAsync(Guid id)
        {
            var entity = await _uow.Repository<PhongBan>().GetByIdAsync(id);
            return entity is null ? null : _mapper.Map<PhongBanDto>(entity);
        }

        // CREATE
        public async Task<PhongBanDto> CreateAsync(CreatePhongBanDto request)
        {
            // Kiểm tra tên trùng
            var exists = await _uow.Repository<PhongBan>()
                .FindAsync(x => x.TenPhongBan == request.TenPhongBan);
            if (exists.Any())
                throw new InvalidOperationException("Tên phòng ban đã tồn tại.");

            var entity = _mapper.Map<PhongBan>(request);
            await _uow.Repository<PhongBan>().AddAsync(entity);
            await _uow.SaveChangesAsync();
            return _mapper.Map<PhongBanDto>(entity);
        }

        // UPDATE
        public async Task<PhongBanDto> UpdateAsync(Guid id, UpdatePhongBanDto request)
        {
            var entity = await _uow.Repository<PhongBan>().GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Không tìm thấy Id: {id}");

            _mapper.Map(request, entity);
            _uow.Repository<PhongBan>().Update(entity);
            await _uow.SaveChangesAsync();
            return _mapper.Map<PhongBanDto>(entity);
        }

        // DELETE (soft delete)
        public async Task DeleteAsync(Guid id)
        {
            var entity = await _uow.Repository<PhongBan>().GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Không tìm thấy Id: {id}");

            _uow.Repository<PhongBan>().Delete(entity);
            await _uow.SaveChangesAsync();
        }
    }
}
