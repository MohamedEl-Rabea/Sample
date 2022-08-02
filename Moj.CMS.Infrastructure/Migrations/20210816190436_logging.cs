using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorHero.CleanArchitecture.Infrastructure.Migrations
{
    public partial class logging : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                schema: "CMS",
                table: "RequestTerminationReasons");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "RequestTerminationReasons");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                schema: "CMS",
                table: "PromissoryTypes");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "PromissoryTypes");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                schema: "CMS",
                table: "Promissory");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                schema: "CMS",
                table: "Promissory");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                schema: "CMS",
                table: "Promissory");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "Promissory");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                schema: "CMS",
                table: "PartyTypes");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "PartyTypes");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                schema: "CMS",
                table: "PartyStatuses");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "PartyStatuses");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                schema: "CMS",
                table: "PartyRoles");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "PartyRoles");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                schema: "CMS",
                table: "PartyLocations");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "PartyLocations");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                schema: "CMS",
                table: "PartyIdentityTypes");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "PartyIdentityTypes");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                schema: "CMS",
                table: "PartyFinancialTypes");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "PartyFinancialTypes");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                schema: "CMS",
                table: "Nationality");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "Nationality");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                schema: "CMS",
                table: "Judges");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "Judges");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                schema: "CMS",
                table: "Divisions");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "Divisions");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                schema: "CMS",
                table: "Currency");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "Currency");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                schema: "CMS",
                table: "Courts");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "Courts");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                schema: "CMS",
                table: "CaseTypes");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "CaseTypes");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                schema: "CMS",
                table: "CaseStatuses");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "CaseStatuses");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                schema: "CMS",
                table: "AuditTrails");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "AuditTrails");

            migrationBuilder.EnsureSchema(
                name: "EventLogging");

            migrationBuilder.CreateTable(
                name: "Logs",
                schema: "EventLogging",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Exception = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    RequestId = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    RequestName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    InputDetails = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs",
                schema: "EventLogging");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                schema: "CMS",
                table: "RequestTerminationReasons",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "RequestTerminationReasons",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                schema: "CMS",
                table: "PromissoryTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "PromissoryTypes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                schema: "CMS",
                table: "Promissory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                schema: "CMS",
                table: "Promissory",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                schema: "CMS",
                table: "Promissory",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "Promissory",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                schema: "CMS",
                table: "PartyTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "PartyTypes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                schema: "CMS",
                table: "PartyStatuses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "PartyStatuses",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                schema: "CMS",
                table: "PartyRoles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "PartyRoles",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                schema: "CMS",
                table: "PartyLocations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "PartyLocations",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                schema: "CMS",
                table: "PartyIdentityTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "PartyIdentityTypes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                schema: "CMS",
                table: "PartyFinancialTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "PartyFinancialTypes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                schema: "CMS",
                table: "Nationality",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "Nationality",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                schema: "CMS",
                table: "Judges",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "Judges",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                schema: "CMS",
                table: "Divisions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "Divisions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                schema: "CMS",
                table: "Currency",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "Currency",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                schema: "CMS",
                table: "Courts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "Courts",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                schema: "CMS",
                table: "CaseTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "CaseTypes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                schema: "CMS",
                table: "CaseStatuses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "CaseStatuses",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                schema: "CMS",
                table: "AuditTrails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "AuditTrails",
                type: "bigint",
                nullable: true);
        }
    }
}
