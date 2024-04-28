using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teb.Data.Migrations
{
    public partial class Experiment_Change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Abbreviation",
                schema: "dbo",
                table: "Experiments");

            migrationBuilder.DropColumn(
                name: "ClinicaNotes",
                schema: "dbo",
                table: "Experiments");

            migrationBuilder.DropColumn(
                name: "CommonCausesOfDeficiencyOrExcess",
                schema: "dbo",
                table: "Experiments");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "dbo",
                table: "Experiments");

            migrationBuilder.DropColumn(
                name: "InterferingFactors",
                schema: "dbo",
                table: "Experiments");

            migrationBuilder.DropColumn(
                name: "NormalSuffering",
                schema: "dbo",
                table: "Experiments");

            migrationBuilder.DropColumn(
                name: "PreSamplingConditions",
                schema: "dbo",
                table: "Experiments");

            migrationBuilder.DropColumn(
                name: "RelatedSymptomsOrDisease",
                schema: "dbo",
                table: "Experiments");

            migrationBuilder.DropColumn(
                name: "RelatedTests",
                schema: "dbo",
                table: "Experiments");

            migrationBuilder.DropColumn(
                name: "RequiredSample",
                schema: "dbo",
                table: "Experiments");

            migrationBuilder.DropColumn(
                name: "ScopeOfCrisis",
                schema: "dbo",
                table: "Experiments");

            migrationBuilder.DropColumn(
                name: "TestLimitations",
                schema: "dbo",
                table: "Experiments");

            migrationBuilder.DropColumn(
                name: "Usage",
                schema: "dbo",
                table: "Experiments");

            migrationBuilder.DropColumn(
                name: "VialType",
                schema: "dbo",
                table: "Experiments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Abbreviation",
                schema: "dbo",
                table: "Experiments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ClinicaNotes",
                schema: "dbo",
                table: "Experiments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CommonCausesOfDeficiencyOrExcess",
                schema: "dbo",
                table: "Experiments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "dbo",
                table: "Experiments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InterferingFactors",
                schema: "dbo",
                table: "Experiments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NormalSuffering",
                schema: "dbo",
                table: "Experiments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PreSamplingConditions",
                schema: "dbo",
                table: "Experiments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RelatedSymptomsOrDisease",
                schema: "dbo",
                table: "Experiments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RelatedTests",
                schema: "dbo",
                table: "Experiments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RequiredSample",
                schema: "dbo",
                table: "Experiments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ScopeOfCrisis",
                schema: "dbo",
                table: "Experiments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TestLimitations",
                schema: "dbo",
                table: "Experiments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Usage",
                schema: "dbo",
                table: "Experiments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VialType",
                schema: "dbo",
                table: "Experiments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
