using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorHero.CleanArchitecture.Infrastructure.Migrations
{
    public partial class ModifyClaim : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ComplaintPartyIdentityNumber",
                schema: "CMS",
                table: "Claims",
                newName: "ComplaintPartyNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ComplaintPartyNumber",
                schema: "CMS",
                table: "Claims",
                newName: "ComplaintPartyIdentityNumber");
        }
    }
}
