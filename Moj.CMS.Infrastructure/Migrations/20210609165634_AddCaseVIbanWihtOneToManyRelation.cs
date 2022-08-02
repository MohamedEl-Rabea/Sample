using Microsoft.EntityFrameworkCore.Migrations;

namespace Moj.CMS.Infrastructure.Migrations
{
    public partial class AddCaseVIbanWihtOneToManyRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VIban_CaseId",
                schema: "CMS",
                table: "VIban");

            migrationBuilder.AlterColumn<bool>(
                name: "IsIndividual",
                schema: "CMS",
                table: "Party",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VIban_CaseId",
                schema: "CMS",
                table: "VIban",
                column: "CaseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VIban_CaseId",
                schema: "CMS",
                table: "VIban");

            migrationBuilder.AlterColumn<bool>(
                name: "IsIndividual",
                schema: "CMS",
                table: "Party",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.CreateIndex(
                name: "IX_VIban_CaseId",
                schema: "CMS",
                table: "VIban",
                column: "CaseId",
                unique: true);
        }
    }
}
