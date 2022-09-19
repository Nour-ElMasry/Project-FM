using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class FixtureEventsModification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_Fixtures_FixtureId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Players_GoalAssisterPlayerId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Players_GoalScorerPlayerId",
                table: "Event");

            migrationBuilder.RenameColumn(
                name: "GoalScorerPlayerId",
                table: "Event",
                newName: "GoalScorerId");

            migrationBuilder.RenameColumn(
                name: "GoalAssisterPlayerId",
                table: "Event",
                newName: "GoalAssisterId");

            migrationBuilder.RenameColumn(
                name: "FixtureId",
                table: "Event",
                newName: "EventFixtureId");

            migrationBuilder.RenameIndex(
                name: "IX_Event_GoalScorerPlayerId",
                table: "Event",
                newName: "IX_Event_GoalScorerId");

            migrationBuilder.RenameIndex(
                name: "IX_Event_GoalAssisterPlayerId",
                table: "Event",
                newName: "IX_Event_GoalAssisterId");

            migrationBuilder.RenameIndex(
                name: "IX_Event_FixtureId",
                table: "Event",
                newName: "IX_Event_EventFixtureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Fixtures_EventFixtureId",
                table: "Event",
                column: "EventFixtureId",
                principalTable: "Fixtures",
                principalColumn: "FixtureId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Players_GoalAssisterId",
                table: "Event",
                column: "GoalAssisterId",
                principalTable: "Players",
                principalColumn: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Players_GoalScorerId",
                table: "Event",
                column: "GoalScorerId",
                principalTable: "Players",
                principalColumn: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_Fixtures_EventFixtureId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Players_GoalAssisterId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Players_GoalScorerId",
                table: "Event");

            migrationBuilder.RenameColumn(
                name: "GoalScorerId",
                table: "Event",
                newName: "GoalScorerPlayerId");

            migrationBuilder.RenameColumn(
                name: "GoalAssisterId",
                table: "Event",
                newName: "GoalAssisterPlayerId");

            migrationBuilder.RenameColumn(
                name: "EventFixtureId",
                table: "Event",
                newName: "FixtureId");

            migrationBuilder.RenameIndex(
                name: "IX_Event_GoalScorerId",
                table: "Event",
                newName: "IX_Event_GoalScorerPlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_Event_GoalAssisterId",
                table: "Event",
                newName: "IX_Event_GoalAssisterPlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_Event_EventFixtureId",
                table: "Event",
                newName: "IX_Event_FixtureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Fixtures_FixtureId",
                table: "Event",
                column: "FixtureId",
                principalTable: "Fixtures",
                principalColumn: "FixtureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Players_GoalAssisterPlayerId",
                table: "Event",
                column: "GoalAssisterPlayerId",
                principalTable: "Players",
                principalColumn: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Players_GoalScorerPlayerId",
                table: "Event",
                column: "GoalScorerPlayerId",
                principalTable: "Players",
                principalColumn: "PlayerId");
        }
    }
}
