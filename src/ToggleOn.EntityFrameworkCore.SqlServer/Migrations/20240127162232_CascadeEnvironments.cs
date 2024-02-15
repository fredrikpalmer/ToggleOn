using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToggleOn.EntityFrameworkCore.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class CascadeEnvironments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Environment_Project_ProjectId",
                table: "Environment");

            migrationBuilder.AddForeignKey(
                name: "FK_Environment_Project_ProjectId",
                table: "Environment",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Environment_Project_ProjectId",
                table: "Environment");

            migrationBuilder.AddForeignKey(
                name: "FK_Environment_Project_ProjectId",
                table: "Environment",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id");
        }
    }
}
