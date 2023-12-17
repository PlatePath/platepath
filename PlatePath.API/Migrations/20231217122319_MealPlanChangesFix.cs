using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlatePath.API.Migrations
{
    public partial class MealPlanChangesFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealPlans_AspNetUsers_UserId",
                table: "MealPlans");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "MealPlans",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_MealPlans_AspNetUsers_UserId",
                table: "MealPlans",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealPlans_AspNetUsers_UserId",
                table: "MealPlans");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "MealPlans",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MealPlans_AspNetUsers_UserId",
                table: "MealPlans",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
