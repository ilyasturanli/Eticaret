using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eticaret.Data.Migrations
{
    /// <inheritdoc />
    public partial class orderentityappusernullolucak : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "Password", "UserGuid" },
                values: new object[] { new DateTime(2025, 8, 28, 12, 7, 18, 393, DateTimeKind.Local).AddTicks(6518), "$2a$12$1wpAUiH5KUbyUYMwa8PpVuWrjlItuxaq2KdGX4N3qm4bEXzie0Emu", new Guid("51c88324-7c21-4aac-bdd7-2e9df2084cb3") });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2025, 8, 28, 12, 7, 18, 661, DateTimeKind.Local).AddTicks(1074));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2025, 8, 28, 12, 7, 18, 661, DateTimeKind.Local).AddTicks(1687));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "Password", "UserGuid" },
                values: new object[] { new DateTime(2025, 8, 28, 11, 42, 54, 135, DateTimeKind.Local).AddTicks(2722), "$2a$12$WhOUH2O9wYcfddoDwZ1F3Ojw.dCcV2LzakvqawimGXSa9fjcA2qIS", new Guid("086ede4b-7767-4482-bc67-657b5f710724") });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2025, 8, 28, 11, 42, 54, 413, DateTimeKind.Local).AddTicks(8382));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2025, 8, 28, 11, 42, 54, 413, DateTimeKind.Local).AddTicks(9021));
        }
    }
}
