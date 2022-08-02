using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorHero.CleanArchitecture.Infrastructure.Migrations
{
    public partial class replacecasedetailslookupsidswithcodes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DivisionCode",
                schema: "CMS",
                table: "CaseDetails",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JudgeCode",
                schema: "CMS",
                table: "CaseDetails",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
