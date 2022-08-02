using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorHero.CleanArchitecture.Infrastructure.Migrations
{
    public partial class CasePromissory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CaseNumber",
                schema: "CMS",
                table: "Promissory");

            migrationBuilder.CreateTable(
                name: "CasePromissory",
                schema: "CMS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PromissoryNumber = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    CaseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CasePromissory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CasePromissory_Case_CaseId",
                        column: x => x.CaseId,
                        principalSchema: "CMS",
                        principalTable: "Case",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CasePromissory_CaseId",
                schema: "CMS",
                table: "CasePromissory",
                column: "CaseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CasePromissory",
                schema: "CMS");

            migrationBuilder.AddColumn<string>(
                name: "CaseNumber",
                schema: "CMS",
                table: "Promissory",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);
        }
    }
}
