using SurveySystem.Entities;
using System.ComponentModel.DataAnnotations;

namespace SurveySystem.Models
{
    public class Surveys
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "İsminizi giriniz.")]
        public string UserName { get; set; }

        // Anketin içerdiği soruların listesi
        public List<Question>? Questions { get; set; }

        // Anketi oluşturan kullanıcının kimliği
        public int UserId { get; set; }

        // En fazla kaç kişiye gönderilebileceği
        public int MaxParticipants { get; set; }

        // İlişkiyi kurmak için kullanılacak User modeli
        public AppUser User { get; set; }
    }
}
