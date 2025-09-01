using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eticaret.Data.Migrations
{
    /// <inheritdoc />
    public partial class phone11hane : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "Password", "UserGuid" },
                values: new object[] { new DateTime(2025, 8, 31, 10, 1, 1, 2, DateTimeKind.Local).AddTicks(2097), "$2a$12$0v7Epi5bStCFd80EwYU4WuTmOCcbYZw.otcHaONQ.Zh19YygCWZ7.", new Guid("f874f25a-7777-4454-bf12-23bead845843") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "Password", "UserGuid" },
                values: new object[] { new DateTime(2025, 8, 31, 9, 59, 40, 624, DateTimeKind.Local).AddTicks(3664), "$2a$12$Am/MoFBe6DP2g55ngEkpfumvaA05z8kIdDysmg9rcHcnwedulUq.O", new Guid("9708af7d-1f3f-4590-bd23-bbe453824960") });
        }
    }
}
