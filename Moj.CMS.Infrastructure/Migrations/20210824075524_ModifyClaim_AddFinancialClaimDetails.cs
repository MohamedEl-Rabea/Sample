using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorHero.CleanArchitecture.Infrastructure.Migrations
{
    public partial class ModifyClaim_AddFinancialClaimDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Claim",
                schema: "CMS",
                table: "Claim");

            migrationBuilder.DropColumn(
                name: "AccusedPartyId",
                schema: "CMS",
                table: "Claim");

            migrationBuilder.DropColumn(
                name: "Amount",
                schema: "CMS",
                table: "Claim");

            migrationBuilder.DropColumn(
                name: "AmountLC",
                schema: "CMS",
                table: "Claim");

            migrationBuilder.DropColumn(
                name: "CaseId",
                schema: "CMS",
                table: "Claim");

            migrationBuilder.DropColumn(
                name: "ComplaintPartyId",
                schema: "CMS",
                table: "Claim");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "CMS",
                table: "Claim");

            migrationBuilder.DropColumn(
                name: "IsOriginPartyComplaint",
                schema: "CMS",
                table: "Claim");

            migrationBuilder.DropColumn(
                name: "Notes",
                schema: "CMS",
                table: "Claim");

            migrationBuilder.DropColumn(
                name: "PromissoryId",
                schema: "CMS",
                table: "Claim");

            migrationBuilder.RenameTable(
                name: "Claim",
                schema: "CMS",
                newName: "Claims",
                newSchema: "CMS");

            migrationBuilder.RenameColumn(
                name: "IbanOfComplaint",
                schema: "CMS",
                table: "Claims",
                newName: "PromissoryNumber");

            migrationBuilder.RenameColumn(
                name: "FinancialClaimNumber",
                schema: "CMS",
                table: "Claims",
                newName: "ComplaintPartyIdentityNumber");

            migrationBuilder.AddColumn<string>(
                name: "CaseNumber",
                schema: "CMS",
                table: "Claims",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ClaimDate",
                schema: "CMS",
                table: "Claims",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ClaimNumber",
                schema: "CMS",
                table: "Claims",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CurrencyIso",
                schema: "CMS",
                table: "Claims",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RemainingAmount",
                schema: "CMS",
                table: "Claims",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RequiredAmount",
                schema: "CMS",
                table: "Claims",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Claims",
                schema: "CMS",
                table: "Claims",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "FinancialClaimDetails",
                schema: "CMS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccusedIdentityNumber = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CurrencyIso = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequiredAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BillingAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    ClaimId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialClaimDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinancialClaimDetails_Claims_ClaimId",
                        column: x => x.ClaimId,
                        principalSchema: "CMS",
                        principalTable: "Claims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinancialClaimDetails_ClaimId",
                schema: "CMS",
                table: "FinancialClaimDetails",
                column: "ClaimId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinancialClaimDetails",
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

            migrationBuilder.DropColumn(
                name: "RequiredAmount",
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
                name: "ComplaintPartyIdentityNumber",
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
