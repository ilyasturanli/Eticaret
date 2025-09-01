using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eticaret.Data.Migrations
{
    /// <inheritdoc />
    public partial class phonenotnull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "Password", "UserGuid" },
                values: new object[] { new DateTime(2025, 8, 31, 9, 59, 40, 624, DateTimeKind.Local).AddTicks(3664), "$2a$12$Am/MoFBe6DP2g55ngEkpfumvaA05z8kIdDysmg9rcHcnwedulUq.O", new Guid("9708af7d-1f3f-4590-bd23-bbe453824960") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "Password", "UserGuid" },
                values: new object[] { new DateTime(2025, 8, 30, 21, 58, 15, 207, DateTimeKind.Local).AddTicks(3269), "$2a$12$xZTuDUJRTTQCxvZ1f54WdOnZ4qBFsN67wgWgTks3F//99nPmwdn5y", new Guid("069163f0-615e-4f72-875e-897cf3f3dd00") });
        }
    }
}
