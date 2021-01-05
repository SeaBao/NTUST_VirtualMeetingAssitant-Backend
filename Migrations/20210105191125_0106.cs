using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class _0106 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatorID",
                table: "Meetings",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_CreatorID",
                table: "Meetings",
                column: "CreatorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Meetings_Users_CreatorID",
                table: "Meetings",
                column: "CreatorID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meetings_Users_CreatorID",
                table: "Meetings");

            migrationBuilder.DropIndex(
                name: "IX_Meetings_CreatorID",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "CreatorID",
                table: "Meetings");
        }
    }
}
