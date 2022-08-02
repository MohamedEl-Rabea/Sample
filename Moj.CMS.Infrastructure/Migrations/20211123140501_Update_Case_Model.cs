using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorHero.CleanArchitecture.Infrastructure.Migrations
{
    public partial class Update_Case_Model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nationality",
                schema: "CMS",
                table: "Party");

            migrationBuilder.DropColumn(
                name: "AcceptanceDateHijri",
                schema: "CMS",
                table: "Case");

            migrationBuilder.DropColumn(
                name: "ReceiveDateHijri",
                schema: "CMS",
                table: "Case");

            migrationBuilder.RenameColumn(
                name: "AcceptanceDate",
                schema: "CMS",
                table: "Case",
                newName: "JudgeAcceptanceDate");

            migrationBuilder.AddColumn<string>(
                name: "NationalityCode",
                schema: "CMS",
                table: "Party",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsApplicant",
                schema: "CMS",
                table: "CaseParty",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "CaseBasicAmount",
                schema: "CMS",
                table: "Case",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                schema: "CMS",
                table: "Case",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "RequiredAmount",
                schema: "CMS",
                table: "Case",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NationalityCode",
                schema: "CMS",
                table: "Party");

            migrationBuilder.DropColumn(
                name: "IsApplicant",
                schema: "CMS",
                table: "CaseParty");

            migrationBuilder.DropColumn(
                name: "CaseBasicAmount",
                schema: "CMS",
                table: "Case");

            migrationBuilder.DropColumn(
                name: "Currency",
                schema: "CMS",
                table: "Case");

            migrationBuilder.DropColumn(
                name: "RequiredAmount",
                schema: "CMS",
                table: "Case");

            migrationBuilder.RenameColumn(
                name: "JudgeAcceptanceDate",
                schema: "CMS",
                table: "Case",
                newName: "AcceptanceDate");

            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                schema: "CMS",
                table: "Party",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AcceptanceDateHijri",
                schema: "CMS",
                table: "Case",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceiveDateHijri",
                schema: "CMS",
                table: "Case",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);
        }
    }
}
