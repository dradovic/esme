using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace esme.Infrastructure.Data.Migrations
{
    public partial class Membership : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CircleUser");

            migrationBuilder.CreateTable(
                name: "Membership",
                columns: table => new
                {
                    CircleId = table.Column<int>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    NumberOfReadMessages = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Membership", x => new { x.CircleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_Membership_Circles_CircleId",
                        column: x => x.CircleId,
                        principalTable: "Circles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Membership_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Membership_UserId",
                table: "Membership",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Membership");

            migrationBuilder.CreateTable(
                name: "CircleUser",
                columns: table => new
                {
                    CircleId = table.Column<int>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    NumberOfReadMessages = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CircleUser", x => new { x.CircleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_CircleUser_Circles_CircleId",
                        column: x => x.CircleId,
                        principalTable: "Circles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CircleUser_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CircleUser_UserId",
                table: "CircleUser",
                column: "UserId");
        }
    }
}
