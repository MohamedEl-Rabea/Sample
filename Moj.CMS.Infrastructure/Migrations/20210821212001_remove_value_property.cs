using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorHero.CleanArchitecture.Infrastructure.Migrations
{
    public partial class remove_value_property : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                schema: "CMS",
                table: "RequestTerminationReasons");

            migrationBuilder.DropColumn(
                name: "Value",
                schema: "CMS",
                table: "PromissoryTypes");

            migrationBuilder.DropColumn(
                name: "Value",
                schema: "CMS",
                table: "PartyTypes");

            migrationBuilder.DropColumn(
                name: "Value",
                schema: "CMS",
                table: "PartyStatuses");

            migrationBuilder.DropColumn(
                name: "Value",
                schema: "CMS",
                table: "PartyRoles");

            migrationBuilder.DropColumn(
                name: "Value",
                schema: "CMS",
                table: "PartyLocations");

            migrationBuilder.DropColumn(
                name: "Value",
                schema: "CMS",
                table: "PartyIdentityTypes");

            migrationBuilder.DropColumn(
                name: "Value",
                schema: "CMS",
                table: "PartyFinancialTypes");

            migrationBuilder.DropColumn(
                name: "Value",
                schema: "CMS",
                table: "Nationality");

            migrationBuilder.DropColumn(
                name: "Value",
                schema: "CMS",
                table: "Judges");

            migrationBuilder.DropColumn(
                name: "Value",
                schema: "CMS",
                table: "Divisions");

            migrationBuilder.DropColumn(
                name: "Value",
                schema: "CMS",
                table: "Currency");

            migrationBuilder.DropColumn(
                name: "Value",
                schema: "CMS",
                table: "Courts");

            migrationBuilder.DropColumn(
                name: "Value",
                schema: "CMS",
                table: "CaseTypes");

            migrationBuilder.DropColumn(
                name: "Value",
                schema: "CMS",
                table: "CaseStatuses");

            migrationBuilder.DropColumn(
                name: "Value",
                schema: "CMS",
                table: "Areas");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "CMS",
                table: "RequestTerminationReasons",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "CMS",
                table: "PromissoryTypes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "CMS",
                table: "PartyTypes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "CMS",
                table: "PartyStatuses",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "CMS",
                table: "PartyRoles",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "CMS",
                table: "PartyLocations",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "CMS",
                table: "PartyIdentityTypes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "CMS",
                table: "PartyFinancialTypes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "CMS",
                table: "Nationality",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "CMS",
                table: "Divisions",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "CMS",
                table: "Currency",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "CMS",
                table: "CaseStatuses",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "CMS",
                table: "Areas",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                schema: "CMS",
                table: "RequestTerminationReasons");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "CMS",
                table: "PromissoryTypes");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "CMS",
                table: "PartyTypes");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "CMS",
                table: "PartyStatuses");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "CMS",
                table: "PartyRoles");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "CMS",
                table: "PartyLocations");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "CMS",
                table: "PartyIdentityTypes");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "CMS",
                table: "PartyFinancialTypes");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "CMS",
                table: "Nationality");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "CMS",
                table: "Currency");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "CMS",
                table: "CaseStatuses");

            migrationBuilder.AddColumn<string>(
                name: "Value",
                schema: "CMS",
                table: "RequestTerminationReasons",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Value",
                schema: "CMS",
                table: "PromissoryTypes",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Value",
                schema: "CMS",
                table: "PartyTypes",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Value",
                schema: "CMS",
                table: "PartyStatuses",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Value",
                schema: "CMS",
                table: "PartyRoles",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Value",
                schema: "CMS",
                table: "PartyLocations",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Value",
                schema: "CMS",
                table: "PartyIdentityTypes",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Value",
                schema: "CMS",
                table: "PartyFinancialTypes",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Value",
                schema: "CMS",
                table: "Nationality",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Value",
                schema: "CMS",
                table: "Judges",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "CMS",
                table: "Divisions",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Value",
                schema: "CMS",
                table: "Divisions",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Value",
                schema: "CMS",
                table: "Currency",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Value",
                schema: "CMS",
                table: "Courts",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Value",
                schema: "CMS",
                table: "CaseTypes",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Value",
                schema: "CMS",
                table: "CaseStatuses",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "CMS",
                table: "Areas",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Value",
                schema: "CMS",
                table: "Areas",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);
        }
    }
}
