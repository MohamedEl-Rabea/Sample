using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorHero.CleanArchitecture.Infrastructure.Migrations
{
    public partial class ChangeDivisioncolumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourtId",
                schema: "CMS",
                table: "Divisions");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "CMS",
                table: "Divisions",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CourtCode",
                schema: "CMS",
                table: "Divisions",
                type: "int",
                maxLength: 15,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "DivisionStatus",
                schema: "CMS",
                table: "Divisions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "CMS",
                table: "Divisions",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourtCode",
                schema: "CMS",
                table: "Divisions");

            migrationBuilder.DropColumn(
                name: "DivisionStatus",
                schema: "CMS",
                table: "Divisions");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "CMS",
                table: "Divisions");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "CMS",
                table: "Divisions",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CourtId",
                schema: "CMS",
                table: "Divisions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
