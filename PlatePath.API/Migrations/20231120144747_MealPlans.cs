using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlatePath.API.Migrations
{
    public partial class MealPlans : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0f3b3216-bfed-4ad1-9c73-411d40765cc4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a33c861c-48df-4b31-b974-3af58888f58f");

            migrationBuilder.CreateTable(
                name: "MealPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MealPlanRecipe",
                columns: table => new
                {
                    MealPlansId = table.Column<int>(type: "int", nullable: false),
                    MealsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealPlanRecipe", x => new { x.MealPlansId, x.MealsId });
                    table.ForeignKey(
                        name: "FK_MealPlanRecipe_MealPlans_MealPlansId",
                        column: x => x.MealPlansId,
                        principalTable: "MealPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealPlanRecipe_Recipes_MealsId",
                        column: x => x.MealsId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            /*migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "655d0884-acdb-439f-8db4-dee281a30cee", "1", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a663d7c5-15fa-4e1d-9ff6-92516ace4e94", "2", "User", "User" });*/

            migrationBuilder.CreateIndex(
                name: "IX_MealPlanRecipe_MealsId",
                table: "MealPlanRecipe",
                column: "MealsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MealPlanRecipe");

            migrationBuilder.DropTable(
                name: "MealPlans");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "655d0884-acdb-439f-8db4-dee281a30cee");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a663d7c5-15fa-4e1d-9ff6-92516ace4e94");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0f3b3216-bfed-4ad1-9c73-411d40765cc4", "1", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a33c861c-48df-4b31-b974-3af58888f58f", "2", "User", "User" });
        }
    }
}
