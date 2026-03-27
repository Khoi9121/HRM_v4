using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddThongBao_WithIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PhongBan",
                table: "PhongBan");

            migrationBuilder.RenameTable(
                name: "PhongBan",
                newName: "PhongBans");

            migrationBuilder.AlterColumn<string>(
                name: "MoTa",
                table: "ChucVus",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhongBans",
                table: "PhongBans",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ThongBaos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TieuDe = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoaiThongBao = table.Column<int>(type: "int", nullable: false),
                    MucDoUuTien = table.Column<int>(type: "int", nullable: false),
                    NguoiGuiId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NguoiNhanId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DaDoc = table.Column<bool>(type: "bit", nullable: false),
                    NgayDoc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayHetHan = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongBaos", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThongBaos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhongBans",
                table: "PhongBans");

            migrationBuilder.RenameTable(
                name: "PhongBans",
                newName: "PhongBan");

            migrationBuilder.AlterColumn<string>(
                name: "MoTa",
                table: "ChucVus",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhongBan",
                table: "PhongBan",
                column: "Id");
        }
    }
}
