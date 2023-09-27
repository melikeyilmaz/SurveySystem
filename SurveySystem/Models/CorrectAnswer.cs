using SurveySystem.Migrations;
using System.ComponentModel.DataAnnotations;

namespace SurveySystem.Models
{
    public class CorrectAnswer
    {       
        public int Id { get; set; }
        public int SurveyId { get; set; } // Hangi anketin doğru cevapları olduğunu göstermek için anket ID'si
        public Survey Survey { get; set; } // İlişkiyi kurmak için kullanılacak Survey modeli
        public int QuestionId { get; set; } // Hangi sorunun doğru cevabı olduğunu göstermek için soru ID'si
        public Question Question { get; set; } // İlişkiyi kurmak için kullanılacak Question modeli
        public int CorrectChoiceId { get; set; } // Sorunun doğru cevabının seçenek ID'si
    }
}
