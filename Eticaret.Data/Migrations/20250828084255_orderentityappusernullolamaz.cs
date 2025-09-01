using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eticaret.Data.Migrations
{
    /// <inheritdoc />
    public partial class orderentityappusernullolamaz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "Password", "UserGuid" },
                values: new object[] { new DateTime(2025, 8, 28, 11, 28, 3, 718, DateTimeKind.Local).AddTicks(824), "$2a$12$5OmTgjtWTKAXkjeex2WfoO/K8Ve9cbY3ZKPJscKspzk0wksjwIUYa", new Guid("b8012dc2-1ade-47fa-ac21-1e38f7fbeb30") });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2025, 8, 28, 11, 28, 4, 15, DateTimeKind.Local).AddTicks(2841));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2025, 8, 28, 11, 28, 4, 15, DateTimeKind.Local).AddTicks(3485));
        }
    }
}
