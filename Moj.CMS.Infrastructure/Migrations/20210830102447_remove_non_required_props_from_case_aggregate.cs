using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorHero.CleanArchitecture.Infrastructure.Migrations
{
    public partial class remove_non_required_props_from_case_aggregate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sadad_Case_CaseId",
                schema: "CMS",
                table: "Sadad");

            migrationBuilder.DropTable(
                name: "VIban",
                schema: "CMS");

            migrationBuilder.DropIndex(
                name: "IX_Sadad_CaseId",
                schema: "CMS",
                table: "Sadad");

            migrationBuilder.DropColumn(
                name: "CaseStatusId",
                schema: "CMS",
                table: "Case");

            migrationBuilder.DropColumn(
                name: "CaseTypeId",
                schema: "CMS",
                table: "Case");

            migrationBuilder.DropColumn(
                name: "IsCreateIban",
                schema: "CMS",
                table: "Case");

            migrationBuilder.DropColumn(
                name: "NumberOfAccused",
                schema: "CMS",
                table: "Case");

            migrationBuilder.RenameColumn(
                name: "NumberOfSadad",
                schema: "CMS",
                table: "Case",
                newName: "CaseType");

            migrationBuilder.RenameColumn(
                name: "NumberOfComplaint",
                schema: "CMS",
                table: "Case",
                newName: "CaseStatus");

            migrationBuilder.AlterColumn<string>(
                name: "SadadNumber",
                schema: "CMS",
                table: "Sadad",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CaseType",
                schema: "CMS",
                table: "Case",
                newName: "NumberOfSadad");

            migrationBuilder.RenameColumn(
                name: "CaseStatus",
                schema: "CMS",
                table: "Case",
                newName: "NumberOfComplaint");

            migrationBuilder.AlterColumn<string>(
                name: "SadadNumber",
                schema: "CMS",
                table: "Sadad",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "CaseStatusId",
                schema: "CMS",
                table: "Case",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CaseTypeId",
                schema: "CMS",
                table: "Case",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsCreateIban",
                schema: "CMS",
                table: "Case",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfAccused",
                schema: "CMS",
                table: "Case",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "VIban",
                schema: "CMS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Alias = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CAP = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CaseId = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "date", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    VIban = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VIban", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VIban_Case_CaseId",
                        column: x => x.CaseId,
                        principalSchema: "CMS",
                        principalTable: "Case",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sadad_CaseId",
                schema: "CMS",
                table: "Sadad",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_VIban_CaseId",
                schema: "CMS",
                table: "VIban",
                column: "CaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sadad_Case_CaseId",
                schema: "CMS",
                table: "Sadad",
                column: "CaseId",
                principalSchema: "CMS",
                principalTable: "Case",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
