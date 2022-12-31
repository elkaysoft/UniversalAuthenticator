using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversalAuthenticator.Domain.Migrations
{
    public partial class ValidationTokenEntityUpdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TokenType",
                table: "ValidationTokens",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TokenType",
                table: "ValidationTokens");
        }
    }
}
