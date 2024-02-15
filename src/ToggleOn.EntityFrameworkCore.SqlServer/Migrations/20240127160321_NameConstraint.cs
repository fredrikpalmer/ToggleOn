using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToggleOn.EntityFrameworkCore.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class NameConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Feature_Name",
                table: "Feature");

            migrationBuilder.DropIndex(
                name: "IX_Feature_ProjectId",
                table: "Feature");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Environment_Name",
                table: "Environment");

            migrationBuilder.DropIndex(
                name: "IX_Environment_ProjectId",
                table: "Environment");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Feature_ProjectId_Name",
                table: "Feature",
                columns: new[] { "ProjectId", "Name" });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Environment_ProjectId_Name",
                table: "Environment",
                columns: new[] { "ProjectId", "Name" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Feature_ProjectId_Name",
                table: "Feature");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Environment_ProjectId_Name",
                table: "Environment");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Feature_Name",
                table: "Feature",
                column: "Name");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Environment_Name",
                table: "Environment",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Feature_ProjectId",
                table: "Feature",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Environment_ProjectId",
                table: "Environment",
                column: "ProjectId");
        }
    }
}
