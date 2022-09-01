using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class ModifiedManagerFluentApi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Managers_Teams_CurrentTeamId",
                table: "Managers");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Managers_TeamManagerId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_TeamManagerId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Managers_CurrentTeamId",
                table: "Managers");

            migrationBuilder.RenameColumn(
                name: "TeamManagerId",
                table: "Teams",
                newName: "ManagerId");

            migrationBuilder.RenameColumn(
                name: "CurrentTeamId",
                table: "Managers",
                newName: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ManagerId",
                table: "Teams",
                column: "ManagerId",
                unique: true,
                filter: "[ManagerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_TeamId",
                table: "Managers",
                column: "TeamId",
                unique: true,
                filter: "[TeamId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_Teams_TeamId",
                table: "Managers",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Managers_ManagerId",
                table: "Teams",
                column: "ManagerId",
                principalTable: "Managers",
                principalColumn: "ManagerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Managers_Teams_TeamId",
                table: "Managers");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Managers_ManagerId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_ManagerId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Managers_TeamId",
                table: "Managers");

            migrationBuilder.RenameColumn(
                name: "ManagerId",
                table: "Teams",
                newName: "TeamManagerId");

            migrationBuilder.RenameColumn(
                name: "TeamId",
                table: "Managers",
                newName: "CurrentTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_TeamManagerId",
                table: "Teams",
                column: "TeamManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_CurrentTeamId",
                table: "Managers",
                column: "CurrentTeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_Teams_CurrentTeamId",
                table: "Managers",
                column: "CurrentTeamId",
                principalTable: "Teams",
                principalColumn: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Managers_TeamManagerId",
                table: "Teams",
                column: "TeamManagerId",
                principalTable: "Managers",
                principalColumn: "ManagerId");
        }
    }
}
