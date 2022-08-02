using Microsoft.EntityFrameworkCore.Migrations;

namespace Moj.CMS.Infrastructure.Migrations
{
    public partial class renameIsCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsCompany",
                schema: "CMS",
                table: "Party",
                newName: "IsIndividual");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsIndividual",
                schema: "CMS",
                table: "Party",
                newName: "IsCompany");
        }
    }
}
