using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorHero.CleanArchitecture.Infrastructure.Migrations
{
    public partial class Update_Promissory_Date_Name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IssueDate",
                schema: "CMS",
                table: "Promissory",
                newName: "PromissoryIssueDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PromissoryIssueDate",
                schema: "CMS",
                table: "Promissory",
                newName: "IssueDate");
        }
    }
}
