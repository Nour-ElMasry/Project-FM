using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    PersonId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.PersonId);
                });

            migrationBuilder.CreateTable(
                name: "PlayerStats",
                columns: table => new
                {
                    PlayerStatsId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Attacking = table.Column<int>(type: "int", nullable: false),
                    PlayMaking = table.Column<int>(type: "int", nullable: false),
                    Defending = table.Column<int>(type: "int", nullable: false),
                    Goalkeeping = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerStats", x => x.PlayerStatsId);
                });

            migrationBuilder.CreateTable(
                name: "Record",
                columns: table => new
                {
                    RecordId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GamesPlayed = table.Column<int>(type: "int", nullable: false),
                    Goals = table.Column<int>(type: "int", nullable: false),
                    Assists = table.Column<int>(type: "int", nullable: false),
                    CleanSheets = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Record", x => x.RecordId);
                });

            migrationBuilder.CreateTable(
                name: "Season",
                columns: table => new
                {
                    SeasonId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Season", x => x.SeasonId);
                });

            migrationBuilder.CreateTable(
                name: "SeasonStats",
                columns: table => new
                {
                    SeasonStatsId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Points = table.Column<int>(type: "int", nullable: false),
                    GamesPlayed = table.Column<int>(type: "int", nullable: false),
                    GamesWon = table.Column<int>(type: "int", nullable: false),
                    GamesDrawn = table.Column<int>(type: "int", nullable: false),
                    GamesLost = table.Column<int>(type: "int", nullable: false),
                    HomeGamesPlayed = table.Column<int>(type: "int", nullable: false),
                    AwayGamesPlayed = table.Column<int>(type: "int", nullable: false),
                    GoalsFor = table.Column<int>(type: "int", nullable: false),
                    GoalsAgainst = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeasonStats", x => x.SeasonStatsId);
                });

            migrationBuilder.CreateTable(
                name: "Tactic",
                columns: table => new
                {
                    TacticId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttackingWeight = table.Column<int>(type: "int", nullable: false),
                    DefendingWeight = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tactic", x => x.TacticId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserPersonId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_People_UserPersonId",
                        column: x => x.UserPersonId,
                        principalTable: "People",
                        principalColumn: "PersonId");
                });

            migrationBuilder.CreateTable(
                name: "Leagues",
                columns: table => new
                {
                    LeagueId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentSeasonId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leagues", x => x.LeagueId);
                    table.ForeignKey(
                        name: "FK_Leagues_Season_CurrentSeasonId",
                        column: x => x.CurrentSeasonId,
                        principalTable: "Season",
                        principalColumn: "SeasonId");
                });

            migrationBuilder.CreateTable(
                name: "TeamSheet",
                columns: table => new
                {
                    TeamSheetId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttackingRating = table.Column<int>(type: "int", nullable: false),
                    DefendingRating = table.Column<int>(type: "int", nullable: false),
                    TeamTacticId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamSheet", x => x.TeamSheetId);
                    table.ForeignKey(
                        name: "FK_TeamSheet_Tactic_TeamTacticId",
                        column: x => x.TeamTacticId,
                        principalTable: "Tactic",
                        principalColumn: "TacticId");
                });

            migrationBuilder.CreateTable(
                name: "Fixtures",
                columns: table => new
                {
                    FixtureId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FixtureLeagueID = table.Column<long>(type: "bigint", nullable: true),
                    Venue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HomeTeamScore = table.Column<int>(type: "int", nullable: false),
                    AwayTeamScore = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fixtures", x => x.FixtureId);
                    table.ForeignKey(
                        name: "FK_Fixtures_Leagues_FixtureLeagueID",
                        column: x => x.FixtureLeagueID,
                        principalTable: "Leagues",
                        principalColumn: "LeagueId");
                });

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
                });

            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    ManagerId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManagerPersonId = table.Column<long>(type: "bigint", nullable: true),
                    CurrentTeamId = table.Column<long>(type: "bigint", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserManagerId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.ManagerId);
                    table.ForeignKey(
                        name: "FK_Managers_People_ManagerPersonId",
                        column: x => x.ManagerPersonId,
                        principalTable: "People",
                        principalColumn: "PersonId");
                    table.ForeignKey(
                        name: "FK_Managers_Users_UserManagerId",
                        column: x => x.UserManagerId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Venue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeamManagerId = table.Column<long>(type: "bigint", nullable: true),
                    CurrentSeasonStatsId = table.Column<long>(type: "bigint", nullable: true),
                    CurrentTeamSheetId = table.Column<long>(type: "bigint", nullable: true),
                    CurrentLeagueId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamId);
                    table.ForeignKey(
                        name: "FK_Teams_Leagues_CurrentLeagueId",
                        column: x => x.CurrentLeagueId,
                        principalTable: "Leagues",
                        principalColumn: "LeagueId");
                    table.ForeignKey(
                        name: "FK_Teams_Managers_TeamManagerId",
                        column: x => x.TeamManagerId,
                        principalTable: "Managers",
                        principalColumn: "ManagerId");
                    table.ForeignKey(
                        name: "FK_Teams_SeasonStats_CurrentSeasonStatsId",
                        column: x => x.CurrentSeasonStatsId,
                        principalTable: "SeasonStats",
                        principalColumn: "SeasonStatsId");
                    table.ForeignKey(
                        name: "FK_Teams_TeamSheet_CurrentTeamSheetId",
                        column: x => x.CurrentTeamSheetId,
                        principalTable: "TeamSheet",
                        principalColumn: "TeamSheetId");
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    PlayerId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerPersonId = table.Column<long>(type: "bigint", nullable: true),
                    PlayerStatsId = table.Column<long>(type: "bigint", nullable: true),
                    CurrentTeamId = table.Column<long>(type: "bigint", nullable: true),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlayerRecordId = table.Column<long>(type: "bigint", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamSheetId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayerId);
                    table.ForeignKey(
                        name: "FK_Players_People_PlayerPersonId",
                        column: x => x.PlayerPersonId,
                        principalTable: "People",
                        principalColumn: "PersonId");
                    table.ForeignKey(
                        name: "FK_Players_PlayerStats_PlayerStatsId",
                        column: x => x.PlayerStatsId,
                        principalTable: "PlayerStats",
                        principalColumn: "PlayerStatsId");
                    table.ForeignKey(
                        name: "FK_Players_Record_PlayerRecordId",
                        column: x => x.PlayerRecordId,
                        principalTable: "Record",
                        principalColumn: "RecordId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Players_Teams_CurrentTeamId",
                        column: x => x.CurrentTeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId");
                    table.ForeignKey(
                        name: "FK_Players_TeamSheet_TeamSheetId",
                        column: x => x.TeamSheetId,
                        principalTable: "TeamSheet",
                        principalColumn: "TeamSheetId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fixtures_FixtureLeagueID",
                table: "Fixtures",
                column: "FixtureLeagueID");

            migrationBuilder.CreateIndex(
                name: "IX_FixtureTeam_TeamsTeamId",
                table: "FixtureTeam",
                column: "TeamsTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Leagues_CurrentSeasonId",
                table: "Leagues",
                column: "CurrentSeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_CurrentTeamId",
                table: "Managers",
                column: "CurrentTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_ManagerPersonId",
                table: "Managers",
                column: "ManagerPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_UserManagerId",
                table: "Managers",
                column: "UserManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_CurrentTeamId",
                table: "Players",
                column: "CurrentTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_PlayerPersonId",
                table: "Players",
                column: "PlayerPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_PlayerRecordId",
                table: "Players",
                column: "PlayerRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_PlayerStatsId",
                table: "Players",
                column: "PlayerStatsId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_TeamSheetId",
                table: "Players",
                column: "TeamSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CurrentLeagueId",
                table: "Teams",
                column: "CurrentLeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CurrentSeasonStatsId",
                table: "Teams",
                column: "CurrentSeasonStatsId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CurrentTeamSheetId",
                table: "Teams",
                column: "CurrentTeamSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_TeamManagerId",
                table: "Teams",
                column: "TeamManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamSheet_TeamTacticId",
                table: "TeamSheet",
                column: "TeamTacticId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserPersonId",
                table: "Users",
                column: "UserPersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_FixtureTeam_Teams_TeamsTeamId",
                table: "FixtureTeam",
                column: "TeamsTeamId",
                principalTable: "Teams",
                principalColumn: "TeamId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_Teams_CurrentTeamId",
                table: "Managers",
                column: "CurrentTeamId",
                principalTable: "Teams",
                principalColumn: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Leagues_CurrentLeagueId",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_Managers_Teams_CurrentTeamId",
                table: "Managers");

            migrationBuilder.DropTable(
                name: "FixtureTeam");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Fixtures");

            migrationBuilder.DropTable(
                name: "PlayerStats");

            migrationBuilder.DropTable(
                name: "Record");

            migrationBuilder.DropTable(
                name: "Leagues");

            migrationBuilder.DropTable(
                name: "Season");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Managers");

            migrationBuilder.DropTable(
                name: "SeasonStats");

            migrationBuilder.DropTable(
                name: "TeamSheet");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Tactic");

            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
