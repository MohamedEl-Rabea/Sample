using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorHero.CleanArchitecture.Infrastructure.Migrations
{
    public partial class Add_IsActive_To_Lookups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DivisionStatus",
                schema: "CMS",
                table: "Divisions",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "Status",
                schema: "CMS",
                table: "Courts",
                newName: "IsActive");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "CMS",
                table: "Judges",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "CMS",
                table: "Areas",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "CMS",
                table: "Judges");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "CMS",
                table: "Areas");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                schema: "CMS",
                table: "Divisions",
                newName: "DivisionStatus");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                schema: "CMS",
                table: "Courts",
                newName: "Status");
        }
    }
}
