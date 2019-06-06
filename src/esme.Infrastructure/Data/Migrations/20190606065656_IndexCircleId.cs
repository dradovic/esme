using Microsoft.EntityFrameworkCore.Migrations;

namespace esme.Infrastructure.Data.Migrations
{
    public partial class IndexCircleId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Messages_CircleId",
                table: "Messages",
                column: "CircleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Messages_CircleId",
                table: "Messages");
        }
    }
}
