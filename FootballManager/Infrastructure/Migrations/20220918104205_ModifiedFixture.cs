using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class ModifiedFixture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fixtures_Leagues_FixtureLeagueID",
                table: "Fixtures");

            migrationBuilder.DropForeignKey(
                name: "FK_Fixtures_Teams_AwayTeamID",
                table: "Fixtures");

            migrationBuilder.DropForeignKey(
                name: "FK_Fixtures_Teams_HomeTeamID",
                table: "Fixtures");

            migrationBuilder.DropColumn(
                name: "AwayTeamScore",
                table: "Fixtures");

            migrationBuilder.DropColumn(
                name: "HomeTeamScore",
                table: "Fixtures");

            migrationBuilder.RenameColumn(
                name: "HomeTeamID",
                table: "Fixtures",
                newName: "HomeTeamId");

            migrationBuilder.RenameColumn(
                name: "FixtureLeagueID",
                table: "Fixtures",
                newName: "FixtureLeagueId");

            migrationBuilder.RenameColumn(
                name: "AwayTeamID",
                table: "Fixtures",
                newName: "AwayTeamId");

            migrationBuilder.RenameIndex(
                name: "IX_Fixtures_HomeTeamID",
                table: "Fixtures",
                newName: "IX_Fixtures_HomeTeamId");

            migrationBuilder.RenameIndex(
                name: "IX_Fixtures_FixtureLeagueID",
                table: "Fixtures",
                newName: "IX_Fixtures_FixtureLeagueId");

            migrationBuilder.RenameIndex(
                name: "IX_Fixtures_AwayTeamID",
                table: "Fixtures",
                newName: "IX_Fixtures_AwayTeamId");

            migrationBuilder.AddColumn<long>(
                name: "FixtureScoreId",
                table: "Fixtures",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isPlayed",
                table: "Fixtures",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    EventId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GoalScorerPlayerId = table.Column<long>(type: "bigint", nullable: true),
                    GoalAssisterPlayerId = table.Column<long>(type: "bigint", nullable: true),
                    FixtureId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_Event_Fixtures_FixtureId",
                        column: x => x.FixtureId,
                        principalTable: "Fixtures",
                        principalColumn: "FixtureId");
                    table.ForeignKey(
                        name: "FK_Event_Players_GoalAssisterPlayerId",
                        column: x => x.GoalAssisterPlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId");
                    table.ForeignKey(
                        name: "FK_Event_Players_GoalScorerPlayerId",
                        column: x => x.GoalScorerPlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId");
                });

            migrationBuilder.CreateTable(
                name: "Score",
                columns: table => new
                {
                    ScoreId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HomeScore = table.Column<int>(type: "int", nullable: false),
                    AwayScore = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Score", x => x.ScoreId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fixtures_FixtureScoreId",
                table: "Fixtures",
                column: "FixtureScoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_FixtureId",
                table: "Event",
                column: "FixtureId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_GoalAssisterPlayerId",
                table: "Event",
                column: "GoalAssisterPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_GoalScorerPlayerId",
                table: "Event",
                column: "GoalScorerPlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fixtures_Leagues_FixtureLeagueId",
                table: "Fixtures",
                column: "FixtureLeagueId",
                principalTable: "Leagues",
                principalColumn: "LeagueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fixtures_Score_FixtureScoreId",
                table: "Fixtures",
                column: "FixtureScoreId",
                principalTable: "Score",
                principalColumn: "ScoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fixtures_Teams_AwayTeamId",
                table: "Fixtures",
                column: "AwayTeamId",
                principalTable: "Teams",
                principalColumn: "TeamId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Fixtures_Teams_HomeTeamId",
                table: "Fixtures",
                column: "HomeTeamId",
                principalTable: "Teams",
                principalColumn: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fixtures_Leagues_FixtureLeagueId",
                table: "Fixtures");

            migrationBuilder.DropForeignKey(
                name: "FK_Fixtures_Score_FixtureScoreId",
                table: "Fixtures");

            migrationBuilder.DropForeignKey(
                name: "FK_Fixtures_Teams_AwayTeamId",
                table: "Fixtures");

            migrationBuilder.DropForeignKey(
                name: "FK_Fixtures_Teams_HomeTeamId",
                table: "Fixtures");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Score");

            migrationBuilder.DropIndex(
                name: "IX_Fixtures_FixtureScoreId",
                table: "Fixtures");

            migrationBuilder.DropColumn(
                name: "FixtureScoreId",
                table: "Fixtures");

            migrationBuilder.DropColumn(
                name: "isPlayed",
                table: "Fixtures");

            migrationBuilder.RenameColumn(
                name: "HomeTeamId",
                table: "Fixtures",
                newName: "HomeTeamID");

            migrationBuilder.RenameColumn(
                name: "FixtureLeagueId",
                table: "Fixtures",
                newName: "FixtureLeagueID");

            migrationBuilder.RenameColumn(
                name: "AwayTeamId",
                table: "Fixtures",
                newName: "AwayTeamID");

            migrationBuilder.RenameIndex(
                name: "IX_Fixtures_HomeTeamId",
                table: "Fixtures",
                newName: "IX_Fixtures_HomeTeamID");

            migrationBuilder.RenameIndex(
                name: "IX_Fixtures_FixtureLeagueId",
                table: "Fixtures",
                newName: "IX_Fixtures_FixtureLeagueID");

            migrationBuilder.RenameIndex(
                name: "IX_Fixtures_AwayTeamId",
                table: "Fixtures",
                newName: "IX_Fixtures_AwayTeamID");

            migrationBuilder.AddColumn<int>(
                name: "AwayTeamScore",
                table: "Fixtures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HomeTeamScore",
                table: "Fixtures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Fixtures_Leagues_FixtureLeagueID",
                table: "Fixtures",
                column: "FixtureLeagueID",
                principalTable: "Leagues",
                principalColumn: "LeagueId");

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
        }
    }
}
