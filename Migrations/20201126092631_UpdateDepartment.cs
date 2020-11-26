using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class UpdateDepartment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Meetings_MeetingID",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_MeetingID",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "MeetingID",
                table: "Departments");

            migrationBuilder.CreateTable(
                name: "DepartmentMeeting",
                columns: table => new
                {
                    DepartmentsName = table.Column<string>(type: "text", nullable: false),
                    RelatedMeetingsMeetingID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentMeeting", x => new { x.DepartmentsName, x.RelatedMeetingsMeetingID });
                    table.ForeignKey(
                        name: "FK_DepartmentMeeting_Departments_DepartmentsName",
                        column: x => x.DepartmentsName,
                        principalTable: "Departments",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentMeeting_Meetings_RelatedMeetingsMeetingID",
                        column: x => x.RelatedMeetingsMeetingID,
                        principalTable: "Meetings",
                        principalColumn: "MeetingID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentMeeting_RelatedMeetingsMeetingID",
                table: "DepartmentMeeting",
                column: "RelatedMeetingsMeetingID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmentMeeting");

            migrationBuilder.AddColumn<int>(
                name: "MeetingID",
                table: "Departments",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_MeetingID",
                table: "Departments",
                column: "MeetingID");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Meetings_MeetingID",
                table: "Departments",
                column: "MeetingID",
                principalTable: "Meetings",
                principalColumn: "MeetingID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
