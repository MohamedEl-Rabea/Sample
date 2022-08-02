using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorHero.CleanArchitecture.Infrastructure.Migrations
{
    public partial class Update_Promissory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DebtTypeId",
                schema: "CMS",
                table: "Promissory");

            migrationBuilder.AddColumn<int>(
                name: "DebtTypeId",
                schema: "CMS",
                table: "Claims",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DebtTypeId",
                schema: "CMS",
                table: "Claims");

            migrationBuilder.AddColumn<int>(
                name: "DebtTypeId",
                schema: "CMS",
                table: "Promissory",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
