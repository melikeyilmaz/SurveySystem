using SurveySystem.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveySystem.Models
{
    public class Survey
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Ad gereklidir.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Soyad gereklidir.")]
        public string LastName { get; set; }

        // Birden fazla soru ve seçilen cevapları bu koleksiyon ile tutabilirsiniz.
        public List<QuestionResponse>? QuestionResponses { get; set; }

        // Anketin içerdiği soruların listesi
        public List<Question>? Questions { get; set; }

        // Benzersiz URL'yi saklamak için alan
        public string? UniqueId { get; set; }

        // Birden fazla soru ve seçilen cevapları bu koleksiyon ile tutabilirsiniz.
        public List<SurveyResponse>? SurveyResponses { get; set; }

        //// Her bir ankete ait sonuç
        //public List<SurveyScore>? SurveyScores { get; set; }

        
        //public int Id { get; set; }

        //[Required(ErrorMessage = "Ad gereklidir.")]
        //public string FirstName { get; set; }

        //[Required(ErrorMessage = "Soyad gereklidir.")]
        //public string LastName { get; set; }

        //// Birden fazla soru ve seçilen cevapları bu koleksiyon ile tutabilirsiniz.
        //public List<QuestionResponse>? QuestionResponses { get; set; }

        //// Anketin içerdiği soruların listesi
        //public List<Question>? Questions { get; set; }

        //// Benzersiz URL'yi saklamak için alan
        //public string? UniqueId { get; set; }

        //// Birden fazla soru ve seçilen cevapları bu koleksiyon ile tutabilirsiniz.
        //public List<SurveyResponse>? SurveyResponses { get; set; }

        //// Her bir anketin bir veya birden fazla SurveyScore kaydı olabilir
        //public List<SurveyScore>? SurveyScores { get; set; }

    }
}
