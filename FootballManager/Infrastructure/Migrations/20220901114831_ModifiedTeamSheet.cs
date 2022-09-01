using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class ModifiedTeamSheet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_TeamSheet_TeamSheetId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_TeamSheetId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "TeamSheetId",
                table: "Players");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TeamSheetId",
                table: "Players",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_TeamSheetId",
                table: "Players",
                column: "TeamSheetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_TeamSheet_TeamSheetId",
                table: "Players",
                column: "TeamSheetId",
                principalTable: "TeamSheet",
                principalColumn: "TeamSheetId");
        }
    }
}
