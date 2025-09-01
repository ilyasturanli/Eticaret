using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eticaret.Data.Migrations
{
    /// <inheritdoc />
    public partial class Identitytceklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityNumber",
                table: "AppUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "IdentityNumber", "Password", "UserGuid" },
                values: new object[] { new DateTime(2025, 8, 30, 13, 27, 23, 292, DateTimeKind.Local).AddTicks(2755), "11111111111", "$2a$12$mewbpzzHLdIe1RbBrDBueesGpJI21/rseFS/ti4Jsf0XIoLRhQVjS", new Guid("d3689cab-395d-40a1-b53c-8fca209af795") });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2025, 8, 30, 13, 27, 23, 584, DateTimeKind.Local).AddTicks(3314));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2025, 8, 30, 13, 27, 23, 584, DateTimeKind.Local).AddTicks(3951));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdentityNumber",
                table: "AppUsers");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "Password", "UserGuid" },
                values: new object[] { new DateTime(2025, 8, 28, 12, 27, 6, 628, DateTimeKind.Local).AddTicks(1233), "$2a$12$I7zyYCZQiwkTteJxTUV1.epsIO8JZ8TYNvCipqmkTnKTp.nsZvrSS", new Guid("237db5fa-157e-4fa2-a240-3890dd98e950") });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2025, 8, 28, 12, 27, 6, 895, DateTimeKind.Local).AddTicks(7474));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2025, 8, 28, 12, 27, 6, 895, DateTimeKind.Local).AddTicks(8047));
        }
    }
}
