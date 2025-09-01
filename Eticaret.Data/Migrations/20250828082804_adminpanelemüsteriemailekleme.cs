using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eticaret.Data.Migrations
{
    /// <inheritdoc />
    public partial class adminpanelemüsteriemailekleme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AppUserId",
                table: "Orders",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AppUsers_AppUserId",
                table: "Orders",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AppUsers_AppUserId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_AppUserId",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "Password", "UserGuid" },
                values: new object[] { new DateTime(2025, 8, 28, 10, 56, 0, 727, DateTimeKind.Local).AddTicks(7510), "$2a$12$WFFXsH0X7TfiKc9oVp/l.eov16X5g.GNrAKHUmHoeSf/0vAsk7UQC", new Guid("ee8d28c4-29d4-4b2c-b4aa-854eb8daba56") });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2025, 8, 28, 10, 56, 1, 4, DateTimeKind.Local).AddTicks(4021));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2025, 8, 28, 10, 56, 1, 4, DateTimeKind.Local).AddTicks(4873));
        }
    }
}
