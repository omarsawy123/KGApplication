using Microsoft.EntityFrameworkCore.Migrations;

namespace Test.Migrations
{
    public partial class changedfk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationTimeDates_ApplicationTimeDates_ApplicationId",
                table: "ApplicationTimeDates");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationTimeDates_ApplicationId",
                table: "ApplicationTimeDates");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "ApplicationTimeDates");

            migrationBuilder.AddColumn<int>(
                name: "FormId",
                table: "ApplicationTimeDates",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationTimeDates_FormId",
                table: "ApplicationTimeDates",
                column: "FormId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationTimeDates_Forms_FormId",
                table: "ApplicationTimeDates",
                column: "FormId",
                principalTable: "Forms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationTimeDates_Forms_FormId",
                table: "ApplicationTimeDates");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationTimeDates_FormId",
                table: "ApplicationTimeDates");

            migrationBuilder.DropColumn(
                name: "FormId",
                table: "ApplicationTimeDates");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "ApplicationTimeDates",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationTimeDates_ApplicationId",
                table: "ApplicationTimeDates",
                column: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationTimeDates_ApplicationTimeDates_ApplicationId",
                table: "ApplicationTimeDates",
                column: "ApplicationId",
                principalTable: "ApplicationTimeDates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
