using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorHero.CleanArchitecture.Infrastructure.Migrations
{
    public partial class Update_Case_Party_Model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PartyId",
                schema: "CMS",
                table: "CaseParty");

            migrationBuilder.RenameColumn(
                name: "PromisoryId",
                schema: "CMS",
                table: "CaseParty",
                newName: "PartyClassificationId");

            migrationBuilder.AddColumn<string>(
                name: "PartyNumber",
                schema: "CMS",
                table: "CaseParty",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PromissoryNumber",
                schema: "CMS",
                table: "CaseParty",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "PartyClassificationLookup",
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
                    table.PrimaryKey("PK_PartyClassificationLookup", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartyClassificationLookup",
                schema: "CMS");

            migrationBuilder.DropColumn(
                name: "PartyNumber",
                schema: "CMS",
                table: "CaseParty");

            migrationBuilder.DropColumn(
                name: "PromissoryNumber",
                schema: "CMS",
                table: "CaseParty");

            migrationBuilder.RenameColumn(
                name: "PartyClassificationId",
                schema: "CMS",
                table: "CaseParty",
                newName: "PromisoryId");

            migrationBuilder.AddColumn<int>(
                name: "PartyId",
                schema: "CMS",
                table: "CaseParty",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
