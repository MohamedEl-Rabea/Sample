using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorHero.CleanArchitecture.Infrastructure.Migrations
{
    public partial class editclaims : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialAdjustmentHistory_Claims_ClaimId",
                schema: "CMS",
                table: "FinancialAdjustmentHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FinancialAdjustmentHistory",
                schema: "CMS",
                table: "FinancialAdjustmentHistory");

            migrationBuilder.DropColumn(
                name: "isDebt",
                schema: "CMS",
                table: "FinancialAdjustmentHistory");

            migrationBuilder.RenameTable(
                name: "FinancialAdjustmentHistory",
                schema: "CMS",
                newName: "ClaimHistory",
                newSchema: "CMS");

            migrationBuilder.RenameIndex(
                name: "IX_FinancialAdjustmentHistory_ClaimId",
                schema: "CMS",
                table: "ClaimHistory",
                newName: "IX_ClaimHistory_ClaimId");

            migrationBuilder.AddColumn<string>(
                name: "DeleterUserId",
                schema: "CMS",
                table: "ClaimDetails",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                schema: "CMS",
                table: "ClaimDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "CMS",
                table: "ClaimDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                schema: "CMS",
                table: "ClaimDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "ClaimDetails",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "NewRemainingAmount",
                schema: "CMS",
                table: "ClaimHistory",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OldRemainingAmount",
                schema: "CMS",
                table: "ClaimHistory",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClaimHistory",
                schema: "CMS",
                table: "ClaimHistory",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ClaimDetailsHistory",
                schema: "CMS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartyNumber = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CurrencyIso = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    NewRequiredAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NewBillingAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OldRequiredAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OldBillingAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ClaimDetailsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimDetailsHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClaimDetailsHistory_ClaimDetails_ClaimDetailsId",
                        column: x => x.ClaimDetailsId,
                        principalSchema: "CMS",
                        principalTable: "ClaimDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClaimDetailsHistory_ClaimDetailsId",
                schema: "CMS",
                table: "ClaimDetailsHistory",
                column: "ClaimDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClaimHistory_Claims_ClaimId",
                schema: "CMS",
                table: "ClaimHistory",
                column: "ClaimId",
                principalSchema: "CMS",
                principalTable: "Claims",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClaimHistory_Claims_ClaimId",
                schema: "CMS",
                table: "ClaimHistory");

            migrationBuilder.DropTable(
                name: "ClaimDetailsHistory",
                schema: "CMS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClaimHistory",
                schema: "CMS",
                table: "ClaimHistory");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                schema: "CMS",
                table: "ClaimDetails");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                schema: "CMS",
                table: "ClaimDetails");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "CMS",
                table: "ClaimDetails");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                schema: "CMS",
                table: "ClaimDetails");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "ClaimDetails");

            migrationBuilder.DropColumn(
                name: "NewRemainingAmount",
                schema: "CMS",
                table: "ClaimHistory");

            migrationBuilder.DropColumn(
                name: "OldRemainingAmount",
                schema: "CMS",
                table: "ClaimHistory");

            migrationBuilder.RenameTable(
                name: "ClaimHistory",
                schema: "CMS",
                newName: "FinancialAdjustmentHistory",
                newSchema: "CMS");

            migrationBuilder.RenameIndex(
                name: "IX_ClaimHistory_ClaimId",
                schema: "CMS",
                table: "FinancialAdjustmentHistory",
                newName: "IX_FinancialAdjustmentHistory_ClaimId");

            migrationBuilder.AddColumn<bool>(
                name: "isDebt",
                schema: "CMS",
                table: "FinancialAdjustmentHistory",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FinancialAdjustmentHistory",
                schema: "CMS",
                table: "FinancialAdjustmentHistory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialAdjustmentHistory_Claims_ClaimId",
                schema: "CMS",
                table: "FinancialAdjustmentHistory",
                column: "ClaimId",
                principalSchema: "CMS",
                principalTable: "Claims",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
