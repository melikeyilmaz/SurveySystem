using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveySystem.Migrations
{
    /// <inheritdoc />
    public partial class srvyquestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorrectOption",
                table: "Questions");

            migrationBuilder.CreateTable(
                name: "CorrectAnswer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnswerText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SurveyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorrectAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CorrectAnswer_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CorrectAnswer_SurveyId",
                table: "CorrectAnswer",
                column: "SurveyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CorrectAnswer");

            migrationBuilder.AddColumn<int>(
                name: "CorrectOption",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
