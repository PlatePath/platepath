using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlatePath.API.Migrations
{
    public partial class AddIsBannedToUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBanned",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBanned",
                table: "AspNetUsers");
        }
    }
}
