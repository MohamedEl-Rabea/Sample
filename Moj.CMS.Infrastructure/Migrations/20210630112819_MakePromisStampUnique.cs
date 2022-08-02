using Microsoft.EntityFrameworkCore.Migrations;

namespace Moj.CMS.Infrastructure.Migrations
{
    public partial class MakePromisStampUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Promissory_PromissoryStampNumber",
                schema: "CMS",
                table: "Promissory",
                column: "PromissoryStampNumber",
                unique: true,
                filter: "[PromissoryStampNumber] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Promissory_PromissoryStampNumber",
                schema: "CMS",
                table: "Promissory");
        }
    }
}
