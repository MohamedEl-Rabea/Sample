using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorHero.CleanArchitecture.Infrastructure.Migrations
{
    public partial class add_total_credit_and_debt_amounts_to_party : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Currency",
                schema: "CMS",
                table: "Party",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalCreditAmount",
                schema: "CMS",
                table: "Party",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalDebtAmount",
                schema: "CMS",
                table: "Party",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                schema: "CMS",
                table: "Party");

            migrationBuilder.DropColumn(
                name: "TotalCreditAmount",
                schema: "CMS",
                table: "Party");

            migrationBuilder.DropColumn(
                name: "TotalDebtAmount",
                schema: "CMS",
                table: "Party");
        }
    }
}
