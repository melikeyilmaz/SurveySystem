using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveySystem.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SurveyScores_Surveys_SurveyId",
                table: "SurveyScores");

            migrationBuilder.DropIndex(
                name: "IX_SurveyScores_SurveyId",
                table: "SurveyScores");

            migrationBuilder.DropColumn(
                name: "SurveyId",
                table: "SurveyScores");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SurveyId",
                table: "SurveyScores",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SurveyScores_SurveyId",
                table: "SurveyScores",
                column: "SurveyId");

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyScores_Surveys_SurveyId",
                table: "SurveyScores",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "Id");
        }
    }
}
