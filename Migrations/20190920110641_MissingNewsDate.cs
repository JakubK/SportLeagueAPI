using Microsoft.EntityFrameworkCore.Migrations;

namespace SportLeagueAPI.Migrations
{
    public partial class MissingNewsDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Newses",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: "2012-02-03");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Newses",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: null);
        }
    }
}
