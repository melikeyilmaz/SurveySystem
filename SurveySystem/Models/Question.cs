using SurveySystem.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveySystem.Models
{
    public class Question
    { 
        public int Id { get; set; }

        [Required(ErrorMessage = "Soru metni gereklidir.")]
        public string QuestionText { get; set; }

        [Required(ErrorMessage = "1. Seçenek gereklidir.")]
        public string Option1 { get; set; }

        [Required(ErrorMessage = "2. Seçenek gereklidir.")]
        public string Option2 { get; set; }

        [Required(ErrorMessage = "3. Seçenek gereklidir.")]
        public string Option3 { get; set; }

        [Required(ErrorMessage = "4. Seçenek gereklidir.")]
        public string Option4 { get; set; }

        [Required(ErrorMessage = "5. Seçenek gereklidir.")]
        public string Option5 { get; set; }

        [Required(ErrorMessage = "Doğru cevap gereklidir.")]
        [RegularExpression("[1-5]", ErrorMessage = "Doğru cevap 1 ile 5 arasında bir değer olmalıdır.")]
        public int CorrectOption { get; set; }
        public bool IsApproved { get; set; }
        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        public AppUser? User { get; set; }

    }
}
