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

        // Foreign Key: Her bir SurveyScore kaydının hangi anketle ilişkilendirildiğini belirtmek için
        // Hangi anketle ilişkilendirildiğini belirtmek için
        //public int RelatedSurveyId { get; set; } 

        // SurveyScore ile Survey arasındaki ilişki
        //public int SurveyId { get; set; }
        //public Survey? Survey { get; set; }

        // Her bir SurveyScore kaydının bir veya birden fazla SurveyResponse kaydı olabilir
        public List<SurveyResponse>? SurveyResponses { get; set; }


    }
}
