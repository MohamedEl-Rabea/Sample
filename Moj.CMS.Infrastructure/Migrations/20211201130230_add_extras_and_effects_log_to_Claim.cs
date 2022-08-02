using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorHero.CleanArchitecture.Infrastructure.Migrations
{
    public partial class add_extras_and_effects_log_to_Claim : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClaimEffectsAndDiscounts_Claims_ClaimId",
                schema: "CMS",
                table: "ClaimEffectsAndDiscounts");

            migrationBuilder.DropTable(
                name: "FinancialAdjustmentReasonsLookup",
                schema: "CMS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClaimEffectsAndDiscounts",
                schema: "CMS",
                table: "ClaimEffectsAndDiscounts");

            migrationBuilder.DropColumn(
                name: "NewAmount",
                schema: "CMS",
                table: "ClaimHistory");

            migrationBuilder.DropColumn(
                name: "NewRemainingAmount",
                schema: "CMS",
                table: "ClaimHistory");

            migrationBuilder.DropColumn(
                name: "PartyNumber",
                schema: "CMS",
                table: "ClaimHistory");

            migrationBuilder.DropColumn(
                name: "ReferenceNumber",
                schema: "CMS",
                table: "ClaimHistory");

            migrationBuilder.DropColumn(
                name: "EffectReason",
                schema: "CMS",
                table: "ClaimEffectsAndDiscounts");

            migrationBuilder.RenameTable(
                name: "ClaimEffectsAndDiscounts",
                schema: "CMS",
                newName: "ClaimExtras",
                newSchema: "CMS");

            migrationBuilder.RenameColumn(
                name: "InitialAmount",
                schema: "CMS",
                table: "Claims",
                newName: "TotalRequiredAmount");

            migrationBuilder.RenameColumn(
                name: "OldRemainingAmount",
                schema: "CMS",
                table: "ClaimHistory",
                newName: "TotalAmountBefore");

            migrationBuilder.RenameColumn(
                name: "OldAmount",
                schema: "CMS",
                table: "ClaimHistory",
                newName: "TotalAmountAfter");

            migrationBuilder.RenameColumn(
                name: "AdjustmentReason",
                schema: "CMS",
                table: "ClaimHistory",
                newName: "EffectType");

            migrationBuilder.RenameColumn(
                name: "OldTotalAmount",
                schema: "CMS",
                table: "ClaimExtras",
                newName: "Remaining");

            migrationBuilder.RenameColumn(
                name: "NewTotalAmount",
                schema: "CMS",
                table: "ClaimExtras",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "EffectType",
                schema: "CMS",
                table: "ClaimExtras",
                newName: "Type");

            migrationBuilder.RenameIndex(
                name: "IX_ClaimEffectsAndDiscounts_ClaimId",
                schema: "CMS",
                table: "ClaimExtras",
                newName: "IX_ClaimExtras_ClaimId");

            migrationBuilder.AddColumn<bool>(
                name: "IsIncrementOnClaim",
                schema: "CMS",
                table: "FinancialEffectsLookup",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "BasicAmount",
                schema: "CMS",
                table: "Claims",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalRemainingAmount",
                schema: "CMS",
                table: "Claims",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                schema: "CMS",
                table: "ClaimExtras",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "ClaimExtras",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClaimExtras",
                schema: "CMS",
                table: "ClaimExtras",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClaimExtras_Claims_ClaimId",
                schema: "CMS",
                table: "ClaimExtras",
                column: "ClaimId",
                principalSchema: "CMS",
                principalTable: "Claims",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClaimExtras_Claims_ClaimId",
                schema: "CMS",
                table: "ClaimExtras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClaimExtras",
                schema: "CMS",
                table: "ClaimExtras");

            migrationBuilder.DropColumn(
                name: "IsIncrementOnClaim",
                schema: "CMS",
                table: "FinancialEffectsLookup");

            migrationBuilder.DropColumn(
                name: "BasicAmount",
                schema: "CMS",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "TotalRemainingAmount",
                schema: "CMS",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                schema: "CMS",
                table: "ClaimExtras");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "ClaimExtras");

            migrationBuilder.RenameTable(
                name: "ClaimExtras",
                schema: "CMS",
                newName: "ClaimEffectsAndDiscounts",
                newSchema: "CMS");

            migrationBuilder.RenameColumn(
                name: "TotalRequiredAmount",
                schema: "CMS",
                table: "Claims",
                newName: "InitialAmount");

            migrationBuilder.RenameColumn(
                name: "TotalAmountBefore",
                schema: "CMS",
                table: "ClaimHistory",
                newName: "OldRemainingAmount");

            migrationBuilder.RenameColumn(
                name: "TotalAmountAfter",
                schema: "CMS",
                table: "ClaimHistory",
                newName: "OldAmount");

            migrationBuilder.RenameColumn(
                name: "EffectType",
                schema: "CMS",
                table: "ClaimHistory",
                newName: "AdjustmentReason");

            migrationBuilder.RenameColumn(
                name: "Remaining",
                schema: "CMS",
                table: "ClaimEffectsAndDiscounts",
                newName: "OldTotalAmount");

            migrationBuilder.RenameColumn(
                name: "Amount",
                schema: "CMS",
                table: "ClaimEffectsAndDiscounts",
                newName: "NewTotalAmount");

            migrationBuilder.RenameColumn(
                name: "Type",
                schema: "CMS",
                table: "ClaimEffectsAndDiscounts",
                newName: "EffectType");

            migrationBuilder.RenameIndex(
                name: "IX_ClaimExtras_ClaimId",
                schema: "CMS",
                table: "ClaimEffectsAndDiscounts",
                newName: "IX_ClaimEffectsAndDiscounts_ClaimId");

            migrationBuilder.AddColumn<decimal>(
                name: "NewAmount",
                schema: "CMS",
                table: "ClaimHistory",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "NewRemainingAmount",
                schema: "CMS",
                table: "ClaimHistory",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "PartyNumber",
                schema: "CMS",
                table: "ClaimHistory",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReferenceNumber",
                schema: "CMS",
                table: "ClaimHistory",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "EffectReason",
                schema: "CMS",
                table: "ClaimEffectsAndDiscounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClaimEffectsAndDiscounts",
                schema: "CMS",
                table: "ClaimEffectsAndDiscounts",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "FinancialAdjustmentReasonsLookup",
                schema: "CMS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialAdjustmentReasonsLookup", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ClaimEffectsAndDiscounts_Claims_ClaimId",
                schema: "CMS",
                table: "ClaimEffectsAndDiscounts",
                column: "ClaimId",
                principalSchema: "CMS",
                principalTable: "Claims",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
