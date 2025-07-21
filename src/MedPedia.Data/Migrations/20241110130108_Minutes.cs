using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teb.Data.Migrations
{
    /// <inheritdoc />
    public partial class Minutes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Minutes",
                schema: "dbo",
                table: "QuizTypes");

            migrationBuilder.AddColumn<int>(
                name: "Minutes",
                schema: "dbo",
                table: "Quizzes",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Minutes",
                schema: "dbo",
                table: "Quizzes");

            migrationBuilder.AddColumn<int>(
                name: "Minutes",
                schema: "dbo",
                table: "QuizTypes",
                type: "int",
                nullable: true);
        }
    }
}
