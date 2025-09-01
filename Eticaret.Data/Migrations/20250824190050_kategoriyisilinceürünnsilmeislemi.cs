using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eticaret.Data.Migrations
{
    /// <inheritdoc />
    public partial class kategoriyisilinceürünnsilmeislemi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "Password", "UserGuid" },
                values: new object[] { new DateTime(2025, 8, 24, 22, 0, 49, 769, DateTimeKind.Local).AddTicks(5428), "$2a$12$PInXxaxKTFNz3prJrc2oruf1FjejGzTJWITsFwYqMxUk6OPWXqCbG", new Guid("728a423a-209b-4029-b7db-45f8dd213e8d") });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2025, 8, 24, 22, 0, 50, 55, DateTimeKind.Local).AddTicks(5931));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2025, 8, 24, 22, 0, 50, 55, DateTimeKind.Local).AddTicks(6591));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
