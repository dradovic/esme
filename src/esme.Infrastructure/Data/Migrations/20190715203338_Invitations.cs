using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace esme.Infrastructure.Data.Migrations
{
    public partial class Invitations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Messages",
                maxLength: 8192,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 8192,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Invitation",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    To = table.Column<string>(nullable: false),
                    SentAt = table.Column<DateTimeOffset>(nullable: false),
                    AcceptedAt = table.Column<DateTimeOffset>(nullable: true),
                    ApplicationUserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invitation_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invitation_ApplicationUserId",
                table: "Invitation",
                column: "ApplicationUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invitation");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Messages",
                maxLength: 8192,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 8192);
        }
    }
}
