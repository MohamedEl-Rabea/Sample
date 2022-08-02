using Microsoft.EntityFrameworkCore.Migrations;

namespace Moj.CMS.Infrastructure.Migrations
{
    public partial class AddPromissoryStampNumberCol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PromissoryStampNumber",
                schema: "CMS",
                table: "Promissory",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue:"non");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PromissoryStampNumber",
                schema: "CMS",
                table: "Promissory");
        }
    }
}
