using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversalAuthenticator.Domain.Migrations
{
    public partial class ApplicationUserModel_Modification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserRoles_ApplicationUserId",
                table: "UserRoles");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_ApplicationUserId",
                table: "UserRoles",
                column: "ApplicationUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserRoles_ApplicationUserId",
                table: "UserRoles");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_ApplicationUserId",
                table: "UserRoles",
                column: "ApplicationUserId",
                unique: true);
        }
    }
}
