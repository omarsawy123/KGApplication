using Microsoft.EntityFrameworkCore.Migrations;

namespace Test.Migrations
{
    public partial class settingstableisDefaultfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "DefaultSettings",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "DefaultSettings");
        }
    }
}
