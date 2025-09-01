using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eticaret.Data.Migrations
{
    /// <inheritdoc />
    public partial class EnumOrderStateeklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderState",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderState",
                table: "Orders");

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
    }
}
