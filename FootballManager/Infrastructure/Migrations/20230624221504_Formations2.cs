using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class Formations2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_TeamSheet_TeamSheetId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_TeamSheet_TeamSheetId1",
                table: "Players");

            migrationBuilder.RenameColumn(
                name: "TeamSheetId1",
                table: "Players",
                newName: "StartingElevenPlayers");

            migrationBuilder.RenameColumn(
                name: "TeamSheetId",
                table: "Players",
                newName: "BenchPlayers");

            migrationBuilder.RenameIndex(
                name: "IX_Players_TeamSheetId1",
                table: "Players",
                newName: "IX_Players_StartingElevenPlayers");

            migrationBuilder.RenameIndex(
                name: "IX_Players_TeamSheetId",
                table: "Players",
                newName: "IX_Players_BenchPlayers");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_TeamSheet_BenchPlayers",
                table: "Players",
                column: "BenchPlayers",
                principalTable: "TeamSheet",
                principalColumn: "TeamSheetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_TeamSheet_StartingElevenPlayers",
                table: "Players",
                column: "StartingElevenPlayers",
                principalTable: "TeamSheet",
                principalColumn: "TeamSheetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_TeamSheet_BenchPlayers",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_TeamSheet_StartingElevenPlayers",
                table: "Players");

            migrationBuilder.RenameColumn(
                name: "StartingElevenPlayers",
                table: "Players",
                newName: "TeamSheetId1");

            migrationBuilder.RenameColumn(
                name: "BenchPlayers",
                table: "Players",
                newName: "TeamSheetId");

            migrationBuilder.RenameIndex(
                name: "IX_Players_StartingElevenPlayers",
                table: "Players",
                newName: "IX_Players_TeamSheetId1");

            migrationBuilder.RenameIndex(
                name: "IX_Players_BenchPlayers",
                table: "Players",
                newName: "IX_Players_TeamSheetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_TeamSheet_TeamSheetId",
                table: "Players",
                column: "TeamSheetId",
                principalTable: "TeamSheet",
                principalColumn: "TeamSheetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_TeamSheet_TeamSheetId1",
                table: "Players",
                column: "TeamSheetId1",
                principalTable: "TeamSheet",
                principalColumn: "TeamSheetId");
        }
    }
}
