using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorHero.CleanArchitecture.Infrastructure.Migrations
{
    public partial class Rename_Case_Operation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CaseOperations",
                schema: "CMS",
                table: "CaseOperations");

            migrationBuilder.RenameTable(
                name: "CaseOperations",
                schema: "CMS",
                newName: "CaseOperation",
                newSchema: "CMS");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CaseOperation",
                schema: "CMS",
                table: "CaseOperation",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CaseOperation",
                schema: "CMS",
                table: "CaseOperation");

            migrationBuilder.RenameTable(
                name: "CaseOperation",
                schema: "CMS",
                newName: "CaseOperations",
                newSchema: "CMS");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CaseOperations",
                schema: "CMS",
                table: "CaseOperations",
                column: "Id");
        }
    }
}
