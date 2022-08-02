using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorHero.CleanArchitecture.Infrastructure.Migrations
{
    public partial class addpartyroletocaseparty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PartyRoleTypeId",
                schema: "CMS",
                table: "CaseParty",
                newName: "PromisoryId");

            migrationBuilder.RenameColumn(
                name: "PartyFinancialTypeId",
                schema: "CMS",
                table: "CaseParty",
                newName: "PartyRoleId");

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
            migrationBuilder.RenameColumn(
                name: "PromisoryId",
                schema: "CMS",
                table: "CaseParty",
                newName: "PartyRoleTypeId");

            migrationBuilder.RenameColumn(
                name: "PartyRoleId",
                schema: "CMS",
                table: "CaseParty",
                newName: "PartyFinancialTypeId");

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
