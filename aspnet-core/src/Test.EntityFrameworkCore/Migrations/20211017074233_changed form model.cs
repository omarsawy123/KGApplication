using Microsoft.EntityFrameworkCore.Migrations;

namespace Test.Migrations
{
    public partial class changedformmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasRelatives",
                table: "Forms",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MotherSchoolGraduate",
                table: "Forms",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasRelatives",
                table: "Forms");

            migrationBuilder.DropColumn(
                name: "MotherSchoolGraduate",
                table: "Forms");
        }
    }
}
