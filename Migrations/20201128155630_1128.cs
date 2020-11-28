using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Backend.Migrations
{
    public partial class _1128 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentMeeting_Departments_DepartmentsName",
                table: "DepartmentMeeting");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentMeeting_Meetings_RelatedMeetingsMeetingID",
                table: "DepartmentMeeting");

            migrationBuilder.DropForeignKey(
                name: "FK_Meetings_Rooms_LocationName",
                table: "Meetings");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingUser_Meetings_AttendMeetingsMeetingID",
                table: "MeetingUser");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingUser_Users_AttendeesUserID",
                table: "MeetingUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Departments_DepartmentName",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_DepartmentName",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rooms",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Meetings_LocationName",
                table: "Meetings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Departments",
                table: "Departments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DepartmentMeeting",
                table: "DepartmentMeeting");

            migrationBuilder.DropColumn(
                name: "DepartmentName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LocationName",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "DepartmentsName",
                table: "DepartmentMeeting");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Users",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "AttendeesUserID",
                table: "MeetingUser",
                newName: "AttendeesID");

            migrationBuilder.RenameColumn(
                name: "AttendMeetingsMeetingID",
                table: "MeetingUser",
                newName: "AttendMeetingsID");

            migrationBuilder.RenameIndex(
                name: "IX_MeetingUser_AttendeesUserID",
                table: "MeetingUser",
                newName: "IX_MeetingUser_AttendeesID");

            migrationBuilder.RenameColumn(
                name: "MeetingID",
                table: "Meetings",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "RelatedMeetingsMeetingID",
                table: "DepartmentMeeting",
                newName: "RelatedMeetingsID");

            migrationBuilder.RenameIndex(
                name: "IX_DepartmentMeeting_RelatedMeetingsMeetingID",
                table: "DepartmentMeeting",
                newName: "IX_DepartmentMeeting_RelatedMeetingsID");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "Users",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DepartmentID",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateTime",
                table: "Users",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "Rooms",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "Rooms",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateTime",
                table: "Rooms",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "Meetings",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateTime",
                table: "Meetings",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "LocationID",
                table: "Meetings",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Departments",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "Departments",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "Departments",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateTime",
                table: "Departments",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DepartmentsID",
                table: "DepartmentMeeting",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rooms",
                table: "Rooms",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Departments",
                table: "Departments",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DepartmentMeeting",
                table: "DepartmentMeeting",
                columns: new[] { "DepartmentsID", "RelatedMeetingsID" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_DepartmentID",
                table: "Users",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_LocationID",
                table: "Meetings",
                column: "LocationID");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentMeeting_Departments_DepartmentsID",
                table: "DepartmentMeeting",
                column: "DepartmentsID",
                principalTable: "Departments",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentMeeting_Meetings_RelatedMeetingsID",
                table: "DepartmentMeeting",
                column: "RelatedMeetingsID",
                principalTable: "Meetings",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Meetings_Rooms_LocationID",
                table: "Meetings",
                column: "LocationID",
                principalTable: "Rooms",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingUser_Meetings_AttendMeetingsID",
                table: "MeetingUser",
                column: "AttendMeetingsID",
                principalTable: "Meetings",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingUser_Users_AttendeesID",
                table: "MeetingUser",
                column: "AttendeesID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Departments_DepartmentID",
                table: "Users",
                column: "DepartmentID",
                principalTable: "Departments",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentMeeting_Departments_DepartmentsID",
                table: "DepartmentMeeting");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentMeeting_Meetings_RelatedMeetingsID",
                table: "DepartmentMeeting");

            migrationBuilder.DropForeignKey(
                name: "FK_Meetings_Rooms_LocationID",
                table: "Meetings");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingUser_Meetings_AttendMeetingsID",
                table: "MeetingUser");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingUser_Users_AttendeesID",
                table: "MeetingUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Departments_DepartmentID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_DepartmentID",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rooms",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Meetings_LocationID",
                table: "Meetings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Departments",
                table: "Departments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DepartmentMeeting",
                table: "DepartmentMeeting");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DepartmentID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastUpdateTime",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "LastUpdateTime",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "LastUpdateTime",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "LocationID",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "LastUpdateTime",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "DepartmentsID",
                table: "DepartmentMeeting");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Users",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "AttendeesID",
                table: "MeetingUser",
                newName: "AttendeesUserID");

            migrationBuilder.RenameColumn(
                name: "AttendMeetingsID",
                table: "MeetingUser",
                newName: "AttendMeetingsMeetingID");

            migrationBuilder.RenameIndex(
                name: "IX_MeetingUser_AttendeesID",
                table: "MeetingUser",
                newName: "IX_MeetingUser_AttendeesUserID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Meetings",
                newName: "MeetingID");

            migrationBuilder.RenameColumn(
                name: "RelatedMeetingsID",
                table: "DepartmentMeeting",
                newName: "RelatedMeetingsMeetingID");

            migrationBuilder.RenameIndex(
                name: "IX_DepartmentMeeting_RelatedMeetingsID",
                table: "DepartmentMeeting",
                newName: "IX_DepartmentMeeting_RelatedMeetingsMeetingID");

            migrationBuilder.AddColumn<string>(
                name: "DepartmentName",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocationName",
                table: "Meetings",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Departments",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DepartmentsName",
                table: "DepartmentMeeting",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rooms",
                table: "Rooms",
                column: "Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Departments",
                table: "Departments",
                column: "Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DepartmentMeeting",
                table: "DepartmentMeeting",
                columns: new[] { "DepartmentsName", "RelatedMeetingsMeetingID" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_DepartmentName",
                table: "Users",
                column: "DepartmentName");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_LocationName",
                table: "Meetings",
                column: "LocationName");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentMeeting_Departments_DepartmentsName",
                table: "DepartmentMeeting",
                column: "DepartmentsName",
                principalTable: "Departments",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentMeeting_Meetings_RelatedMeetingsMeetingID",
                table: "DepartmentMeeting",
                column: "RelatedMeetingsMeetingID",
                principalTable: "Meetings",
                principalColumn: "MeetingID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Meetings_Rooms_LocationName",
                table: "Meetings",
                column: "LocationName",
                principalTable: "Rooms",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingUser_Meetings_AttendMeetingsMeetingID",
                table: "MeetingUser",
                column: "AttendMeetingsMeetingID",
                principalTable: "Meetings",
                principalColumn: "MeetingID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingUser_Users_AttendeesUserID",
                table: "MeetingUser",
                column: "AttendeesUserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Departments_DepartmentName",
                table: "Users",
                column: "DepartmentName",
                principalTable: "Departments",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
