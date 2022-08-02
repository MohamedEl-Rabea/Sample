using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorHero.CleanArchitecture.Infrastructure.Migrations
{
    public partial class FixColNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClaimAmount",
                schema: "CMS",
                table: "Claims",
                newName: "RequiredAmount");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RequiredAmount",
                schema: "CMS",
                table: "Claims",
                newName: "ClaimAmount");
        }
    }
}
