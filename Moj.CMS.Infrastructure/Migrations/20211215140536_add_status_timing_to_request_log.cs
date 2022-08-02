using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorHero.CleanArchitecture.Infrastructure.Migrations
{
    public partial class add_status_timing_to_request_log : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BankName",
                schema: "CMS",
                table: "VIbans",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParentAccountNumber",
                schema: "CMS",
                table: "VIbans",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ProcessingTime",
                schema: "CMS",
                table: "VIbanRequestLog",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResponseTime",
                schema: "CMS",
                table: "VIbanRequestLog",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ScheduledTime",
                schema: "CMS",
                table: "VIbanRequestLog",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankName",
                schema: "CMS",
                table: "VIbans");

            migrationBuilder.DropColumn(
                name: "ParentAccountNumber",
                schema: "CMS",
                table: "VIbans");

            migrationBuilder.DropColumn(
                name: "ProcessingTime",
                schema: "CMS",
                table: "VIbanRequestLog");

            migrationBuilder.DropColumn(
                name: "ResponseTime",
                schema: "CMS",
                table: "VIbanRequestLog");

            migrationBuilder.DropColumn(
                name: "ScheduledTime",
                schema: "CMS",
                table: "VIbanRequestLog");
        }
    }
}
