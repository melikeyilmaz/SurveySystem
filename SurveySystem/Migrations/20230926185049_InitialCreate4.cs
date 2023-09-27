using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveySystem.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CorrectAnswers_Surveys_SurveyId",
                table: "CorrectAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyQuestions_Questions_QuestionsId",
                table: "SurveyQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyQuestions_Surveys_SurveysId",
                table: "SurveyQuestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SurveyQuestions",
                table: "SurveyQuestions");

            migrationBuilder.RenameTable(
                name: "SurveyQuestions",
                newName: "QuestionSurvey");

            migrationBuilder.RenameIndex(
                name: "IX_SurveyQuestions_SurveysId",
                table: "QuestionSurvey",
                newName: "IX_QuestionSurvey_SurveysId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionSurvey",
                table: "QuestionSurvey",
                columns: new[] { "QuestionsId", "SurveysId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CorrectAnswers_Surveys_SurveyId",
                table: "CorrectAnswers",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionSurvey_Questions_QuestionsId",
                table: "QuestionSurvey",
                column: "QuestionsId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionSurvey_Surveys_SurveysId",
                table: "QuestionSurvey",
                column: "SurveysId",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CorrectAnswers_Surveys_SurveyId",
                table: "CorrectAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionSurvey_Questions_QuestionsId",
                table: "QuestionSurvey");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionSurvey_Surveys_SurveysId",
                table: "QuestionSurvey");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionSurvey",
                table: "QuestionSurvey");

            migrationBuilder.RenameTable(
                name: "QuestionSurvey",
                newName: "SurveyQuestions");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionSurvey_SurveysId",
                table: "SurveyQuestions",
                newName: "IX_SurveyQuestions_SurveysId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SurveyQuestions",
                table: "SurveyQuestions",
                columns: new[] { "QuestionsId", "SurveysId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CorrectAnswers_Surveys_SurveyId",
                table: "CorrectAnswers",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyQuestions_Questions_QuestionsId",
                table: "SurveyQuestions",
                column: "QuestionsId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyQuestions_Surveys_SurveysId",
                table: "SurveyQuestions",
                column: "SurveysId",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
