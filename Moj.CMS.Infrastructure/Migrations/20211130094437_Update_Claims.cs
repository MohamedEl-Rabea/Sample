using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorHero.CleanArchitecture.Infrastructure.Migrations
{
    public partial class Update_Claims : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                schema: "CMS",
                table: "Claims",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "CloseDate",
                schema: "CMS",
                table: "Claims",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CloseReferenceNumber",
                schema: "CMS",
                table: "Claims",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FinancialStatus",
                schema: "CMS",
                table: "Claims",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ClaimFinancialStatusesLookup",
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
                    table.PrimaryKey("PK_ClaimFinancialStatusesLookup", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClaimFinancialStatusesLookup",
                schema: "CMS");

            migrationBuilder.DropColumn(
                name: "CloseDate",
                schema: "CMS",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "CloseReferenceNumber",
                schema: "CMS",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "FinancialStatus",
                schema: "CMS",
                table: "Claims");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                schema: "CMS",
                table: "Claims",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
