using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorHero.CleanArchitecture.Infrastructure.Migrations
{
    public partial class Add_HiLo_In_Sadad_Invoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SadadCashNotifications",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "SadadPaymentNotifications",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "SadadInvoices",
                schema: "CMS");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SadadInvoices",
                schema: "CMS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClaimNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeleterUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinBillableAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartyNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VIban = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SadadInvoices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SadadCashNotifications",
                schema: "CMS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CashAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SadadId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SadadCashNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SadadCashNotifications_SadadInvoices_SadadId",
                        column: x => x.SadadId,
                        principalSchema: "CMS",
                        principalTable: "SadadInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SadadPaymentNotifications",
                schema: "CMS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaidAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SadadId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SadadPaymentNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SadadPaymentNotifications_SadadInvoices_SadadId",
                        column: x => x.SadadId,
                        principalSchema: "CMS",
                        principalTable: "SadadInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SadadCashNotifications_SadadId",
                schema: "CMS",
                table: "SadadCashNotifications",
                column: "SadadId");

            migrationBuilder.CreateIndex(
                name: "IX_SadadPaymentNotifications_SadadId",
                schema: "CMS",
                table: "SadadPaymentNotifications",
                column: "SadadId");
        }
    }
}
