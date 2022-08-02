using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorHero.CleanArchitecture.Infrastructure.Migrations
{
    public partial class renamebankaccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccounts_Courts_CourtId",
                schema: "CMS",
                table: "BankAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BankAccounts",
                schema: "CMS",
                table: "BankAccounts");

            migrationBuilder.RenameTable(
                name: "BankAccounts",
                schema: "CMS",
                newName: "CourtBankAccounts",
                newSchema: "CMS");

            migrationBuilder.RenameIndex(
                name: "IX_BankAccounts_CourtId",
                schema: "CMS",
                table: "CourtBankAccounts",
                newName: "IX_CourtBankAccounts_CourtId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourtBankAccounts",
                schema: "CMS",
                table: "CourtBankAccounts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourtBankAccounts_Courts_CourtId",
                schema: "CMS",
                table: "CourtBankAccounts",
                column: "CourtId",
                principalSchema: "CMS",
                principalTable: "Courts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourtBankAccounts_Courts_CourtId",
                schema: "CMS",
                table: "CourtBankAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourtBankAccounts",
                schema: "CMS",
                table: "CourtBankAccounts");

            migrationBuilder.RenameTable(
                name: "CourtBankAccounts",
                schema: "CMS",
                newName: "BankAccounts",
                newSchema: "CMS");

            migrationBuilder.RenameIndex(
                name: "IX_CourtBankAccounts_CourtId",
                schema: "CMS",
                table: "BankAccounts",
                newName: "IX_BankAccounts_CourtId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BankAccounts",
                schema: "CMS",
                table: "BankAccounts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccounts_Courts_CourtId",
                schema: "CMS",
                table: "BankAccounts",
                column: "CourtId",
                principalSchema: "CMS",
                principalTable: "Courts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
