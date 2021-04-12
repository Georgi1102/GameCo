using Microsoft.EntityFrameworkCore.Migrations;

namespace GameCoData.Migrations
{
    public partial class levelAddedToUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountLevel",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountLevel",
                table: "AspNetUsers");
        }
    }
}
