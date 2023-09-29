namespace SurveySystem.Models
{
    public class SurveyScore
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Score { get; set; }

        // Foreign Key: Her bir SurveyScore kaydının hangi anketle ilişkilendirildiğini belirtmek için
        public int SurveyId { get; set; }

        // SurveyScore sınıfı ile Survey arasında bir ilişki
        public Survey Survey { get; set; }

        // Birden fazla soru ve seçilen cevapları bu koleksiyon ile tutabilirsiniz.
        public List<SurveyResponse> SurveyResponses { get; set; }

    }
}
