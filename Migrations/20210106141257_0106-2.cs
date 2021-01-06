using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class _01062 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "DepartmentID", "Email", "GroupType", "IsNeededChangePassword", "IsVerified", "Name", "Password" },
                values: new object[] { 1, null, "manager@example.com", 1, true, true, "Manager", "$2a$11$issCIamZCReOitXzYbpZ0eJCEMDQO7R28PqCQTtMn5a1/7ezQn2Fe" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1);
        }
    }
}
