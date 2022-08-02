using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorHero.CleanArchitecture.Infrastructure.Migrations
{
    public partial class FinancialAdjustmentHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.RenameColumn(
                        name: "RequiredAmount",
                        schema: "CMS",
                        table: "Claims",
                        newName: "ClaimAmount");


            migrationBuilder.CreateTable(
                name: "FinancialAdjustmentHistory",
                schema: "CMS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartyNumber = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CurrencyIso = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    NewAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OldAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AdjustmentReason = table.Column<int>(type: "int", nullable: false),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    isDebt = table.Column<bool>(type: "bit", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    ClaimId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialAdjustmentHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinancialAdjustmentHistory_Claims_ClaimId",
                        column: x => x.ClaimId,
                        principalSchema: "CMS",
                        principalTable: "Claims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });


            migrationBuilder.CreateIndex(
                name: "IX_FinancialAdjustmentHistory_ClaimId",
                schema: "CMS",
                table: "FinancialAdjustmentHistory",
                column: "ClaimId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClaimDetails",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "FinancialAdjustmentHistory",
                schema: "CMS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Claims",
                schema: "CMS",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "CaseNumber",
                schema: "CMS",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "ClaimAmount",
                schema: "CMS",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "ClaimDate",
                schema: "CMS",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "ClaimNumber",
                schema: "CMS",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "CurrencyIso",
                schema: "CMS",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "RemainingAmount",
                schema: "CMS",
                table: "Claims");

            migrationBuilder.RenameTable(
                name: "Claims",
                schema: "CMS",
                newName: "Claim",
                newSchema: "CMS");

            migrationBuilder.RenameColumn(
                name: "PromissoryNumber",
                schema: "CMS",
                table: "Claim",
                newName: "IbanOfComplaint");

            migrationBuilder.RenameColumn(
                name: "ComplaintPartyNumber",
                schema: "CMS",
                table: "Claim",
                newName: "FinancialClaimNumber");

            migrationBuilder.AddColumn<int>(
                name: "AccusedPartyId",
                schema: "CMS",
                table: "Claim",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                schema: "CMS",
                table: "Claim",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AmountLC",
                schema: "CMS",
                table: "Claim",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "CaseId",
                schema: "CMS",
                table: "Claim",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ComplaintPartyId",
                schema: "CMS",
                table: "Claim",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "CMS",
                table: "Claim",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsOriginPartyComplaint",
                schema: "CMS",
                table: "Claim",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                schema: "CMS",
                table: "Claim",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PromissoryId",
                schema: "CMS",
                table: "Claim",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Claim",
                schema: "CMS",
                table: "Claim",
                column: "Id");
        }
    }
}
