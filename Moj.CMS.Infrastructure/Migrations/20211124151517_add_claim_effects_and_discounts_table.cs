using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorHero.CleanArchitecture.Infrastructure.Migrations
{
    public partial class add_claim_effects_and_discounts_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RequiredAmount",
                schema: "CMS",
                table: "Claims",
                newName: "TotalAmount");

            migrationBuilder.AddColumn<decimal>(
                name: "InitialAmount",
                schema: "CMS",
                table: "Claims",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "ClaimEffectsAndDiscounts",
                schema: "CMS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    OldTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NewTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EffectType = table.Column<int>(type: "int", nullable: false),
                    EffectReason = table.Column<int>(type: "int", nullable: false),
                    CreatorUserId = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClaimId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimEffectsAndDiscounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClaimEffectsAndDiscounts_Claims_ClaimId",
                        column: x => x.ClaimId,
                        principalSchema: "CMS",
                        principalTable: "Claims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FinancialEffectsLookup",
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
                    table.PrimaryKey("PK_FinancialEffectsLookup", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClaimEffectsAndDiscounts_ClaimId",
                schema: "CMS",
                table: "ClaimEffectsAndDiscounts",
                column: "ClaimId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClaimEffectsAndDiscounts",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "FinancialEffectsLookup",
                schema: "CMS");

            migrationBuilder.DropColumn(
                name: "InitialAmount",
                schema: "CMS",
                table: "Claims");

            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                schema: "CMS",
                table: "Claims",
                newName: "RequiredAmount");
        }
    }
}
