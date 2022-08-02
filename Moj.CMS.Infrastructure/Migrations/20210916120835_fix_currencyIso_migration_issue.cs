using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorHero.CleanArchitecture.Infrastructure.Migrations
{
    public partial class fix_currencyIso_migration_issue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CurrencyIso",
                schema: "CMS",
                table: "Claims",
                newName: "Currency");

            migrationBuilder.RenameColumn(
                name: "CurrencyIso",
                schema: "CMS",
                table: "ClaimHistory",
                newName: "Currency");

            migrationBuilder.RenameColumn(
                name: "CurrencyIso",
                schema: "CMS",
                table: "ClaimDetailsHistory",
                newName: "Currency");

            migrationBuilder.RenameColumn(
                name: "CurrencyIso",
                schema: "CMS",
                table: "ClaimDetails",
                newName: "Currency");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Currency",
                schema: "CMS",
                table: "Claims",
                newName: "CurrencyIso");

            migrationBuilder.RenameColumn(
                name: "Currency",
                schema: "CMS",
                table: "ClaimHistory",
                newName: "CurrencyIso");

            migrationBuilder.RenameColumn(
                name: "Currency",
                schema: "CMS",
                table: "ClaimDetailsHistory",
                newName: "CurrencyIso");

            migrationBuilder.RenameColumn(
                name: "Currency",
                schema: "CMS",
                table: "ClaimDetails",
                newName: "CurrencyIso");
        }
    }
}
