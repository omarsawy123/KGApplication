using Microsoft.EntityFrameworkCore.Migrations;

namespace Test.Migrations
{
    public partial class UserIdinapplicationDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ApplicationTimeDates",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ApplicationTimeDates");
        }
    }
}
