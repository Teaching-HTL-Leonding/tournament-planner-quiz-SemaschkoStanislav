﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace TournamentPlanner.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Round = table.Column<int>(type: "int", nullable: false),
                    Player1Id = table.Column<int>(type: "int", nullable: false),
                    Player2Id = table.Column<int>(type: "int", nullable: false),
                    WinnerOfTheMatchID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Matches_Players_Player1Id",
                        column: x => x.Player1Id,
                        principalTable: "Players",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Matches_Players_Player2Id",
                        column: x => x.Player2Id,
                        principalTable: "Players",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Matches_Players_WinnerOfTheMatchID",
                        column: x => x.WinnerOfTheMatchID,
                        principalTable: "Players",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_Player1Id",
                table: "Matches",
                column: "Player1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_Player2Id",
                table: "Matches",
                column: "Player2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_WinnerOfTheMatchID",
                table: "Matches",
                column: "WinnerOfTheMatchID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
