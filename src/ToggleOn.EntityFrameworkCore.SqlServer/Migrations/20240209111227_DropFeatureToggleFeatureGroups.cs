using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToggleOn.EntityFrameworkCore.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class DropFeatureToggleFeatureGroups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeatureGroups",
                table: "FeatureToggle");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FeatureGroups",
                table: "FeatureToggle",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
