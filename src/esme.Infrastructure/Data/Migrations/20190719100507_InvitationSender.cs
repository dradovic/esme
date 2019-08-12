using Microsoft.EntityFrameworkCore.Migrations;

namespace esme.Infrastructure.Data.Migrations
{
    public partial class InvitationSender : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invitations_AspNetUsers_ApplicationUserId",
                table: "Invitations");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Invitations",
                newName: "SentById");

            migrationBuilder.RenameIndex(
                name: "IX_Invitations_ApplicationUserId",
                table: "Invitations",
                newName: "IX_Invitations_SentById");

            migrationBuilder.AddForeignKey(
                name: "FK_Invitations_AspNetUsers_SentById",
                table: "Invitations",
                column: "SentById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invitations_AspNetUsers_SentById",
                table: "Invitations");

            migrationBuilder.RenameColumn(
                name: "SentById",
                table: "Invitations",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Invitations_SentById",
                table: "Invitations",
                newName: "IX_Invitations_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invitations_AspNetUsers_ApplicationUserId",
                table: "Invitations",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
