using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorHero.CleanArchitecture.Infrastructure.Migrations
{
    public partial class updateNaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccusedIdentityNumber",
                schema: "CMS",
                table: "ClaimDetails",
                newName: "PartyNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PartyNumber",
                schema: "CMS",
                table: "ClaimDetails",
                newName: "AccusedIdentityNumber");
        }
    }
}
