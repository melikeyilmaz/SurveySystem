using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveySystem.Migrations
{
    /// <inheritdoc />
    public partial class InitialSurveyResponses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SurveyScoreId",
                table: "SurveyResponses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SurveyResponses_SurveyScoreId",
                table: "SurveyResponses",
                column: "SurveyScoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyResponses_SurveyScores_SurveyScoreId",
                table: "SurveyResponses",
                column: "SurveyScoreId",
                principalTable: "SurveyScores",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SurveyResponses_SurveyScores_SurveyScoreId",
                table: "SurveyResponses");

            migrationBuilder.DropIndex(
                name: "IX_SurveyResponses_SurveyScoreId",
                table: "SurveyResponses");

            migrationBuilder.DropColumn(
                name: "SurveyScoreId",
                table: "SurveyResponses");
        }
    }
}
