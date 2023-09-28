namespace SurveySystem.Models
{
    public class SurveyScore
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int SurveyId { get; set; }
        public int QuestionId { get; set; } // Hangi soru için cevaplandı
        public int SelectedOption { get; set; } // Kullanıcının seçtiği cevap
        public int Score { get; set; }

        public virtual Survey Survey { get; set; } // Anket ile ilişki
        public virtual Question Question { get; set; } // Soru ile ilişki
    }
}
