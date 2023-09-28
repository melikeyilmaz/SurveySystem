using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveySystem.Migrations
{
    /// <inheritdoc />
    public partial class QuestionResponse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionResponse_Surveys_SurveyId",
                table: "QuestionResponse");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionSurvey_Surveys_SurveysId",
                table: "QuestionSurvey");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Surveys",
                table: "Surveys");

            migrationBuilder.RenameTable(
                name: "Surveys",
                newName: "Survey");

            migrationBuilder.AlterColumn<int>(
                name: "SurveyId",
                table: "QuestionResponse",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UniqueId",
                table: "Survey",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Survey",
                table: "Survey",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionResponse_QuestionId",
                table: "QuestionResponse",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionResponse_Questions_QuestionId",
                table: "QuestionResponse",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionResponse_Survey_SurveyId",
                table: "QuestionResponse",
                column: "SurveyId",
                principalTable: "Survey",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionSurvey_Survey_SurveysId",
                table: "QuestionSurvey",
                column: "SurveysId",
                principalTable: "Survey",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionResponse_Questions_QuestionId",
                table: "QuestionResponse");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionResponse_Survey_SurveyId",
                table: "QuestionResponse");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionSurvey_Survey_SurveysId",
                table: "QuestionSurvey");

            migrationBuilder.DropIndex(
                name: "IX_QuestionResponse_QuestionId",
                table: "QuestionResponse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Survey",
                table: "Survey");

            migrationBuilder.RenameTable(
                name: "Survey",
                newName: "Surveys");

            migrationBuilder.AlterColumn<int>(
                name: "SurveyId",
                table: "QuestionResponse",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UniqueId",
                table: "Surveys",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Surveys",
                table: "Surveys",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionResponse_Surveys_SurveyId",
                table: "QuestionResponse",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionSurvey_Surveys_SurveysId",
                table: "QuestionSurvey",
                column: "SurveysId",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
