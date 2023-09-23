using System.ComponentModel.DataAnnotations;

namespace SurveySystem.Models
{
    public class Question
    {
        //public int Id { get; set; }
        //public string QuestionText { get; set; }

        //[Required(ErrorMessage = "Doğru cevap seçeneği zorunludur.")]
        //[Range(1, 5, ErrorMessage = "Doğru cevap seçeneği 1 ile 5 arasında bir değer olmalıdır.")]
        //public int CorrectOptionId { get; set; }
        //public List<Option> Options { get; set; } // Şık seçeneklerini bu liste içinde tutuyoruz
        //public Option CorrectOption { get; set; }

        public int Id { get; set; }

        [Required(ErrorMessage = "Soru metni gereklidir.")]
        public string QuestionText { get; set; }

        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public string Option5 { get; set; }

        [Required(ErrorMessage = "Doğru cevap gereklidir.")]
        [RegularExpression("[1-5]", ErrorMessage = "Doğru cevap 1 ile 5 arasında bir değer olmalıdır.")]
        public int CorrectOption { get; set; }

    }
}
