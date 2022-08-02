using Microsoft.EntityFrameworkCore.Migrations;

namespace Moj.CMS.Infrastructure.Migrations
{
    public partial class AddDescriptiontoCasaStatuses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "CMS",
                table: "CaseStatuses",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                schema: "CMS",
                table: "CaseStatuses");
        }
    }
}
