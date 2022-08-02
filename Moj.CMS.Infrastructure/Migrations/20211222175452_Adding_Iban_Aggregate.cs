using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorHero.CleanArchitecture.Infrastructure.Migrations
{
    public partial class Adding_Iban_Aggregate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                schema: "CMS",
                table: "CourtBankAccounts");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "CourtBankAccounts");

            migrationBuilder.DropColumn(
                name: "RemainingVIbanCount",
                schema: "CMS",
                table: "CourtBankAccounts");

            migrationBuilder.CreateTable(
                name: "IbanPurposeLookup",
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
                    table.PrimaryKey("PK_IbanPurposeLookup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ibans",
                schema: "CMS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Bank = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Branch = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    VIbanQuantity = table.Column<int>(type: "int", nullable: false),
                    VIbanRemaining = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ReferenceType = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ibans", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IbanPurposeLookup",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "Ibans",
                schema: "CMS");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                schema: "CMS",
                table: "CourtBankAccounts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifierUserId",
                schema: "CMS",
                table: "CourtBankAccounts",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RemainingVIbanCount",
                schema: "CMS",
                table: "CourtBankAccounts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
