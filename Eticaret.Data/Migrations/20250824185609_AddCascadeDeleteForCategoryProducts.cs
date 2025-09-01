using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eticaret.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCascadeDeleteForCategoryProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "Password", "UserGuid" },
                values: new object[] { new DateTime(2025, 8, 24, 21, 56, 7, 824, DateTimeKind.Local).AddTicks(1160), "$2a$12$aQU6e1OPjqxeHZ3dcLxQWehC.8P5EkO4nxSlJVcfT5/UywcknTLcu", new Guid("ab6481a1-dcf6-499a-893a-6524c6e4be2c") });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2025, 8, 24, 21, 56, 8, 227, DateTimeKind.Local).AddTicks(5916));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2025, 8, 24, 21, 56, 8, 227, DateTimeKind.Local).AddTicks(6591));

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "Password", "UserGuid" },
                values: new object[] { new DateTime(2025, 8, 24, 18, 34, 30, 648, DateTimeKind.Local).AddTicks(6488), "$2a$12$hwCRbcadDvPj5aqYJ79ANev2WG6TGRzDMTHV9cmzxHIp3JKJSv0Ju", new Guid("7bb748f8-a9de-4edf-8456-05fa7d831874") });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2025, 8, 24, 18, 34, 31, 73, DateTimeKind.Local).AddTicks(7748));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2025, 8, 24, 18, 34, 31, 73, DateTimeKind.Local).AddTicks(8853));

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
