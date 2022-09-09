using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class UserManagerCascadeDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Managers_Users_UserManagerId",
                table: "Managers");

            migrationBuilder.DropIndex(
                name: "IX_Managers_UserManagerId",
                table: "Managers");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_UserManagerId",
                table: "Managers",
                column: "UserManagerId",
                unique: true,
                filter: "[UserManagerId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_Users_UserManagerId",
                table: "Managers",
                column: "UserManagerId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Managers_Users_UserManagerId",
                table: "Managers");

            migrationBuilder.DropIndex(
                name: "IX_Managers_UserManagerId",
                table: "Managers");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_UserManagerId",
                table: "Managers",
                column: "UserManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_Users_UserManagerId",
                table: "Managers",
                column: "UserManagerId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
