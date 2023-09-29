namespace SurveySystem.Models
{
    public class SurveyResponse
    {
        public int Id { get; set; }
        public int SelectedOption { get; set; }

        // İlgili anket bilgisi
        public Survey? Survey { get; set; }
        public int SurveyId { get; set; }

        // İlgili soru bilgisi
        public Question? Question { get; set; }
        public int QuestionId { get; set; }

        // SurveyResponse'un hangi SurveyScore kaydına ait olduğunu belirtmek için
        public int SurveyScoreId { get; set; }
        public SurveyScore? SurveyScore { get; set; }

        

    }
}
