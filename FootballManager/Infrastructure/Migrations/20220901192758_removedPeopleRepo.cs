using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class removedPeopleRepo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Managers_People_ManagerPersonId",
                table: "Managers");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_People_PlayerPersonId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_People_UserPersonId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_People",
                table: "People");

            migrationBuilder.RenameTable(
                name: "People",
                newName: "Person");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Person",
                table: "Person",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_Person_ManagerPersonId",
                table: "Managers",
                column: "ManagerPersonId",
                principalTable: "Person",
                principalColumn: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Person_PlayerPersonId",
                table: "Players",
                column: "PlayerPersonId",
                principalTable: "Person",
                principalColumn: "PersonId");

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
                name: "FK_Managers_Person_ManagerPersonId",
                table: "Managers");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Person_PlayerPersonId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Person_UserPersonId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Person",
                table: "Person");

            migrationBuilder.RenameTable(
                name: "Person",
                newName: "People");

            migrationBuilder.AddPrimaryKey(
                name: "PK_People",
                table: "People",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_People_ManagerPersonId",
                table: "Managers",
                column: "ManagerPersonId",
                principalTable: "People",
                principalColumn: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_People_PlayerPersonId",
                table: "Players",
                column: "PlayerPersonId",
                principalTable: "People",
                principalColumn: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_People_UserPersonId",
                table: "Users",
                column: "UserPersonId",
                principalTable: "People",
                principalColumn: "PersonId");
        }
    }
}
