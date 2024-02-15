using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToggleOn.EntityFrameworkCore.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class CascadeFeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeatureFilter_FeatureToggle_FeatureToggleId",
                table: "FeatureFilter");

            migrationBuilder.DropForeignKey(
                name: "FK_FeatureFilterParameter_FeatureFilter_FeatureFilterId",
                table: "FeatureFilterParameter");

            migrationBuilder.DropForeignKey(
                name: "FK_FeatureToggle_Feature_FeatureId",
                table: "FeatureToggle");

            migrationBuilder.AddForeignKey(
                name: "FK_FeatureFilter_FeatureToggle_FeatureToggleId",
                table: "FeatureFilter",
                column: "FeatureToggleId",
                principalTable: "FeatureToggle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FeatureFilterParameter_FeatureFilter_FeatureFilterId",
                table: "FeatureFilterParameter",
                column: "FeatureFilterId",
                principalTable: "FeatureFilter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FeatureToggle_Feature_FeatureId",
                table: "FeatureToggle",
                column: "FeatureId",
                principalTable: "Feature",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeatureFilter_FeatureToggle_FeatureToggleId",
                table: "FeatureFilter");

            migrationBuilder.DropForeignKey(
                name: "FK_FeatureFilterParameter_FeatureFilter_FeatureFilterId",
                table: "FeatureFilterParameter");

            migrationBuilder.DropForeignKey(
                name: "FK_FeatureToggle_Feature_FeatureId",
                table: "FeatureToggle");

            migrationBuilder.AddForeignKey(
                name: "FK_FeatureFilter_FeatureToggle_FeatureToggleId",
                table: "FeatureFilter",
                column: "FeatureToggleId",
                principalTable: "FeatureToggle",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FeatureFilterParameter_FeatureFilter_FeatureFilterId",
                table: "FeatureFilterParameter",
                column: "FeatureFilterId",
                principalTable: "FeatureFilter",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FeatureToggle_Feature_FeatureId",
                table: "FeatureToggle",
                column: "FeatureId",
                principalTable: "Feature",
                principalColumn: "Id");
        }
    }
}
