using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorHero.CleanArchitecture.Infrastructure.Migrations
{
    public partial class removeprmossoryfromcase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Promissory",
                schema: "CMS");

            migrationBuilder.DropColumn(
                name: "NumberOfPromissory",
                schema: "CMS",
                table: "Case");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfPromissory",
                schema: "CMS",
                table: "Case",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Promissory",
                schema: "CMS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "date", nullable: false),
                    PromissoryNumber = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    PromissoryStampNumber = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    PromissoryTypeId = table.Column<int>(type: "int", nullable: false)
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
                name: "IX_Promissory_CaseId",
                schema: "CMS",
                table: "Promissory",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Promissory_PromissoryStampNumber",
                schema: "CMS",
                table: "Promissory",
                column: "PromissoryStampNumber",
                unique: true,
                filter: "[PromissoryStampNumber] IS NOT NULL");
        }
    }
}
