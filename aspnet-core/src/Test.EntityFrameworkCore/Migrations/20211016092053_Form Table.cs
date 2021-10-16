using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Test.Migrations
{
    public partial class FormTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Forms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    StudentName = table.Column<string>(nullable: false),
                    StudentNameAr = table.Column<string>(nullable: false),
                    StudentBirthDate = table.Column<DateTime>(nullable: false),
                    StudentReligion = table.Column<string>(nullable: false),
                    FatherJob = table.Column<string>(nullable: false),
                    MotherJob = table.Column<string>(nullable: false),
                    FatherMobile = table.Column<string>(nullable: false),
                    MotherMobile = table.Column<string>(nullable: false),
                    Telephone = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    StudentNationalId = table.Column<long>(nullable: false),
                    StudentRelativeName = table.Column<string>(nullable: true),
                    StudentRelativeGrade = table.Column<string>(nullable: true),
                    JoiningSchool = table.Column<string>(nullable: true),
                    TenantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forms", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Forms");
        }
    }
}
