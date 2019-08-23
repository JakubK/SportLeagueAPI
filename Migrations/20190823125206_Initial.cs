using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SportLeagueAPI.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Medias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Url = table.Column<string>(nullable: true),
                    EventId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Newses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    MediaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Newses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Newses_Medias_MediaId",
                        column: x => x.MediaId,
                        principalTable: "Medias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Settlements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    MediaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settlements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Settlements_Medias_MediaId",
                        column: x => x.MediaId,
                        principalTable: "Medias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    SettlementId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Settlements_SettlementId",
                        column: x => x.SettlementId,
                        principalTable: "Settlements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    SettlementId = table.Column<int>(nullable: true),
                    MediaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Medias_MediaId",
                        column: x => x.MediaId,
                        principalTable: "Medias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Players_Settlements_SettlementId",
                        column: x => x.SettlementId,
                        principalTable: "Settlements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Scores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Value = table.Column<int>(nullable: false),
                    PlayerId = table.Column<int>(nullable: true),
                    EventId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Scores_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Scores_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Medias",
                columns: new[] { "Id", "EventId", "Url" },
                values: new object[] { 1, null, "https://google.com" });

            migrationBuilder.InsertData(
                table: "Medias",
                columns: new[] { "Id", "EventId", "Url" },
                values: new object[] { 2, null, "https://wp.pl" });

            migrationBuilder.InsertData(
                table: "Medias",
                columns: new[] { "Id", "EventId", "Url" },
                values: new object[] { 3, null, "https://wp.pl" });

            migrationBuilder.InsertData(
                table: "Medias",
                columns: new[] { "Id", "EventId", "Url" },
                values: new object[] { 4, null, "https://wp.pl" });

            migrationBuilder.InsertData(
                table: "Medias",
                columns: new[] { "Id", "EventId", "Url" },
                values: new object[] { 5, null, "https://wp.pl" });

            migrationBuilder.InsertData(
                table: "Newses",
                columns: new[] { "Id", "Date", "Description", "MediaId", "Name" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 5, "Test News" });

            migrationBuilder.InsertData(
                table: "Settlements",
                columns: new[] { "Id", "Description", "MediaId", "Name" },
                values: new object[] { 2, null, 2, "Settlement 2" });

            migrationBuilder.InsertData(
                table: "Settlements",
                columns: new[] { "Id", "Description", "MediaId", "Name" },
                values: new object[] { 1, null, 1, "Settlement 1" });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Date", "Description", "Name", "SettlementId" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Test Event", 1 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "MediaId", "Name", "SettlementId" },
                values: new object[] { 1, 3, "Player 1", 1 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "MediaId", "Name", "SettlementId" },
                values: new object[] { 2, 4, "Player 2", 2 });

            migrationBuilder.InsertData(
                table: "Medias",
                columns: new[] { "Id", "EventId", "Url" },
                values: new object[] { 6, 1, "https://wp.pl" });

            migrationBuilder.InsertData(
                table: "Medias",
                columns: new[] { "Id", "EventId", "Url" },
                values: new object[] { 7, 1, "https://wp.pl" });

            migrationBuilder.InsertData(
                table: "Scores",
                columns: new[] { "Id", "EventId", "PlayerId", "Value" },
                values: new object[] { 1, 1, 1, 10 });

            migrationBuilder.InsertData(
                table: "Scores",
                columns: new[] { "Id", "EventId", "PlayerId", "Value" },
                values: new object[] { 2, 1, 2, 10 });

            migrationBuilder.CreateIndex(
                name: "IX_Events_SettlementId",
                table: "Events",
                column: "SettlementId");

            migrationBuilder.CreateIndex(
                name: "IX_Medias_EventId",
                table: "Medias",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Newses_MediaId",
                table: "Newses",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_MediaId",
                table: "Players",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_SettlementId",
                table: "Players",
                column: "SettlementId");

            migrationBuilder.CreateIndex(
                name: "IX_Scores_EventId",
                table: "Scores",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Scores_PlayerId",
                table: "Scores",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Settlements_MediaId",
                table: "Settlements",
                column: "MediaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_Events_EventId",
                table: "Medias",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Settlements_SettlementId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "Newses");

            migrationBuilder.DropTable(
                name: "Scores");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Settlements");

            migrationBuilder.DropTable(
                name: "Medias");

            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
