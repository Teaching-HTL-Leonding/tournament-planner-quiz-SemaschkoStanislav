using Microsoft.EntityFrameworkCore.Migrations;

namespace TournamentPlanner.Migrations
{
    public partial class OptionalWinnerId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Players_Player1Id",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Players_Player2Id",
                table: "Matches");

            migrationBuilder.RenameColumn(
                name: "Player2Id",
                table: "Matches",
                newName: "Player2ID");

            migrationBuilder.RenameColumn(
                name: "Player1Id",
                table: "Matches",
                newName: "Player1ID");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_Player2Id",
                table: "Matches",
                newName: "IX_Matches_Player2ID");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_Player1Id",
                table: "Matches",
                newName: "IX_Matches_Player1ID");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Players",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Players",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "WinnerOfTheMatchID",
                table: "Matches",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Players_Player1ID",
                table: "Matches",
                column: "Player1ID",
                principalTable: "Players",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Players_Player2ID",
                table: "Matches",
                column: "Player2ID",
                principalTable: "Players",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Players_Player1ID",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Players_Player2ID",
                table: "Matches");

            migrationBuilder.RenameColumn(
                name: "Player2ID",
                table: "Matches",
                newName: "Player2Id");

            migrationBuilder.RenameColumn(
                name: "Player1ID",
                table: "Matches",
                newName: "Player1Id");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_Player2ID",
                table: "Matches",
                newName: "IX_Matches_Player2Id");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_Player1ID",
                table: "Matches",
                newName: "IX_Matches_Player1Id");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<int>(
                name: "WinnerOfTheMatchID",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Players_Player1Id",
                table: "Matches",
                column: "Player1Id",
                principalTable: "Players",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Players_Player2Id",
                table: "Matches",
                column: "Player2Id",
                principalTable: "Players",
                principalColumn: "ID");
        }
    }
}
