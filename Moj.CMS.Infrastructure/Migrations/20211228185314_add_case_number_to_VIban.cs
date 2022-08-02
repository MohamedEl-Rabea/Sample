using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorHero.CleanArchitecture.Infrastructure.Migrations
{
    public partial class add_case_number_to_VIban : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ParentAccountNumber",
                schema: "CMS",
                table: "VIbans",
                newName: "Iban");

            migrationBuilder.AddColumn<string>(
                name: "CaseNumber",
                schema: "CMS",
                table: "VIbans",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CaseNumber",
                schema: "CMS",
                table: "VIbans");

            migrationBuilder.RenameColumn(
                name: "Iban",
                schema: "CMS",
                table: "VIbans",
                newName: "ParentAccountNumber");
        }
    }
}
