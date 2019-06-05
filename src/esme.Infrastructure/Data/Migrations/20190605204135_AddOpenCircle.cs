using Microsoft.EntityFrameworkCore.Migrations;

namespace esme.Infrastructure.Data.Migrations
{
    public partial class AddOpenCircle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Circles",
                columns: new[] { "Id", "Name" },
                values: new object[] { -1, "Open Circle" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Circles",
                keyColumn: "Id",
                keyValue: -1);
        }
    }
}
