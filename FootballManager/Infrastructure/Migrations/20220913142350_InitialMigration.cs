using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    PersonId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.PersonId);
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
                        name: "FK_Users_Person_UserPersonId",
                        column: x => x.UserPersonId,
                        principalTable: "Person",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
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
                    HomeTeamID = table.Column<long>(type: "bigint", nullable: true),
                    AwayTeamID = table.Column<long>(type: "bigint", nullable: true),
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
                        name: "FK_Managers_Person_ManagerPersonId",
                        column: x => x.ManagerPersonId,
                        principalTable: "Person",
                        principalColumn: "PersonId");
                    table.ForeignKey(
                        name: "FK_Managers_Users_UserManagerId",
                        column: x => x.UserManagerId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
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
                    CurrentSeasonStatsId = table.Column<long>(type: "bigint", nullable: true),
                    CurrentTeamSheetId = table.Column<long>(type: "bigint", nullable: true),
                    CurrentLeagueId = table.Column<long>(type: "bigint", nullable: true),
                    TeamManagerId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamId);
                    table.ForeignKey(
                        name: "FK_Teams_Leagues_CurrentLeagueId",
                        column: x => x.CurrentLeagueId,
                        principalTable: "Leagues",
                        principalColumn: "LeagueId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teams_Managers_TeamManagerId",
                        column: x => x.TeamManagerId,
                        principalTable: "Managers",
                        principalColumn: "ManagerId",
                        onDelete: ReferentialAction.SetNull);
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
                    PlayerRecordId = table.Column<long>(type: "bigint", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayerId);
                    table.ForeignKey(
                        name: "FK_Players_Person_PlayerPersonId",
                        column: x => x.PlayerPersonId,
                        principalTable: "Person",
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
                        principalColumn: "RecordId");
                    table.ForeignKey(
                        name: "FK_Players_Teams_CurrentTeamId",
                        column: x => x.CurrentTeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fixtures_AwayTeamID",
                table: "Fixtures",
                column: "AwayTeamID");

            migrationBuilder.CreateIndex(
                name: "IX_Fixtures_FixtureLeagueID",
                table: "Fixtures",
                column: "FixtureLeagueID");

            migrationBuilder.CreateIndex(
                name: "IX_Fixtures_HomeTeamID",
                table: "Fixtures",
                column: "HomeTeamID");

            migrationBuilder.CreateIndex(
                name: "IX_Leagues_CurrentSeasonId",
                table: "Leagues",
                column: "CurrentSeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_CurrentTeamId",
                table: "Managers",
                column: "CurrentTeamId",
                unique: true,
                filter: "[CurrentTeamId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_ManagerPersonId",
                table: "Managers",
                column: "ManagerPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_UserManagerId",
                table: "Managers",
                column: "UserManagerId",
                unique: true,
                filter: "[UserManagerId] IS NOT NULL");

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
                column: "TeamManagerId",
                unique: true,
                filter: "[TeamManagerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TeamSheet_TeamTacticId",
                table: "TeamSheet",
                column: "TeamTacticId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserPersonId",
                table: "Users",
                column: "UserPersonId",
                unique: true,
                filter: "[UserPersonId] IS NOT NULL");

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
                name: "FK_Managers_Teams_CurrentTeamId",
                table: "Managers",
                column: "CurrentTeamId",
                principalTable: "Teams",
                principalColumn: "TeamId",
                onDelete: ReferentialAction.SetNull);
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
                name: "Fixtures");

            migrationBuilder.DropTable(
                name: "Players");

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
                name: "Person");
        }
    }
}
