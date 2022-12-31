using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversalAuthenticator.Domain.Migrations
{
    public partial class ValidationTokenModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ValidationTokenExpiration",
                table: "SystemConfigurations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValidationTokenExpiration",
                table: "SystemConfigurations");
        }
    }
}
