using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SportLeagueAPI.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medias",
                columns: table => new
                {
                    Url = table.Column<string>(nullable: false),
                    OwnerId = table.Column<int>(nullable: false),
                    EventId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medias", x => x.Url);
                    table.ForeignKey(
                        name: "FK_Medias_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    MediaUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Newses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Newses_Medias_MediaUrl",
                        column: x => x.MediaUrl,
                        principalTable: "Medias",
                        principalColumn: "Url",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    MediaUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Medias_MediaUrl",
                        column: x => x.MediaUrl,
                        principalTable: "Medias",
                        principalColumn: "Url",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Settlements",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    MediaUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settlements", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Settlements_Medias_MediaUrl",
                        column: x => x.MediaUrl,
                        principalTable: "Medias",
                        principalColumn: "Url",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Date", "Description", "Name" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cool Event" });

            migrationBuilder.InsertData(
                table: "Medias",
                columns: new[] { "Url", "EventId", "OwnerId" },
                values: new object[] { "https://google.com", null, 1 });

            migrationBuilder.InsertData(
                table: "Medias",
                columns: new[] { "Url", "EventId", "OwnerId" },
                values: new object[] { "https://wp.pl", null, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Medias_EventId",
                table: "Medias",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Newses_MediaUrl",
                table: "Newses",
                column: "MediaUrl");

            migrationBuilder.CreateIndex(
                name: "IX_Players_MediaUrl",
                table: "Players",
                column: "MediaUrl");

            migrationBuilder.CreateIndex(
                name: "IX_Settlements_MediaUrl",
                table: "Settlements",
                column: "MediaUrl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Newses");

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
