using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorHero.CleanArchitecture.Infrastructure.Migrations
{
    public partial class change_audit_primarykey_to_entityId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PrimaryKey",
                schema: "CMS",
                table: "AuditTrails",
                newName: "EntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EntityId",
                schema: "CMS",
                table: "AuditTrails",
                newName: "PrimaryKey");
        }
    }
}
