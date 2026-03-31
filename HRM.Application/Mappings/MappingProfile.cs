using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HRM.Application.DTOs;
using HRM.Domain.Entities;
using HRM.Domain.Enums;
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
            // thông báo 
            CreateMap<ThongBao, ThongBaoDto>()
    .ForMember(d => d.TenLoai, o => o.MapFrom(s =>
        s.LoaiThongBao == LoaiThongBao.HeThong ? "Hệ thống" :
        s.LoaiThongBao == LoaiThongBao.CaNhan ? "Cá nhân" : "Nhóm"))
    .ForMember(d => d.TenMucDo, o => o.MapFrom(s =>
        s.MucDoUuTien == MucDoUuTien.Thap ? "Thấp" :
        s.MucDoUuTien == MucDoUuTien.TrungBinh ? "Trung bình" :
        s.MucDoUuTien == MucDoUuTien.Cao ? "Cao" : "Khẩn cấp"));

            CreateMap<CreateThongBaoDto, ThongBao>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.DaDoc, o => o.MapFrom(_ => false)) // Mặc định chưa đọc
                .ForMember(d => d.CreatedAt, o => o.MapFrom(_ => DateTime.UtcNow))
                .ForMember(d => d.IsDeleted, o => o.Ignore());

            CreateMap<UpdateThongBaoDto, ThongBao>()
                .ForAllMembers(o => o.Condition((src, dest, srcMember) => srcMember != null)); // Chỉ map các trường có giá trị
        }
    }
}
