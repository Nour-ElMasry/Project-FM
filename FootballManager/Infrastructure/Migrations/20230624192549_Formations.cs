using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class Formations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TeamFormationId",
                table: "TeamSheet",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TeamSheetId",
                table: "Players",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TeamSheetId1",
                table: "Players",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Formation",
                columns: table => new
                {
                    FormationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Defenders = table.Column<int>(type: "int", nullable: false),
                    Midfielders = table.Column<int>(type: "int", nullable: false),
                    Attackers = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formation", x => x.FormationId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamSheet_TeamFormationId",
                table: "TeamSheet",
                column: "TeamFormationId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_TeamSheetId",
                table: "Players",
                column: "TeamSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_TeamSheetId1",
                table: "Players",
                column: "TeamSheetId1");

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

            migrationBuilder.AddForeignKey(
                name: "FK_TeamSheet_Formation_TeamFormationId",
                table: "TeamSheet",
                column: "TeamFormationId",
                principalTable: "Formation",
                principalColumn: "FormationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_TeamSheet_TeamSheetId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_TeamSheet_TeamSheetId1",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamSheet_Formation_TeamFormationId",
                table: "TeamSheet");

            migrationBuilder.DropTable(
                name: "Formation");

            migrationBuilder.DropIndex(
                name: "IX_TeamSheet_TeamFormationId",
                table: "TeamSheet");

            migrationBuilder.DropIndex(
                name: "IX_Players_TeamSheetId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_TeamSheetId1",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "TeamFormationId",
                table: "TeamSheet");

            migrationBuilder.DropColumn(
                name: "TeamSheetId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "TeamSheetId1",
                table: "Players");
        }
    }
}
