using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorHero.CleanArchitecture.Infrastructure.Migrations
{
    public partial class update_parties_logic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommercialRegistry",
                schema: "CMS",
                table: "Party");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                schema: "CMS",
                table: "Party");

            migrationBuilder.DropColumn(
                name: "FirstName",
                schema: "CMS",
                table: "Party");

            migrationBuilder.DropColumn(
                name: "IsIndividual",
                schema: "CMS",
                table: "Party");

            migrationBuilder.DropColumn(
                name: "LastName",
                schema: "CMS",
                table: "Party");

            migrationBuilder.DropColumn(
                name: "NationalityId",
                schema: "CMS",
                table: "Party");

            migrationBuilder.DropColumn(
                name: "PartyStatusId",
                schema: "CMS",
                table: "Party");

            migrationBuilder.DropColumn(
                name: "SecondName",
                schema: "CMS",
                table: "Party");

            migrationBuilder.DropColumn(
                name: "ShortName",
                schema: "CMS",
                table: "Party");

            migrationBuilder.DropColumn(
                name: "ThirdName",
                schema: "CMS",
                table: "Party");

            migrationBuilder.AlterColumn<string>(
                name: "PartyIdentityNumber",
                schema: "CMS",
                table: "PartyIdentity",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Gender",
                schema: "CMS",
                table: "Party",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                schema: "CMS",
                table: "Party",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                schema: "CMS",
                table: "Party",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                schema: "CMS",
                table: "Party",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PartyNumber",
                schema: "CMS",
                table: "Party",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "InputDetails",
                schema: "EventLogging",
                table: "Logs",
                type: "varchar(Max)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                schema: "CMS",
                table: "Party");

            migrationBuilder.DropColumn(
                name: "Nationality",
                schema: "CMS",
                table: "Party");

            migrationBuilder.DropColumn(
                name: "PartyNumber",
                schema: "CMS",
                table: "Party");

            migrationBuilder.AlterColumn<string>(
                name: "PartyIdentityNumber",
                schema: "CMS",
                table: "PartyIdentity",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<int>(
                name: "Gender",
                schema: "CMS",
                table: "Party",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                schema: "CMS",
                table: "Party",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60);

            migrationBuilder.AddColumn<string>(
                name: "CommercialRegistry",
                schema: "CMS",
                table: "Party",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                schema: "CMS",
                table: "Party",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                schema: "CMS",
                table: "Party",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsIndividual",
                schema: "CMS",
                table: "Party",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                schema: "CMS",
                table: "Party",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NationalityId",
                schema: "CMS",
                table: "Party",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PartyStatusId",
                schema: "CMS",
                table: "Party",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SecondName",
                schema: "CMS",
                table: "Party",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortName",
                schema: "CMS",
                table: "Party",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThirdName",
                schema: "CMS",
                table: "Party",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "InputDetails",
                schema: "EventLogging",
                table: "Logs",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(Max)",
                oldMaxLength: 256,
                oldNullable: true);
        }
    }
}
