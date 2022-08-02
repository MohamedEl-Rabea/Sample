using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Moj.CMS.Infrastructure.Migrations
{
    public partial class AddClaimsAggregate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "ClaimSequenceHilo",
                schema: "CMS",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "Claim",
                schema: "CMS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    FinancialClaimNumber = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CaseId = table.Column<int>(type: "int", nullable: false),
                    PromissoryId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountLC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ComplaintPartyId = table.Column<int>(type: "int", nullable: false),
                    IsOriginPartyComplaint = table.Column<bool>(type: "bit", nullable: false),
                    IbanOfComplaint = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    AccusedPartyId = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claim", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Claim",
                schema: "CMS");

            migrationBuilder.DropSequence(
                name: "ClaimSequenceHilo",
                schema: "CMS");
        }
    }
}
