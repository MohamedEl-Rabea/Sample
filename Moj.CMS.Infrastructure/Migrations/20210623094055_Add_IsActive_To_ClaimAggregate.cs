using Microsoft.EntityFrameworkCore.Migrations;

namespace Moj.CMS.Infrastructure.Migrations
{
    public partial class Add_IsActive_To_ClaimAggregate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "CMS",
                table: "Claim",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "CMS",
                table: "Claim");
        }
    }
}
