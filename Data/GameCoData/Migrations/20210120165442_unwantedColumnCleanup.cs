using Microsoft.EntityFrameworkCore.Migrations;

namespace GameCoData.Migrations
{
    public partial class unwantedColumnCleanup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            //migrationBuilder.DropColumn("NormalizedUserName", "AspNetUsers");
            //migrationBuilder.DropColumn("NormalizedEmail", "AspNetUsers");
            //migrationBuilder.DropColumn("LockoutEnabled", "AspNetUsers");

            //remove all this but ask first
            //migrationBuilder.DropColumn("SecurityStamp", "AspNetUsers");
            //migrationBuilder.DropColumn("ConcurrencyStamp", "AspNetUsers");
            //migrationBuilder.DropColumn("PhoneNumber", "AspNetUsers");
            //migrationBuilder.DropColumn("PhoneNumberConfirmed", "AspNetUsers");
            //migrationBuilder.DropColumn("TwoFactorEnabled", "AspNetUsers");
            //migrationBuilder.DropColumn("LockoutEnd", "AspNetUsers");
            //migrationBuilder.DropColumn("AccessFailedCount", "AspNetUsers");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
