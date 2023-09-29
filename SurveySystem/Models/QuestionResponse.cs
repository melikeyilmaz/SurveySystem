namespace SurveySystem.Models
{
    public class QuestionResponse
    {
        public int Id { get; set; }
        public int SurveyId { get; set; } // Hangi ankete ait olduğunu belirten özellik
        public int QuestionId { get; set; }
        public int SelectedOption { get; set; } //Seçilen doğru cevap bilgisi

        // 
        public Question? Question { get; set; } // Soru ile ilişkiyi tanımlayın
        public Survey? Survey { get; set; }
    }
}
