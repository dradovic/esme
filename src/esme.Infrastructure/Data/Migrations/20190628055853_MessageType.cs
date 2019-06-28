using Microsoft.EntityFrameworkCore.Migrations;

namespace esme.Infrastructure.Data.Migrations
{
    public partial class MessageType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Messages",
                newName: "Content");

            migrationBuilder.AddColumn<byte>(
                name: "ContentType",
                table: "Messages",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Messages",
                newName: "Text");
        }
    }
}
