using Microsoft.EntityFrameworkCore.Migrations;

namespace Moj.CMS.Infrastructure.Migrations
{
    public partial class Use_Division_Court_Judge_IDs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourtCode",
                schema: "CMS",
                table: "CaseDetails");

            migrationBuilder.DropColumn(
                name: "DivisionCode",
                schema: "CMS",
                table: "CaseDetails");

            migrationBuilder.DropColumn(
                name: "JudgeCode",
                schema: "CMS",
                table: "CaseDetails");

            migrationBuilder.AddColumn<int>(
                name: "CourtId",
                schema: "CMS",
                table: "CaseDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DivisionId",
                schema: "CMS",
                table: "CaseDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "JudgeId",
                schema: "CMS",
                table: "CaseDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourtId",
                schema: "CMS",
                table: "CaseDetails");

            migrationBuilder.DropColumn(
                name: "DivisionId",
                schema: "CMS",
                table: "CaseDetails");

            migrationBuilder.DropColumn(
                name: "JudgeId",
                schema: "CMS",
                table: "CaseDetails");

            migrationBuilder.AddColumn<string>(
                name: "CourtCode",
                schema: "CMS",
                table: "CaseDetails",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DivisionCode",
                schema: "CMS",
                table: "CaseDetails",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "JudgeCode",
                schema: "CMS",
                table: "CaseDetails",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");
        }
    }
}
