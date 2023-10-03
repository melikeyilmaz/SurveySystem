using System.Reflection;
using System.Runtime.ConstrainedExecution;

namespace SurveySystem.Models
{
    public class SurveyScore
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Score { get; set; }
        public int CorrectAnswers { get; set; }
        public int IncorrectAnswers { get; set; }

        // Her bir SurveyScore kaydının bir veya birden fazla SurveyResponse kaydı olabilir
        public List<SurveyResponse>? SurveyResponses { get; set; }


    }
}
