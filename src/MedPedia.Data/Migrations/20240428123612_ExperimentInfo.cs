using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teb.Data.Migrations
{
    public partial class ExperimentInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExperimentInformations",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExperimentId = table.Column<int>(type: "int", nullable: false),
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
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegUserId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperimentInformations", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExperimentInformations",
                schema: "dbo");
        }
    }
}
