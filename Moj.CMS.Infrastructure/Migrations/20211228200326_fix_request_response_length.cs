using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorHero.CleanArchitecture.Infrastructure.Migrations
{
    public partial class fix_request_response_length : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SadadInvoiceRequestLogs",
                schema: "CMS",
                table: "SadadInvoiceRequestLogs");

            migrationBuilder.RenameTable(
                name: "SadadInvoiceRequestLogs",
                schema: "CMS",
                newName: "SadadInvoiceRequestLog",
                newSchema: "CMS");

            migrationBuilder.AlterColumn<string>(
                name: "Response",
                schema: "CMS",
                table: "VIbanRequestLog",
                type: "nvarchar(max)",
                maxLength: 2147483647,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Request",
                schema: "CMS",
                table: "VIbanRequestLog",
                type: "nvarchar(max)",
                maxLength: 2147483647,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Response",
                schema: "CMS",
                table: "SadadInvoiceRequestLog",
                type: "nvarchar(max)",
                maxLength: 2147483647,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Request",
                schema: "CMS",
                table: "SadadInvoiceRequestLog",
                type: "nvarchar(max)",
                maxLength: 2147483647,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SadadInvoiceRequestLog",
                schema: "CMS",
                table: "SadadInvoiceRequestLog",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SadadInvoiceRequestLog",
                schema: "CMS",
                table: "SadadInvoiceRequestLog");

            migrationBuilder.RenameTable(
                name: "SadadInvoiceRequestLog",
                schema: "CMS",
                newName: "SadadInvoiceRequestLogs",
                newSchema: "CMS");

            migrationBuilder.AlterColumn<string>(
                name: "Response",
                schema: "CMS",
                table: "VIbanRequestLog",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 2147483647,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Request",
                schema: "CMS",
                table: "VIbanRequestLog",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 2147483647,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Response",
                schema: "CMS",
                table: "SadadInvoiceRequestLogs",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 2147483647,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Request",
                schema: "CMS",
                table: "SadadInvoiceRequestLogs",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 2147483647,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SadadInvoiceRequestLogs",
                schema: "CMS",
                table: "SadadInvoiceRequestLogs",
                column: "Id");
        }
    }
}
