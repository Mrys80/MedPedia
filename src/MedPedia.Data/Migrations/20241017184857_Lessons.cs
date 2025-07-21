using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teb.Data.Migrations
{
    /// <inheritdoc />
    public partial class Lessons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuizTypes",
                schema: "dbo");

            migrationBuilder.RenameColumn(
                name: "QuizTypeId",
                schema: "dbo",
                table: "QuizCategories",
                newName: "LessonId");

            migrationBuilder.CreateTable(
                name: "Lessons",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegUserId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lessons",
                schema: "dbo");

            migrationBuilder.RenameColumn(
                name: "LessonId",
                schema: "dbo",
                table: "QuizCategories",
                newName: "QuizTypeId");

            migrationBuilder.CreateTable(
                name: "QuizTypes",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegUserId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizTypes", x => x.Id);
                });
        }
    }
}
