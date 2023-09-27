using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveySystem.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CorrectAnswers_Surveys_SurveyId",
                table: "CorrectAnswers");

            migrationBuilder.AddForeignKey(
                name: "FK_CorrectAnswers_Surveys_SurveyId",
                table: "CorrectAnswers",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CorrectAnswers_Surveys_SurveyId",
                table: "CorrectAnswers");

            migrationBuilder.AddForeignKey(
                name: "FK_CorrectAnswers_Surveys_SurveyId",
                table: "CorrectAnswers",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
