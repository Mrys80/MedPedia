using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teb.Data.Migrations
{
    public partial class DiseaseInformation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropColumn(
                name: "WhenVisit",
                schema: "dbo",
                table: "Diseases");

            migrationBuilder.CreateTable(
                name: "DiseaseInformations",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DefinitionOfDisease = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Signs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cause = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RiskFactors = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransmissionWays = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Complications = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreparingForVisit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhenVisit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Recovery = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiagnosticTests = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DrugTreatment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Treatment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChangeOfLifestyle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImportantPoints = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BasicPointsOfTreatment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prevention = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegUserId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiseaseInformations", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiseaseInformations",
                schema: "dbo");

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

            migrationBuilder.AddColumn<string>(
                name: "WhenVisit",
                schema: "dbo",
                table: "Diseases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
