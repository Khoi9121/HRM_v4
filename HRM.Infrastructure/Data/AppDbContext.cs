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

        public DbSet<NhanVien> NhanViens => Set<NhanVien>();
        public DbSet<ChucVu> ChucVus => Set<ChucVu>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cấu hình NhanVien
            modelBuilder.Entity<NhanVien>(e => {
                e.HasKey(x => x.Id);
                e.Property(x => x.HoTen).IsRequired().HasMaxLength(100);
                e.Property(x => x.Email).IsRequired().HasMaxLength(150);
                e.Property(x => x.LuongCoBan).HasColumnType("decimal(18,2)");
                // FK thuần — không cấu hình HasOne/WithMany vì không có Navigation
                e.Property(x => x.ChucVuId).IsRequired();
            });

            // Cấu hình ChucVu
            modelBuilder.Entity<ChucVu>(e => {
                e.HasKey(x => x.Id);
                e.Property(x => x.TenChucVu).IsRequired().HasMaxLength(100);
            });

            // Global query filter — tự động lọc IsDeleted
            modelBuilder.Entity<NhanVien>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<ChucVu>().HasQueryFilter(x => !x.IsDeleted);

            //Cấu hình PhongBan
            // Trong OnModelCreating — thêm vào cuối, trước dấu }
            modelBuilder.Entity<PhongBan>(e => {
                e.HasKey(x => x.Id);
                e.Property(x => x.TenPhongBan)
                    .IsRequired()
                    .HasMaxLength(100);
                e.Property(x => x.MoTa)
                    .HasMaxLength(500);
            });
            modelBuilder.Entity<ChucVu>(e => {
                e.HasKey(x => x.Id);

                e.Property(x => x.TenChucVu)
                    .IsRequired()
                    .HasMaxLength(100);

                e.Property(x => x.MoTa)
                    .HasMaxLength(500);
            });
        }
    }
}
