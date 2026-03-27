using HRM.Application.Interfaces;
using HRM.Application.Mappings;
using HRM.Application.Services;
using HRM.Domain.Interfaces;
using HRM.Infrastructure;
using HRM.Infrastructure.Data;
using HRM.Infrastructure.Repositories; // 👈 nhớ thêm dòng này
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// Repository & UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Services
builder.Services.AddScoped<INhanVienService, NhanVienService>();
builder.Services.AddScoped<IPhongBanService, PhongBanService>();
builder.Services.AddScoped<IChucVuService, ChucVuService>();

// 🔥 THÊM 2 DÒNG NÀY
builder.Services.AddScoped<IThongBaoService, ThongBaoService>();
builder.Services.AddScoped<IThongBaoRepository, ThongBaoRepository>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();