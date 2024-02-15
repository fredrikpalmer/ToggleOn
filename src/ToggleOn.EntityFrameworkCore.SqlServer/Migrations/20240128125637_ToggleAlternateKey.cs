using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToggleOn.EntityFrameworkCore.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class ToggleAlternateKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FeatureToggle_FeatureId",
                table: "FeatureToggle");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_FeatureToggle_FeatureId_EnvironmentId",
                table: "FeatureToggle",
                columns: new[] { "FeatureId", "EnvironmentId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_FeatureToggle_FeatureId_EnvironmentId",
                table: "FeatureToggle");

            migrationBuilder.CreateIndex(
                name: "IX_FeatureToggle_FeatureId",
                table: "FeatureToggle",
                column: "FeatureId");
        }
    }
}
