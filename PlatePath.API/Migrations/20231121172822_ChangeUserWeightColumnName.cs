using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlatePath.API.Migrations
{
    public partial class ChangeUserWeightColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "046b0fb4-792a-4e6a-be57-055f6fb66002");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f2e036f5-d0eb-46ef-a95d-79d563b41807");

            migrationBuilder.RenameColumn(
                name: "WeightCm",
                table: "AspNetUsers",
                newName: "WeightKg");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "79046ee0-144f-49fb-94c6-1070d0e1d8c8", "1", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f1e928d7-775e-40b1-bf7e-86939afa81f9", "2", "User", "User" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "79046ee0-144f-49fb-94c6-1070d0e1d8c8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f1e928d7-775e-40b1-bf7e-86939afa81f9");

            migrationBuilder.RenameColumn(
                name: "WeightKg",
                table: "AspNetUsers",
                newName: "WeightCm");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "046b0fb4-792a-4e6a-be57-055f6fb66002", "2", "User", "User" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f2e036f5-d0eb-46ef-a95d-79d563b41807", "1", "Admin", "Admin" });
        }
    }
}
