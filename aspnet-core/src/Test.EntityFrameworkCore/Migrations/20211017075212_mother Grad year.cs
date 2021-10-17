using Microsoft.EntityFrameworkCore.Migrations;

namespace Test.Migrations
{
    public partial class motherGradyear : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MotherGraduationYear",
                table: "Forms",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MotherGraduationYear",
                table: "Forms");
        }
    }
}
