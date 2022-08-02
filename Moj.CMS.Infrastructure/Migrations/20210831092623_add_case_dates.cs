using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorHero.CleanArchitecture.Infrastructure.Migrations
{
    public partial class add_case_dates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IssueDate",
                schema: "CMS",
                table: "CaseDetails");

            migrationBuilder.DropColumn(
                name: "IssueDateHijri",
                schema: "CMS",
                table: "CaseDetails");

            migrationBuilder.DropColumn(
                name: "IssueTime",
                schema: "CMS",
                table: "CaseDetails");

            migrationBuilder.AddColumn<DateTime>(
                name: "AcceptanceDate",
                schema: "CMS",
                table: "Case",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AcceptanceDateHijri",
                schema: "CMS",
                table: "Case",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReceiveDate",
                schema: "CMS",
                table: "Case",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceiveDateHijri",
                schema: "CMS",
                table: "Case",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptanceDate",
                schema: "CMS",
                table: "Case");

            migrationBuilder.DropColumn(
                name: "AcceptanceDateHijri",
                schema: "CMS",
                table: "Case");

            migrationBuilder.DropColumn(
                name: "ReceiveDate",
                schema: "CMS",
                table: "Case");

            migrationBuilder.DropColumn(
                name: "ReceiveDateHijri",
                schema: "CMS",
                table: "Case");

            migrationBuilder.AddColumn<DateTime>(
                name: "IssueDate",
                schema: "CMS",
                table: "CaseDetails",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "IssueDateHijri",
                schema: "CMS",
                table: "CaseDetails",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "IssueTime",
                schema: "CMS",
                table: "CaseDetails",
                type: "time(7)",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
