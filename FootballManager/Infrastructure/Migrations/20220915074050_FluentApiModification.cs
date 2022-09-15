using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class FluentApiModification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fixtures_Teams_HomeTeamID",
                table: "Fixtures");

            migrationBuilder.DropForeignKey(
                name: "FK_Person_Players_PlayerPersonId",
                table: "Person");

            migrationBuilder.DropForeignKey(
                name: "FK_Person_Users_UserPersonId",
                table: "Person");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerStats_Players_CurrentPlayerStatsId",
                table: "PlayerStats");

            migrationBuilder.DropForeignKey(
                name: "FK_Record_Players_PlayerRecordId",
                table: "Record");

            migrationBuilder.DropForeignKey(
                name: "FK_SeasonStats_Teams_CurrentSeasonStatsId",
                table: "SeasonStats");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamSheet_Teams_CurrentTeamSheetId",
                table: "TeamSheet");

            migrationBuilder.DropIndex(
                name: "IX_TeamSheet_CurrentTeamSheetId",
                table: "TeamSheet");

            migrationBuilder.DropIndex(
                name: "IX_SeasonStats_CurrentSeasonStatsId",
                table: "SeasonStats");

            migrationBuilder.DropIndex(
                name: "IX_Record_PlayerRecordId",
                table: "Record");

            migrationBuilder.DropIndex(
                name: "IX_PlayerStats_CurrentPlayerStatsId",
                table: "PlayerStats");

            migrationBuilder.DropIndex(
                name: "IX_Person_PlayerPersonId",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Person_UserPersonId",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "CurrentTeamSheetId",
                table: "TeamSheet");

            migrationBuilder.DropColumn(
                name: "CurrentSeasonStatsId",
                table: "SeasonStats");

            migrationBuilder.DropColumn(
                name: "PlayerRecordId",
                table: "Record");

            migrationBuilder.DropColumn(
                name: "CurrentPlayerStatsId",
                table: "PlayerStats");

            migrationBuilder.DropColumn(
                name: "PlayerPersonId",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "UserPersonId",
                table: "Person");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserPersonId",
                table: "Users",
                column: "UserPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CurrentSeasonStatsId",
                table: "Teams",
                column: "CurrentSeasonStatsId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CurrentTeamSheetId",
                table: "Teams",
                column: "CurrentTeamSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_CurrentPlayerStatsId",
                table: "Players",
                column: "CurrentPlayerStatsId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_PlayerPersonId",
                table: "Players",
                column: "PlayerPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_PlayerRecordId",
                table: "Players",
                column: "PlayerRecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fixtures_Teams_HomeTeamID",
                table: "Fixtures",
                column: "HomeTeamID",
                principalTable: "Teams",
                principalColumn: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Person_PlayerPersonId",
                table: "Players",
                column: "PlayerPersonId",
                principalTable: "Person",
                principalColumn: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_PlayerStats_CurrentPlayerStatsId",
                table: "Players",
                column: "CurrentPlayerStatsId",
                principalTable: "PlayerStats",
                principalColumn: "PlayerStatsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Record_PlayerRecordId",
                table: "Players",
                column: "PlayerRecordId",
                principalTable: "Record",
                principalColumn: "RecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_SeasonStats_CurrentSeasonStatsId",
                table: "Teams",
                column: "CurrentSeasonStatsId",
                principalTable: "SeasonStats",
                principalColumn: "SeasonStatsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_TeamSheet_CurrentTeamSheetId",
                table: "Teams",
                column: "CurrentTeamSheetId",
                principalTable: "TeamSheet",
                principalColumn: "TeamSheetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Person_UserPersonId",
                table: "Users",
                column: "UserPersonId",
                principalTable: "Person",
                principalColumn: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fixtures_Teams_HomeTeamID",
                table: "Fixtures");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Person_PlayerPersonId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_PlayerStats_CurrentPlayerStatsId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Record_PlayerRecordId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_SeasonStats_CurrentSeasonStatsId",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_TeamSheet_CurrentTeamSheetId",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Person_UserPersonId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserPersonId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Teams_CurrentSeasonStatsId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_CurrentTeamSheetId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Players_CurrentPlayerStatsId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_PlayerPersonId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_PlayerRecordId",
                table: "Players");

            migrationBuilder.AddColumn<long>(
                name: "CurrentTeamSheetId",
                table: "TeamSheet",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CurrentSeasonStatsId",
                table: "SeasonStats",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PlayerRecordId",
                table: "Record",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CurrentPlayerStatsId",
                table: "PlayerStats",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PlayerPersonId",
                table: "Person",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserPersonId",
                table: "Person",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeamSheet_CurrentTeamSheetId",
                table: "TeamSheet",
                column: "CurrentTeamSheetId",
                unique: true,
                filter: "[CurrentTeamSheetId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SeasonStats_CurrentSeasonStatsId",
                table: "SeasonStats",
                column: "CurrentSeasonStatsId",
                unique: true,
                filter: "[CurrentSeasonStatsId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Record_PlayerRecordId",
                table: "Record",
                column: "PlayerRecordId",
                unique: true,
                filter: "[PlayerRecordId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStats_CurrentPlayerStatsId",
                table: "PlayerStats",
                column: "CurrentPlayerStatsId",
                unique: true,
                filter: "[CurrentPlayerStatsId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Person_PlayerPersonId",
                table: "Person",
                column: "PlayerPersonId",
                unique: true,
                filter: "[PlayerPersonId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Person_UserPersonId",
                table: "Person",
                column: "UserPersonId",
                unique: true,
                filter: "[UserPersonId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Fixtures_Teams_HomeTeamID",
                table: "Fixtures",
                column: "HomeTeamID",
                principalTable: "Teams",
                principalColumn: "TeamId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Players_PlayerPersonId",
                table: "Person",
                column: "PlayerPersonId",
                principalTable: "Players",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Users_UserPersonId",
                table: "Person",
                column: "UserPersonId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerStats_Players_CurrentPlayerStatsId",
                table: "PlayerStats",
                column: "CurrentPlayerStatsId",
                principalTable: "Players",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Record_Players_PlayerRecordId",
                table: "Record",
                column: "PlayerRecordId",
                principalTable: "Players",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SeasonStats_Teams_CurrentSeasonStatsId",
                table: "SeasonStats",
                column: "CurrentSeasonStatsId",
                principalTable: "Teams",
                principalColumn: "TeamId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamSheet_Teams_CurrentTeamSheetId",
                table: "TeamSheet",
                column: "CurrentTeamSheetId",
                principalTable: "Teams",
                principalColumn: "TeamId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
