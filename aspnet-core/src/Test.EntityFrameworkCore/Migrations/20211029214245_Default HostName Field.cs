using Microsoft.EntityFrameworkCore.Migrations;

namespace Test.Migrations
{
    public partial class DefaultHostNameField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DefaultHostName",
                table: "DefaultSettings",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultHostName",
                table: "DefaultSettings");
        }
    }
}
