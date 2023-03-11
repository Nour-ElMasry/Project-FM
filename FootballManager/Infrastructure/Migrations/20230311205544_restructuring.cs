using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class restructuring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Managers_AspNetUsers_UserManagerId",
                table: "Managers");

            migrationBuilder.DropIndex(
                name: "IX_Managers_UserManagerId",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "UserManagerId",
                table: "Managers");

            migrationBuilder.AddColumn<int>(
                name: "CareerId",
                table: "Leagues",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Careers",
                columns: table => new
                {
                    CareerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CareerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CareerUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CareerManagerManagerId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Careers", x => x.CareerId);
                    table.ForeignKey(
                        name: "FK_Careers_AspNetUsers_CareerUserId",
                        column: x => x.CareerUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Careers_Managers_CareerManagerManagerId",
                        column: x => x.CareerManagerManagerId,
                        principalTable: "Managers",
                        principalColumn: "ManagerId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Leagues_CareerId",
                table: "Leagues",
                column: "CareerId");

            migrationBuilder.CreateIndex(
                name: "IX_Careers_CareerManagerManagerId",
                table: "Careers",
                column: "CareerManagerManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Careers_CareerUserId",
                table: "Careers",
                column: "CareerUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leagues_Careers_CareerId",
                table: "Leagues",
                column: "CareerId",
                principalTable: "Careers",
                principalColumn: "CareerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leagues_Careers_CareerId",
                table: "Leagues");

            migrationBuilder.DropTable(
                name: "Careers");

            migrationBuilder.DropIndex(
                name: "IX_Leagues_CareerId",
                table: "Leagues");

            migrationBuilder.DropColumn(
                name: "CareerId",
                table: "Leagues");

            migrationBuilder.AddColumn<string>(
                name: "UserManagerId",
                table: "Managers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Managers_UserManagerId",
                table: "Managers",
                column: "UserManagerId",
                unique: true,
                filter: "[UserManagerId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_AspNetUsers_UserManagerId",
                table: "Managers",
                column: "UserManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
