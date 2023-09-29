namespace SurveySystem.Models
{
    public class SurveyResponse
    {
        public int Id { get; set; }

        // Foreign Key: Her bir SurveyResponse kaydının hangi anketle ilişkilendirildiğini belirtmek için
        public int SurveyId { get; set; }

        // Foreign Key: Her bir SurveyResponse kaydının hangi soruyla ilişkilendirildiğini belirtmek için
        public int QuestionId { get; set; }

        public int SelectedOption { get; set; }

        // İlgili anket bilgisi
        public Survey Survey { get; set; }

        // İlgili soru bilgisi
        public Question Question { get; set; }
      
    }
}
