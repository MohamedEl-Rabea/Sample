using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Moj.CMS.UserAccess.Infrastructure.Migrations
{
    public partial class Add_Document_Info_ValueObject_To_UserProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePictureDataUrl",
                schema: "Identity",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "ProfilePictureId",
                schema: "Identity",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureUrl",
                schema: "Identity",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePictureId",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProfilePictureUrl",
                schema: "Identity",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureDataUrl",
                schema: "Identity",
                table: "Users",
                type: "text",
                nullable: true);
        }
    }
}
