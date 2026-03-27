using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HRM.Application.DTOs;
using HRM.Domain.Entities;
namespace HRM.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // NhanVien → DTO (TenChucVu được set thủ công trong Service)
            CreateMap<NhanVien, NhanVienDto>();

            // CreateDto → Entity
            CreateMap<CreateNhanVienDto, NhanVien>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.CreatedAt, o => o.Ignore())
                .ForMember(d => d.IsDeleted, o => o.Ignore());

            // UpdateDto → Entity (merge vào existing)
            CreateMap<UpdateNhanVienDto, NhanVien>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Email, o => o.Ignore())
                .ForMember(d => d.NgaySinh, o => o.Ignore())
                .ForMember(d => d.NgayVaoLam, o => o.Ignore());
            // PhongBan → DTO
            CreateMap<PhongBan, PhongBanDto>();

            // CreateDto → Entity
            CreateMap<CreatePhongBanDto, PhongBan>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.CreatedAt, o => o.Ignore())
                .ForMember(d => d.IsDeleted, o => o.Ignore());

            // UpdateDto → Entity (merge vào entity đang có)
            CreateMap<UpdatePhongBanDto, PhongBan>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.CreatedAt, o => o.Ignore());

            // ChucVu → ChucVuDto (GET)
            CreateMap<ChucVu, ChucVuDto>();

            // CreateChucVuDto → ChucVu (POST)
            CreateMap<CreateChucVuDto, ChucVu>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.CreatedAt, o => o.Ignore())
                .ForMember(d => d.UpdatedAt, o => o.Ignore())
                .ForMember(d => d.IsDeleted, o => o.Ignore());

            // UpdateChucVuDto → ChucVu (PUT — merge vào entity đang có)
            CreateMap<UpdateChucVuDto, ChucVu>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.CreatedAt, o => o.Ignore())
                .ForMember(d => d.UpdatedAt, o => o.Ignore())
                .ForMember(d => d.IsDeleted, o => o.Ignore());
        }
    }
}
