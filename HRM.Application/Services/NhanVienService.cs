using System;
using System.Collections.Generic;
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
    public class NhanVienService : INhanVienService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public NhanVienService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NhanVienDto>> GetAllAsync()
        {
            var nvList = await _uow.Repository<NhanVien>().GetAllAsync();
            var cvList = await _uow.Repository<ChucVu>().GetAllAsync();
            var cvDict = cvList.ToDictionary(x => x.Id, x => x.TenChucVu);

            return nvList.Select(nv => {
                var dto = _mapper.Map<NhanVienDto>(nv);
                dto.TenChucVu = cvDict.GetValueOrDefault(nv.ChucVuId, string.Empty);
                return dto;
            });
        }

        public async Task<NhanVienDto?> GetByIdAsync(Guid id)
        {
            var nv = await _uow.Repository<NhanVien>().GetByIdAsync(id);
            if (nv is null) return null;

            var cv = await _uow.Repository<ChucVu>().GetByIdAsync(nv.ChucVuId);
            var dto = _mapper.Map<NhanVienDto>(nv);
            dto.TenChucVu = cv?.TenChucVu ?? string.Empty;
            return dto;
        }

        public async Task<NhanVienDto> CreateAsync(CreateNhanVienDto request)
        {
            var exists = await _uow.Repository<NhanVien>()
                .FindAsync(x => x.Email == request.Email);
            if (exists.Any())
                throw new InvalidOperationException("Email đã tồn tại.");

            var nv = _mapper.Map<NhanVien>(request);
            await _uow.Repository<NhanVien>().AddAsync(nv);
            await _uow.SaveChangesAsync();

            return await GetByIdAsync(nv.Id) ?? throw new Exception("Lỗi tạo mới.");
        }

        public async Task<NhanVienDto> UpdateAsync(Guid id, UpdateNhanVienDto request)
        {
            var nv = await _uow.Repository<NhanVien>().GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Không tìm thấy Id: {id}");

            _mapper.Map(request, nv);
            _uow.Repository<NhanVien>().Update(nv);
            await _uow.SaveChangesAsync();

            return await GetByIdAsync(id) ?? throw new Exception("Lỗi cập nhật.");
        }

        public async Task DeleteAsync(Guid id)
        {
            var nv = await _uow.Repository<NhanVien>().GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Không tìm thấy Id: {id}");

            _uow.Repository<NhanVien>().Delete(nv);
            await _uow.SaveChangesAsync();
        }
    }
}
