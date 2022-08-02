using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Moj.CMS.Infrastructure.Migrations
{
    public partial class addPartyTypetoparty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PartyIdentityTypeId",
                schema: "CMS",
                table: "Party");

            migrationBuilder.RenameColumn(
                name: "PartyIdentityNumber",
                schema: "CMS",
                table: "Party",
                newName: "CurrentIdentityNumber");

            migrationBuilder.CreateTable(
                name: "PartyIdentity",
                schema: "CMS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartyIdentityTypeId = table.Column<int>(type: "int", nullable: false),
                    PartyIdentityNumber = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    PartyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartyIdentity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartyIdentity_Party_PartyId",
                        column: x => x.PartyId,
                        principalSchema: "CMS",
                        principalTable: "Party",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PartyIdentity_PartyId",
                schema: "CMS",
                table: "PartyIdentity",
                column: "PartyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartyIdentity",
                schema: "CMS");

            migrationBuilder.RenameColumn(
                name: "CurrentIdentityNumber",
                schema: "CMS",
                table: "Party",
                newName: "PartyIdentityNumber");

            migrationBuilder.AddColumn<int>(
                name: "PartyIdentityTypeId",
                schema: "CMS",
                table: "Party",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
