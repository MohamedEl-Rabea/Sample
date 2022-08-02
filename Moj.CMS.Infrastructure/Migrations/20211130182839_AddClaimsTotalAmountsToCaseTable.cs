using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorHero.CleanArchitecture.Infrastructure.Migrations
{
    public partial class AddClaimsTotalAmountsToCaseTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RequiredAmount",
                schema: "CMS",
                table: "Case",
                newName: "TotalRequiredAmount");

            migrationBuilder.AddColumn<decimal>(
                name: "ApprovedAmount",
                schema: "CMS",
                table: "Case",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalRemainingAmount",
                schema: "CMS",
                table: "Case",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedAmount",
                schema: "CMS",
                table: "Case");

            migrationBuilder.DropColumn(
                name: "TotalRemainingAmount",
                schema: "CMS",
                table: "Case");

            migrationBuilder.RenameColumn(
                name: "TotalRequiredAmount",
                schema: "CMS",
                table: "Case",
                newName: "RequiredAmount");
        }
    }
}
