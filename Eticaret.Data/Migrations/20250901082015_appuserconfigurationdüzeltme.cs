using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eticaret.Data.Migrations
{
    /// <inheritdoc />
    public partial class appuserconfigurationdüzeltme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "Email", "Password", "UserGuid" },
                values: new object[] { new DateTime(2025, 9, 1, 11, 20, 14, 655, DateTimeKind.Local).AddTicks(3402), "admin@cnrticaret.com", "$2a$12$eDrriWOtcGK0N/pSCyXxN.kG8ixISoY/vZNXEDLhhWH0tVMPAqTHC", new Guid("4d7d0fd7-f038-45a4-b9cf-0740a18b6d96") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "Email", "Password", "UserGuid" },
                values: new object[] { new DateTime(2025, 8, 31, 10, 1, 1, 2, DateTimeKind.Local).AddTicks(2097), "ilyas@gmail.com", "$2a$12$0v7Epi5bStCFd80EwYU4WuTmOCcbYZw.otcHaONQ.Zh19YygCWZ7.", new Guid("f874f25a-7777-4454-bf12-23bead845843") });
        }
    }
}
