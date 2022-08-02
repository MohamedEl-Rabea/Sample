using Microsoft.EntityFrameworkCore.Migrations;

namespace Moj.CMS.Infrastructure.Migrations
{
    public partial class fix_auditing_maxLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OldValues",
                schema: "CMS",
                table: "AuditTrails",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: null,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NewValues",
                schema: "CMS",
                table: "AuditTrails",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: null,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AffectedColumns",
                schema: "CMS",
                table: "AuditTrails",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: null,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OldValues",
                schema: "CMS",
                table: "AuditTrails",
                type: "nvarchar(256)",
                nullable: true,
                defaultValue: null,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NewValues",
                schema: "CMS",
                table: "AuditTrails",
                type: "nvarchar(256)",
                nullable: true,
                defaultValue: null,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AffectedColumns",
                schema: "CMS",
                table: "AuditTrails",
                type: "nvarchar(256)",
                nullable: true,
                defaultValue: null,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
