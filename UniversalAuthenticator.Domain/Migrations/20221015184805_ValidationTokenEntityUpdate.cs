using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversalAuthenticator.Domain.Migrations
{
    public partial class ValidationTokenEntityUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "ValidationTokens",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateGenerated",
                table: "ValidationTokens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiryDate",
                table: "ValidationTokens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateGenerated",
                table: "ValidationTokens");

            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "ValidationTokens");

            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "ValidationTokens",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);
        }
    }
}
