using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Moj.CMS.Infrastructure.Migrations
{
    public partial class Add_Case_Aggregate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "CaseSequenceHilo",
                schema: "CMS",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "Case",
                schema: "CMS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CaseNumber = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CaseTypeId = table.Column<int>(type: "int", nullable: false),
                    CaseStatusId = table.Column<int>(type: "int", nullable: false),
                    ShouldCreateIban = table.Column<bool>(type: "bit", nullable: false),
                    NumberOfAccused = table.Column<int>(type: "int", nullable: false),
                    NumberOfComplaint = table.Column<int>(type: "int", nullable: false),
                    NumberOfPromissory = table.Column<int>(type: "int", nullable: false),
                    NumberOfSadad = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Case", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CaseDetails",
                schema: "CMS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourtCode = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    DivisionCode = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    JudgeCode = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    IssueDate = table.Column<DateTime>(type: "date", nullable: false),
                    IssueDateHijri = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    IssueTime = table.Column<TimeSpan>(type: "time(7)", nullable: false),
                    CaseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CaseDetails_Case_CaseId",
                        column: x => x.CaseId,
                        principalSchema: "CMS",
                        principalTable: "Case",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CaseParty",
                schema: "CMS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartyId = table.Column<int>(type: "int", nullable: false),
                    PartyFinancialTypeId = table.Column<int>(type: "int", nullable: false),
                    CaseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseParty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CaseParty_Case_CaseId",
                        column: x => x.CaseId,
                        principalSchema: "CMS",
                        principalTable: "Case",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Promissory",
                schema: "CMS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PromissoryNumber = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    PromissoryTypeId = table.Column<int>(type: "int", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "date", nullable: false),
                    CaseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promissory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Promissory_Case_CaseId",
                        column: x => x.CaseId,
                        principalSchema: "CMS",
                        principalTable: "Case",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CaseDetails_CaseId",
                schema: "CMS",
                table: "CaseDetails",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_CaseParty_CaseId",
                schema: "CMS",
                table: "CaseParty",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Promissory_CaseId",
                schema: "CMS",
                table: "Promissory",
                column: "CaseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CaseDetails",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "CaseParty",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "Promissory",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "Case",
                schema: "CMS");

            migrationBuilder.DropSequence(
                name: "CaseSequenceHilo",
                schema: "CMS");
        }
    }
}
