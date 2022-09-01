using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class SlightModifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Record_PlayerRecordId",
                table: "Players");

            migrationBuilder.AlterColumn<long>(
                name: "PlayerRecordId",
                table: "Players",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Record_PlayerRecordId",
                table: "Players",
                column: "PlayerRecordId",
                principalTable: "Record",
                principalColumn: "RecordId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Record_PlayerRecordId",
                table: "Players");

            migrationBuilder.AlterColumn<long>(
                name: "PlayerRecordId",
                table: "Players",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Record_PlayerRecordId",
                table: "Players",
                column: "PlayerRecordId",
                principalTable: "Record",
                principalColumn: "RecordId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
