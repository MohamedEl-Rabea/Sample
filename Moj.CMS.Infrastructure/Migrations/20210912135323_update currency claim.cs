using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorHero.CleanArchitecture.Infrastructure.Migrations
{
    public partial class updatecurrencyclaim : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PartyNumber",
                schema: "CMS",
                table: "ClaimDetails",
                newName: "AccusedIdentityNumber");

            migrationBuilder.AlterColumn<string>(
                name: "CreatorUserId",
                schema: "CMS",
                table: "ClaimDetailsHistory",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccusedIdentityNumber",
                schema: "CMS",
                table: "ClaimDetails",
                newName: "PartyNumber");

            migrationBuilder.AlterColumn<string>(
                name: "CreatorUserId",
                schema: "CMS",
                table: "ClaimDetailsHistory",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);
        }
    }
}
