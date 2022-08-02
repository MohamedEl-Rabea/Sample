using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorHero.CleanArchitecture.Infrastructure.Migrations
{
    public partial class editprmossory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "PromissorySequenceHilo",
                schema: "CMS",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "Promissory",
                schema: "CMS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    PromissoryStampNumber = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PromissoryNumber = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    PromissoryTypeId = table.Column<int>(type: "int", nullable: false),
                    DebtTypeId = table.Column<int>(type: "int", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("PK_Promissory", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Promissory",
                schema: "CMS");

            migrationBuilder.DropSequence(
                name: "PromissorySequenceHilo",
                schema: "CMS");
        }
    }
}
