using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Moj.CMS.Infrastructure.Migrations
{
    public partial class AddParty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "PartyHilo",
                schema: "CMS",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "Party",
                schema: "CMS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    PartyIdentityTypeId = table.Column<int>(type: "int", nullable: false),
                    PartyTypeId = table.Column<int>(type: "int", nullable: false),
                    PartyIdentityNumber = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    PartyStatusId = table.Column<int>(type: "int", nullable: false),
                    PartyLocationId = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ShortName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NationalityId = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CommercialRegistry = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IsCompany = table.Column<bool>(type: "bit", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SecondName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ThirdName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_Party", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Party",
                schema: "CMS");

            migrationBuilder.DropSequence(
                name: "PartyHilo",
                schema: "CMS");
        }
    }
}
