using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teb.Data.Migrations
{
    public partial class ChangesInDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                schema: "dbo",
                table: "Diseases",
                newName: "WhenVisit");

            migrationBuilder.AddColumn<string>(
                name: "BasicPointsOfTreatment",
                schema: "dbo",
                table: "Diseases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Cause",
                schema: "dbo",
                table: "Diseases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ChangeOfLifestyle",
                schema: "dbo",
                table: "Diseases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Complications",
                schema: "dbo",
                table: "Diseases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DefinitionOfDisease",
                schema: "dbo",
                table: "Diseases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DiagnosticTests",
                schema: "dbo",
                table: "Diseases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DrugTreatment",
                schema: "dbo",
                table: "Diseases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EnTitle",
                schema: "dbo",
                table: "Diseases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FaTitle",
                schema: "dbo",
                table: "Diseases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImportantPoints",
                schema: "dbo",
                table: "Diseases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PreparingForVisit",
                schema: "dbo",
                table: "Diseases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Prevention",
                schema: "dbo",
                table: "Diseases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Recovery",
                schema: "dbo",
                table: "Diseases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RiskFactors",
                schema: "dbo",
                table: "Diseases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Signs",
                schema: "dbo",
                table: "Diseases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TransmissionWays",
                schema: "dbo",
                table: "Diseases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Treatment",
                schema: "dbo",
                table: "Diseases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ExperimentCategories",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegUserId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperimentCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Experiments",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FaTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Usage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InterferingFactors = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NormalSuffering = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScopeOfCrisis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommonCausesOfDeficiencyOrExcess = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RelatedSymptomsOrDisease = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreSamplingConditions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequiredSample = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VialType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RelatedTests = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClinicaNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestLimitations = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExperimentCategoryId = table.Column<int>(type: "int", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegUserId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiments", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExperimentCategories",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Experiments",
                schema: "dbo");

            migrationBuilder.DropColumn(
                name: "BasicPointsOfTreatment",
                schema: "dbo",
                table: "Diseases");

            migrationBuilder.DropColumn(
                name: "Cause",
                schema: "dbo",
                table: "Diseases");

            migrationBuilder.DropColumn(
                name: "ChangeOfLifestyle",
                schema: "dbo",
                table: "Diseases");

            migrationBuilder.DropColumn(
                name: "Complications",
                schema: "dbo",
                table: "Diseases");

            migrationBuilder.DropColumn(
                name: "DefinitionOfDisease",
                schema: "dbo",
                table: "Diseases");

            migrationBuilder.DropColumn(
                name: "DiagnosticTests",
                schema: "dbo",
                table: "Diseases");

            migrationBuilder.DropColumn(
                name: "DrugTreatment",
                schema: "dbo",
                table: "Diseases");

            migrationBuilder.DropColumn(
                name: "EnTitle",
                schema: "dbo",
                table: "Diseases");

            migrationBuilder.DropColumn(
                name: "FaTitle",
                schema: "dbo",
                table: "Diseases");

            migrationBuilder.DropColumn(
                name: "ImportantPoints",
                schema: "dbo",
                table: "Diseases");

            migrationBuilder.DropColumn(
                name: "PreparingForVisit",
                schema: "dbo",
                table: "Diseases");

            migrationBuilder.DropColumn(
                name: "Prevention",
                schema: "dbo",
                table: "Diseases");

            migrationBuilder.DropColumn(
                name: "Recovery",
                schema: "dbo",
                table: "Diseases");

            migrationBuilder.DropColumn(
                name: "RiskFactors",
                schema: "dbo",
                table: "Diseases");

            migrationBuilder.DropColumn(
                name: "Signs",
                schema: "dbo",
                table: "Diseases");

            migrationBuilder.DropColumn(
                name: "TransmissionWays",
                schema: "dbo",
                table: "Diseases");

            migrationBuilder.DropColumn(
                name: "Treatment",
                schema: "dbo",
                table: "Diseases");

            migrationBuilder.RenameColumn(
                name: "WhenVisit",
                schema: "dbo",
                table: "Diseases",
                newName: "Title");
        }
    }
}
