using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class AddAlternateUserProjectKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserProjects_UserId",
                table: "UserProjects");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_UserProjects_UserId_ProjectId",
                table: "UserProjects",
                columns: new[] { "UserId", "ProjectId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_UserProjects_UserId_ProjectId",
                table: "UserProjects");

            migrationBuilder.CreateIndex(
                name: "IX_UserProjects_UserId",
                table: "UserProjects",
                column: "UserId");
        }
    }
}
