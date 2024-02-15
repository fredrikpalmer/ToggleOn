using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToggleOn.EntityFrameworkCore.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class FilterParameters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "FeatureFilter");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Project",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "FeatureGroup",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Feature",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Environment",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Project_Name",
                table: "Project",
                column: "Name");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_FeatureGroup_Name",
                table: "FeatureGroup",
                column: "Name");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Feature_Name",
                table: "Feature",
                column: "Name");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Environment_Name",
                table: "Environment",
                column: "Name");

            migrationBuilder.CreateTable(
                name: "FeatureFilterParameter",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FeatureFilterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureFilterParameter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeatureFilterParameter_FeatureFilter_FeatureFilterId",
                        column: x => x.FeatureFilterId,
                        principalTable: "FeatureFilter",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeatureFilterParameter_FeatureFilterId",
                table: "FeatureFilterParameter",
                column: "FeatureFilterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeatureFilterParameter");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Project_Name",
                table: "Project");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_FeatureGroup_Name",
                table: "FeatureGroup");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Feature_Name",
                table: "Feature");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Environment_Name",
                table: "Environment");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Project",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "FeatureGroup",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "FeatureFilter",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Feature",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Environment",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
