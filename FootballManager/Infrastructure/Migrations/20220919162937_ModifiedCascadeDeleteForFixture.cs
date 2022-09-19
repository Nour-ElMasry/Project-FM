using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class ModifiedCascadeDeleteForFixture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fixtures_Score_FixtureScoreId",
                table: "Fixtures");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Managers_UserManagerId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserManagerId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Fixtures_FixtureScoreId",
                table: "Fixtures");

            migrationBuilder.DropColumn(
                name: "UserManagerId",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_UserManagerId",
                table: "Managers",
                column: "UserManagerId",
                unique: true,
                filter: "[UserManagerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Fixtures_FixtureScoreId",
                table: "Fixtures",
                column: "FixtureScoreId",
                unique: true,
                filter: "[FixtureScoreId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Fixtures_Score_FixtureScoreId",
                table: "Fixtures",
                column: "FixtureScoreId",
                principalTable: "Score",
                principalColumn: "ScoreId",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Fixtures_Score_FixtureScoreId",
                table: "Fixtures");

            migrationBuilder.DropForeignKey(
                name: "FK_Managers_Users_UserManagerId",
                table: "Managers");

            migrationBuilder.DropIndex(
                name: "IX_Managers_UserManagerId",
                table: "Managers");

            migrationBuilder.DropIndex(
                name: "IX_Fixtures_FixtureScoreId",
                table: "Fixtures");

            migrationBuilder.AddColumn<long>(
                name: "UserManagerId",
                table: "Users",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserManagerId",
                table: "Users",
                column: "UserManagerId",
                unique: true,
                filter: "[UserManagerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Fixtures_FixtureScoreId",
                table: "Fixtures",
                column: "FixtureScoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fixtures_Score_FixtureScoreId",
                table: "Fixtures",
                column: "FixtureScoreId",
                principalTable: "Score",
                principalColumn: "ScoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Managers_UserManagerId",
                table: "Users",
                column: "UserManagerId",
                principalTable: "Managers",
                principalColumn: "ManagerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
