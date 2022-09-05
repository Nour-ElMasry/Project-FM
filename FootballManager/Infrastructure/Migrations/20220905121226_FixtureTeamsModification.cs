using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class FixtureTeamsModification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Teams_CurrentTeamId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Leagues_CurrentLeagueId",
                table: "Teams");

            migrationBuilder.DropTable(
                name: "FixtureTeam");

            migrationBuilder.AddColumn<long>(
                name: "AwayTeamID",
                table: "Fixtures",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "HomeTeamID",
                table: "Fixtures",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fixtures_AwayTeamID",
                table: "Fixtures",
                column: "AwayTeamID");

            migrationBuilder.CreateIndex(
                name: "IX_Fixtures_HomeTeamID",
                table: "Fixtures",
                column: "HomeTeamID");

            migrationBuilder.AddForeignKey(
                name: "FK_Fixtures_Teams_AwayTeamID",
                table: "Fixtures",
                column: "AwayTeamID",
                principalTable: "Teams",
                principalColumn: "TeamId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Fixtures_Teams_HomeTeamID",
                table: "Fixtures",
                column: "HomeTeamID",
                principalTable: "Teams",
                principalColumn: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Teams_CurrentTeamId",
                table: "Players",
                column: "CurrentTeamId",
                principalTable: "Teams",
                principalColumn: "TeamId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Leagues_CurrentLeagueId",
                table: "Teams",
                column: "CurrentLeagueId",
                principalTable: "Leagues",
                principalColumn: "LeagueId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fixtures_Teams_AwayTeamID",
                table: "Fixtures");

            migrationBuilder.DropForeignKey(
                name: "FK_Fixtures_Teams_HomeTeamID",
                table: "Fixtures");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Teams_CurrentTeamId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Leagues_CurrentLeagueId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Fixtures_AwayTeamID",
                table: "Fixtures");

            migrationBuilder.DropIndex(
                name: "IX_Fixtures_HomeTeamID",
                table: "Fixtures");

            migrationBuilder.DropColumn(
                name: "AwayTeamID",
                table: "Fixtures");

            migrationBuilder.DropColumn(
                name: "HomeTeamID",
                table: "Fixtures");

            migrationBuilder.CreateTable(
                name: "FixtureTeam",
                columns: table => new
                {
                    FixturesFixtureId = table.Column<long>(type: "bigint", nullable: false),
                    TeamsTeamId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FixtureTeam", x => new { x.FixturesFixtureId, x.TeamsTeamId });
                    table.ForeignKey(
                        name: "FK_FixtureTeam_Fixtures_FixturesFixtureId",
                        column: x => x.FixturesFixtureId,
                        principalTable: "Fixtures",
                        principalColumn: "FixtureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FixtureTeam_Teams_TeamsTeamId",
                        column: x => x.TeamsTeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FixtureTeam_TeamsTeamId",
                table: "FixtureTeam",
                column: "TeamsTeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Teams_CurrentTeamId",
                table: "Players",
                column: "CurrentTeamId",
                principalTable: "Teams",
                principalColumn: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Leagues_CurrentLeagueId",
                table: "Teams",
                column: "CurrentLeagueId",
                principalTable: "Leagues",
                principalColumn: "LeagueId");
        }
    }
}
