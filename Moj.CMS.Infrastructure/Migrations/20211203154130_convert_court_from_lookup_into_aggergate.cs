using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorHero.CleanArchitecture.Infrastructure.Migrations
{
    public partial class convert_court_from_lookup_into_aggergate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourtBankAccounts_CourtsLookup_CourtId",
                schema: "CMS",
                table: "CourtBankAccounts");

            migrationBuilder.DropTable(
                name: "CourtsLookup",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "DivisionsLookup",
                schema: "CMS");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "CMS",
                table: "CourtBankAccounts",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

            migrationBuilder.CreateTable(
                name: "ClientIntegrationSettings",
                schema: "CMS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ClientType = table.Column<int>(type: "int", nullable: false),
                    AuthToken = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    TokenExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientIntegrationSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courts",
                schema: "CMS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    AreaCode = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_Courts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourtDivisions",
                schema: "CMS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CourtId = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourtDivisions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourtDivisions_Courts_CourtId",
                        column: x => x.CourtId,
                        principalSchema: "CMS",
                        principalTable: "Courts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourtDivisions_CourtId",
                schema: "CMS",
                table: "CourtDivisions",
                column: "CourtId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourtBankAccounts_Courts_CourtId",
                schema: "CMS",
                table: "CourtBankAccounts",
                column: "CourtId",
                principalSchema: "CMS",
                principalTable: "Courts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourtBankAccounts_Courts_CourtId",
                schema: "CMS",
                table: "CourtBankAccounts");

            migrationBuilder.DropTable(
                name: "ClientIntegrationSettings",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "CourtDivisions",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "Courts",
                schema: "CMS");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "CMS",
                table: "CourtBankAccounts");

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
                name: "CourtsLookup",
                schema: "CMS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AreaCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourtsLookup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DivisionsLookup",
                schema: "CMS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    CourtCode = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DivisionsLookup", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CourtBankAccounts_CourtsLookup_CourtId",
                schema: "CMS",
                table: "CourtBankAccounts",
                column: "CourtId",
                principalSchema: "CMS",
                principalTable: "CourtsLookup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
