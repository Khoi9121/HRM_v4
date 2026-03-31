using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HRM.Domain.Entities;

namespace HRM.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // DbSet
        public DbSet<NhanVien> NhanViens => Set<NhanVien>();
        public DbSet<ChucVu> ChucVus => Set<ChucVu>();
        public DbSet<PhongBan> PhongBans => Set<PhongBan>();
        public DbSet<ThongBao> ThongBaos => Set<ThongBao>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ========================
            // NhanVien
            // ========================
            modelBuilder.Entity<NhanVien>(e =>
            {
                e.HasKey(x => x.Id);

                e.Property(x => x.HoTen)
                    .IsRequired()
                    .HasMaxLength(100);

                e.Property(x => x.Email)
                    .IsRequired()
                    .HasMaxLength(150);

                e.Property(x => x.LuongCoBan)
                    .HasColumnType("decimal(18,2)");

                e.Property(x => x.ChucVuId)
                    .IsRequired();
            });

            // ========================
            // ChucVu
            // ========================
            modelBuilder.Entity<ChucVu>(e =>
            {
                e.HasKey(x => x.Id);

                e.Property(x => x.TenChucVu)
                    .IsRequired()
                    .HasMaxLength(100);

                e.Property(x => x.MoTa)
                    .HasMaxLength(500);
            });

            // ========================
            // PhongBan
            // ========================
            modelBuilder.Entity<PhongBan>(e =>
            {
                e.HasKey(x => x.Id);

                e.Property(x => x.TenPhongBan)
                    .IsRequired()
                    .HasMaxLength(100);

                e.Property(x => x.MoTa)
                    .HasMaxLength(500);
            });

            // ========================
            // ThongBao
            // ========================
            modelBuilder.Entity<ThongBao>(e =>
            {
                e.HasKey(x => x.Id);

                e.Property(x => x.TieuDe)
                    .IsRequired()
                    .HasMaxLength(200);

                e.Property(x => x.NoiDung)
                    .IsRequired();
                e.HasIndex(x => x.NguoiNhanId)
                .HasDatabaseName("IX_ThongBaos_NguoiNhanId");
            });

            // ========================
            // Global Query Filter (Soft Delete)
            // ========================
            modelBuilder.Entity<NhanVien>()
                .HasQueryFilter(x => !x.IsDeleted);

            modelBuilder.Entity<ChucVu>()
                .HasQueryFilter(x => !x.IsDeleted);

            modelBuilder.Entity<PhongBan>()
                .HasQueryFilter(x => !x.IsDeleted);
        }
    }
} 