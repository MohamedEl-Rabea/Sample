using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Moj.CMS.Infrastructure.Migrations
{
    public partial class Add_Audting_Info_For_All_Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShouldCreateIban",
                schema: "CMS",
                table: "Case",
                newName: "IsCreateIban");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                schema: "CMS",
                table: "PromissoryTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                schema: "CMS",
                table: "PromissoryTypes",
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
                name: "CreationTime",
                schema: "CMS",
                table: "PartyTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                schema: "CMS",
                table: "PartyTypes",
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
                name: "CreationTime",
                schema: "CMS",
                table: "PartyStatuses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                schema: "CMS",
                table: "PartyStatuses",
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
                name: "CreationTime",
                schema: "CMS",
                table: "PartyLocations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                schema: "CMS",
                table: "PartyLocations",
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
                name: "CreationTime",
                schema: "CMS",
                table: "PartyIdentityTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                schema: "CMS",
                table: "PartyIdentityTypes",
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
                name: "CreationTime",
                schema: "CMS",
                table: "PartyFinancialTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                schema: "CMS",
                table: "PartyFinancialTypes",
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
                name: "CreationTime",
                schema: "CMS",
                table: "Nationality",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                schema: "CMS",
                table: "Nationality",
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
                name: "CreationTime",
                schema: "CMS",
                table: "Judges",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                schema: "CMS",
                table: "Judges",
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
                name: "CreationTime",
                schema: "CMS",
                table: "Divisions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                schema: "CMS",
                table: "Divisions",
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
                name: "CreationTime",
                schema: "CMS",
                table: "Currency",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                schema: "CMS",
                table: "Currency",
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
                name: "CreationTime",
                schema: "CMS",
                table: "Courts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                schema: "CMS",
                table: "Courts",
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
                name: "CreationTime",
                schema: "CMS",
                table: "CaseTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                schema: "CMS",
                table: "CaseTypes",
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
                name: "CreationTime",
                schema: "CMS",
                table: "CaseStatuses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                schema: "CMS",
                table: "CaseStatuses",
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
                name: "CreationTime",
                schema: "CMS",
                table: "CaseParty",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                schema: "CMS",
                table: "CaseParty",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                schema: "CMS",
                table: "CaseDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                schema: "CMS",
                table: "CaseDetails",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                schema: "CMS",
                table: "AuditTrails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                schema: "CMS",
                table: "AuditTrails",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                schema: "CMS",
                table: "PromissoryTypes");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                schema: "CMS",
                table: "PromissoryTypes");

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
                name: "CreationTime",
                schema: "CMS",
                table: "PartyTypes");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                schema: "CMS",
                table: "PartyTypes");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                schema: "CMS",
                table: "PartyTypes");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "PartyTypes");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                schema: "CMS",
                table: "PartyStatuses");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                schema: "CMS",
                table: "PartyStatuses");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                schema: "CMS",
                table: "PartyStatuses");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "PartyStatuses");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                schema: "CMS",
                table: "PartyLocations");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                schema: "CMS",
                table: "PartyLocations");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                schema: "CMS",
                table: "PartyLocations");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "PartyLocations");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                schema: "CMS",
                table: "PartyIdentityTypes");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                schema: "CMS",
                table: "PartyIdentityTypes");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                schema: "CMS",
                table: "PartyIdentityTypes");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "PartyIdentityTypes");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                schema: "CMS",
                table: "PartyFinancialTypes");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                schema: "CMS",
                table: "PartyFinancialTypes");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                schema: "CMS",
                table: "PartyFinancialTypes");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "PartyFinancialTypes");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                schema: "CMS",
                table: "Nationality");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                schema: "CMS",
                table: "Nationality");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                schema: "CMS",
                table: "Nationality");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "Nationality");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                schema: "CMS",
                table: "Judges");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                schema: "CMS",
                table: "Judges");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                schema: "CMS",
                table: "Judges");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "Judges");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                schema: "CMS",
                table: "Divisions");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                schema: "CMS",
                table: "Divisions");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                schema: "CMS",
                table: "Divisions");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "Divisions");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                schema: "CMS",
                table: "Currency");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                schema: "CMS",
                table: "Currency");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                schema: "CMS",
                table: "Currency");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "Currency");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                schema: "CMS",
                table: "Courts");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                schema: "CMS",
                table: "Courts");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                schema: "CMS",
                table: "Courts");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "Courts");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                schema: "CMS",
                table: "CaseTypes");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                schema: "CMS",
                table: "CaseTypes");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                schema: "CMS",
                table: "CaseTypes");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "CaseTypes");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                schema: "CMS",
                table: "CaseStatuses");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                schema: "CMS",
                table: "CaseStatuses");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                schema: "CMS",
                table: "CaseStatuses");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "CaseStatuses");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                schema: "CMS",
                table: "CaseParty");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                schema: "CMS",
                table: "CaseParty");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                schema: "CMS",
                table: "CaseDetails");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                schema: "CMS",
                table: "CaseDetails");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                schema: "CMS",
                table: "AuditTrails");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                schema: "CMS",
                table: "AuditTrails");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                schema: "CMS",
                table: "AuditTrails");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "AuditTrails");

            migrationBuilder.RenameColumn(
                name: "IsCreateIban",
                schema: "CMS",
                table: "Case",
                newName: "ShouldCreateIban");
        }
    }
}
