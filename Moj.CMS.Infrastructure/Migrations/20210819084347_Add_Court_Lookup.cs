using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorHero.CleanArchitecture.Infrastructure.Migrations
{
    public partial class Add_Court_Lookup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "CMS",
                table: "Courts",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AreaCode",
                schema: "CMS",
                table: "Courts",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankAccounts",
                schema: "CMS",
                table: "Courts",
                type: "nvarchar(2500)",
                maxLength: 2500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "CMS",
                table: "Courts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                schema: "CMS",
                table: "Courts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AreaCode",
                schema: "CMS",
                table: "Courts");

            migrationBuilder.DropColumn(
                name: "BankAccounts",
                schema: "CMS",
                table: "Courts");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "CMS",
                table: "Courts");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "CMS",
                table: "Courts");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "CMS",
                table: "Courts",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldNullable: true);
        }
    }
}
