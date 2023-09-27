using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveySystem.Migrations
{
    /// <inheritdoc />
    public partial class SurveyQuestions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CorrectAnswer_Surveys_SurveyId",
                table: "CorrectAnswer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CorrectAnswer",
                table: "CorrectAnswer");

            migrationBuilder.DropIndex(
                name: "IX_CorrectAnswer_SurveyId",
                table: "CorrectAnswer");

            migrationBuilder.RenameTable(
                name: "CorrectAnswer",
                newName: "CorrectAnswers");

            migrationBuilder.AddColumn<int>(
                name: "QuestionId",
                table: "CorrectAnswers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CorrectAnswers",
                table: "CorrectAnswers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "SurveyQuestions",
                columns: table => new
                {
                    SurveyId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyQuestions", x => new { x.SurveyId, x.QuestionId });
                    table.ForeignKey(
                        name: "FK_SurveyQuestions_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SurveyQuestions_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CorrectAnswers_SurveyId_QuestionId",
                table: "CorrectAnswers",
                columns: new[] { "SurveyId", "QuestionId" });

            migrationBuilder.CreateIndex(
                name: "IX_SurveyQuestions_QuestionId",
                table: "SurveyQuestions",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_CorrectAnswers_SurveyQuestions_SurveyId_QuestionId",
                table: "CorrectAnswers",
                columns: new[] { "SurveyId", "QuestionId" },
                principalTable: "SurveyQuestions",
                principalColumns: new[] { "SurveyId", "QuestionId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CorrectAnswers_Surveys_SurveyId",
                table: "CorrectAnswers",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CorrectAnswers_SurveyQuestions_SurveyId_QuestionId",
                table: "CorrectAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_CorrectAnswers_Surveys_SurveyId",
                table: "CorrectAnswers");

            migrationBuilder.DropTable(
                name: "SurveyQuestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CorrectAnswers",
                table: "CorrectAnswers");

            migrationBuilder.DropIndex(
                name: "IX_CorrectAnswers_SurveyId_QuestionId",
                table: "CorrectAnswers");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "CorrectAnswers");

            migrationBuilder.RenameTable(
                name: "CorrectAnswers",
                newName: "CorrectAnswer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CorrectAnswer",
                table: "CorrectAnswer",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CorrectAnswer_SurveyId",
                table: "CorrectAnswer",
                column: "SurveyId");

            migrationBuilder.AddForeignKey(
                name: "FK_CorrectAnswer_Surveys_SurveyId",
                table: "CorrectAnswer",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
