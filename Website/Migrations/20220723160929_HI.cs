using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Website.Migrations
{
    public partial class HI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "Order",
                nullable: false,
                defaultValue: new DateTime());
            migrationBuilder.AddColumn<DateTime>(
               name: "ModifiedDateTime",
               table: "Order",
               nullable: false,
               defaultValue: new DateTime());
            migrationBuilder.AddColumn<int>(
               name: "timeLeftSec",
               table: "Order",
               nullable: false,
               defaultValue: 0);
            migrationBuilder.AddColumn<int>(
               name: "timeLeftSec",
               table: "Order",
               nullable: false,
               defaultValue: 0);




        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
