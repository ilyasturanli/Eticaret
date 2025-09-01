using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eticaret.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAppUserAddresses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AppUsers_AppUserId",
                table: "Addresses");

            migrationBuilder.AlterColumn<string>(
                name: "OpenAdress",
                table: "Addresses",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "Password", "UserGuid" },
                values: new object[] { new DateTime(2025, 8, 30, 14, 41, 55, 14, DateTimeKind.Local).AddTicks(3151), "$2a$12$jHvIsVK0G7iL58zUENug2eHAcOHPABElbG0h1KnXTB61f9hgJX1kC", new Guid("13cc260f-1c1e-4413-83b6-9610d2ef64be") });

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_AppUsers_AppUserId",
                table: "Addresses",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AppUsers_AppUserId",
                table: "Addresses");

            migrationBuilder.AlterColumn<string>(
                name: "OpenAdress",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "Password", "UserGuid" },
                values: new object[] { new DateTime(2025, 8, 30, 14, 3, 18, 508, DateTimeKind.Local).AddTicks(6847), "$2a$12$t66Abj1xkcwib9Vqoc.WA.1u5gcKrD4mY9.QMMn8LpwzD86H5IODy", new Guid("a576e846-8656-450a-93c5-e1a12b71fc0d") });

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_AppUsers_AppUserId",
                table: "Addresses",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "Id");
        }
    }
}
