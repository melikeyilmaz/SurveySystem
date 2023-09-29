using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveySystem.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SurveyResponses_SurveyScores_SurveyScoreId",
                table: "SurveyResponses");

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyResponses_SurveyScores_SurveyScoreId",
                table: "SurveyResponses",
                column: "SurveyScoreId",
                principalTable: "SurveyScores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SurveyResponses_SurveyScores_SurveyScoreId",
                table: "SurveyResponses");

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyResponses_SurveyScores_SurveyScoreId",
                table: "SurveyResponses",
                column: "SurveyScoreId",
                principalTable: "SurveyScores",
                principalColumn: "Id");
        }
    }
}
