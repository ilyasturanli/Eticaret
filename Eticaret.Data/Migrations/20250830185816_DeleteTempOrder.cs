using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eticaret.Data.Migrations
{
    /// <inheritdoc />
    public partial class DeleteTempOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TempOrders");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "Password", "UserGuid" },
                values: new object[] { new DateTime(2025, 8, 30, 21, 58, 15, 207, DateTimeKind.Local).AddTicks(3269), "$2a$12$xZTuDUJRTTQCxvZ1f54WdOnZ4qBFsN67wgWgTks3F//99nPmwdn5y", new Guid("069163f0-615e-4f72-875e-897cf3f3dd00") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TempOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    BillingAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CartJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConversationId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveryAddress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempOrders", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "Password", "UserGuid" },
                values: new object[] { new DateTime(2025, 8, 30, 21, 47, 40, 155, DateTimeKind.Local).AddTicks(7269), "$2a$12$SQ/iuQj8svBilvG6YbKIWuC/bbKokZjUxzsR7dMUtRorVrq5p5bWe", new Guid("7310cf93-749d-4062-b6d2-16c3d26439ee") });
        }
    }
}
