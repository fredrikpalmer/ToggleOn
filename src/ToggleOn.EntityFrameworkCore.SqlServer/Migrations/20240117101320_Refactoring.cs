using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToggleOn.EntityFrameworkCore.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class Refactoring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Environment_Project_ProjectId",
                table: "Environment");

            migrationBuilder.DropTable(
                name: "FeatureRule");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "FeatureToggle");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "FeatureToggle",
                newName: "FeatureId");

            migrationBuilder.AlterColumn<string>(
                name: "FeatureGroups",
                table: "FeatureToggle",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UserIds",
                table: "FeatureGroup",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "IpAddresses",
                table: "FeatureGroup",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Feature",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feature_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FeatureFilter",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FeatureToggleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureFilter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeatureFilter_FeatureToggle_FeatureToggleId",
                        column: x => x.FeatureToggleId,
                        principalTable: "FeatureToggle",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeatureToggle_EnvironmentId",
                table: "FeatureToggle",
                column: "EnvironmentId");

            migrationBuilder.CreateIndex(
                name: "IX_FeatureToggle_FeatureId",
                table: "FeatureToggle",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_Feature_ProjectId",
                table: "Feature",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_FeatureFilter_FeatureToggleId",
                table: "FeatureFilter",
                column: "FeatureToggleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Environment_Project_ProjectId",
                table: "Environment",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FeatureToggle_Environment_EnvironmentId",
                table: "FeatureToggle",
                column: "EnvironmentId",
                principalTable: "Environment",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FeatureToggle_Feature_FeatureId",
                table: "FeatureToggle",
                column: "FeatureId",
                principalTable: "Feature",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Environment_Project_ProjectId",
                table: "Environment");

            migrationBuilder.DropForeignKey(
                name: "FK_FeatureToggle_Environment_EnvironmentId",
                table: "FeatureToggle");

            migrationBuilder.DropForeignKey(
                name: "FK_FeatureToggle_Feature_FeatureId",
                table: "FeatureToggle");

            migrationBuilder.DropTable(
                name: "Feature");

            migrationBuilder.DropTable(
                name: "FeatureFilter");

            migrationBuilder.DropIndex(
                name: "IX_FeatureToggle_EnvironmentId",
                table: "FeatureToggle");

            migrationBuilder.DropIndex(
                name: "IX_FeatureToggle_FeatureId",
                table: "FeatureToggle");

            migrationBuilder.RenameColumn(
                name: "FeatureId",
                table: "FeatureToggle",
                newName: "ProjectId");

            migrationBuilder.AlterColumn<string>(
                name: "FeatureGroups",
                table: "FeatureToggle",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "FeatureToggle",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "UserIds",
                table: "FeatureGroup",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IpAddresses",
                table: "FeatureGroup",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "FeatureRule",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FeatureToggleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureRule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeatureRule_FeatureToggle_FeatureToggleId",
                        column: x => x.FeatureToggleId,
                        principalTable: "FeatureToggle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeatureRule_FeatureToggleId",
                table: "FeatureRule",
                column: "FeatureToggleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Environment_Project_ProjectId",
                table: "Environment",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
