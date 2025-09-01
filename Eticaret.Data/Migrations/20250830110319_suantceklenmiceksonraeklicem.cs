using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Eticaret.Data.Migrations
{
    /// <inheritdoc />
    public partial class suantceklenmiceksonraeklicem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "IdentityNumber",
                table: "AppUsers");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "Password", "UserGuid" },
                values: new object[] { new DateTime(2025, 8, 30, 14, 3, 18, 508, DateTimeKind.Local).AddTicks(6847), "$2a$12$t66Abj1xkcwib9Vqoc.WA.1u5gcKrD4mY9.QMMn8LpwzD86H5IODy", new Guid("a576e846-8656-450a-93c5-e1a12b71fc0d") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreateDate", "Description", "Image", "IsActive", "IsTopMenu", "Name", "OrderNo", "ParentId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 30, 13, 27, 23, 584, DateTimeKind.Local).AddTicks(3314), null, null, true, true, "Elektronik", 1, 0 },
                    { 2, new DateTime(2025, 8, 30, 13, 27, 23, 584, DateTimeKind.Local).AddTicks(3951), null, null, true, true, "Bilgisayar", 2, 0 }
                });
        }
    }
}
