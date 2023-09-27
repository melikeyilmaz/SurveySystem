using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveySystem.Migrations
{
    /// <inheritdoc />
    public partial class SurveyQuestiongeri : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CorrectAnswers_SurveyQuestions_SurveyId_QuestionId",
                table: "CorrectAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyQuestions_Questions_QuestionId",
                table: "SurveyQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyQuestions_Surveys_SurveyId",
                table: "SurveyQuestions");

            migrationBuilder.DropTable(
                name: "QuestionSurvey");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SurveyQuestions",
                table: "SurveyQuestions");

            migrationBuilder.DropIndex(
                name: "IX_SurveyQuestions_QuestionId",
                table: "SurveyQuestions");

            migrationBuilder.DropIndex(
                name: "IX_CorrectAnswers_SurveyId_QuestionId",
                table: "CorrectAnswers");

            migrationBuilder.DropColumn(
                name: "SurveyId",
                table: "SurveyQuestions");

            migrationBuilder.DropColumn(
                name: "AnswerText",
                table: "CorrectAnswers");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "SurveyQuestions",
                newName: "SurveysId");

            migrationBuilder.RenameColumn(
                name: "QuestionId",
                table: "SurveyQuestions",
                newName: "QuestionsId");

            migrationBuilder.AddColumn<int>(
                name: "CorrectChoiceId",
                table: "CorrectAnswers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SurveyQuestions",
                table: "SurveyQuestions",
                columns: new[] { "QuestionsId", "SurveysId" });

            migrationBuilder.CreateIndex(
                name: "IX_SurveyQuestions_SurveysId",
                table: "SurveyQuestions",
                column: "SurveysId");

            migrationBuilder.CreateIndex(
                name: "IX_CorrectAnswers_QuestionId",
                table: "CorrectAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_CorrectAnswers_SurveyId",
                table: "CorrectAnswers",
                column: "SurveyId");

            migrationBuilder.AddForeignKey(
                name: "FK_CorrectAnswers_Questions_QuestionId",
                table: "CorrectAnswers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CorrectAnswers_Questions_QuestionId",
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

            migrationBuilder.DropIndex(
                name: "IX_SurveyQuestions_SurveysId",
                table: "SurveyQuestions");

            migrationBuilder.DropIndex(
                name: "IX_CorrectAnswers_QuestionId",
                table: "CorrectAnswers");

            migrationBuilder.DropIndex(
                name: "IX_CorrectAnswers_SurveyId",
                table: "CorrectAnswers");

            migrationBuilder.DropColumn(
                name: "CorrectChoiceId",
                table: "CorrectAnswers");

            migrationBuilder.RenameColumn(
                name: "SurveysId",
                table: "SurveyQuestions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "QuestionsId",
                table: "SurveyQuestions",
                newName: "QuestionId");

            migrationBuilder.AddColumn<int>(
                name: "SurveyId",
                table: "SurveyQuestions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AnswerText",
                table: "CorrectAnswers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SurveyQuestions",
                table: "SurveyQuestions",
                columns: new[] { "SurveyId", "QuestionId" });

            migrationBuilder.CreateTable(
                name: "QuestionSurvey",
                columns: table => new
                {
                    QuestionsId = table.Column<int>(type: "int", nullable: false),
                    SurveysId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionSurvey", x => new { x.QuestionsId, x.SurveysId });
                    table.ForeignKey(
                        name: "FK_QuestionSurvey_Questions_QuestionsId",
                        column: x => x.QuestionsId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionSurvey_Surveys_SurveysId",
                        column: x => x.SurveysId,
                        principalTable: "Surveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SurveyQuestions_QuestionId",
                table: "SurveyQuestions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_CorrectAnswers_SurveyId_QuestionId",
                table: "CorrectAnswers",
                columns: new[] { "SurveyId", "QuestionId" });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionSurvey_SurveysId",
                table: "QuestionSurvey",
                column: "SurveysId");

            migrationBuilder.AddForeignKey(
                name: "FK_CorrectAnswers_SurveyQuestions_SurveyId_QuestionId",
                table: "CorrectAnswers",
                columns: new[] { "SurveyId", "QuestionId" },
                principalTable: "SurveyQuestions",
                principalColumns: new[] { "SurveyId", "QuestionId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyQuestions_Questions_QuestionId",
                table: "SurveyQuestions",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyQuestions_Surveys_SurveyId",
                table: "SurveyQuestions",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
