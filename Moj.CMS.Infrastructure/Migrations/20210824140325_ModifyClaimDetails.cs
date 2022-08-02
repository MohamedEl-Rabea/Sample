using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorHero.CleanArchitecture.Infrastructure.Migrations
{
    public partial class ModifyClaimDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialClaimDetails_Claims_ClaimId",
                schema: "CMS",
                table: "FinancialClaimDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FinancialClaimDetails",
                schema: "CMS",
                table: "FinancialClaimDetails");

            migrationBuilder.RenameTable(
                name: "FinancialClaimDetails",
                schema: "CMS",
                newName: "ClaimDetails",
                newSchema: "CMS");

            migrationBuilder.RenameIndex(
                name: "IX_FinancialClaimDetails_ClaimId",
                schema: "CMS",
                table: "ClaimDetails",
                newName: "IX_ClaimDetails_ClaimId");

            migrationBuilder.AlterColumn<string>(
                name: "CurrencyIso",
                schema: "CMS",
                table: "ClaimDetails",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClaimDetails",
                schema: "CMS",
                table: "ClaimDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClaimDetails_Claims_ClaimId",
                schema: "CMS",
                table: "ClaimDetails",
                column: "ClaimId",
                principalSchema: "CMS",
                principalTable: "Claims",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClaimDetails_Claims_ClaimId",
                schema: "CMS",
                table: "ClaimDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClaimDetails",
                schema: "CMS",
                table: "ClaimDetails");

            migrationBuilder.RenameTable(
                name: "ClaimDetails",
                schema: "CMS",
                newName: "FinancialClaimDetails",
                newSchema: "CMS");

            migrationBuilder.RenameIndex(
                name: "IX_ClaimDetails_ClaimId",
                schema: "CMS",
                table: "FinancialClaimDetails",
                newName: "IX_FinancialClaimDetails_ClaimId");

            migrationBuilder.AlterColumn<string>(
                name: "CurrencyIso",
                schema: "CMS",
                table: "FinancialClaimDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FinancialClaimDetails",
                schema: "CMS",
                table: "FinancialClaimDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialClaimDetails_Claims_ClaimId",
                schema: "CMS",
                table: "FinancialClaimDetails",
                column: "ClaimId",
                principalSchema: "CMS",
                principalTable: "Claims",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
