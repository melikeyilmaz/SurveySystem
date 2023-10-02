using System.ComponentModel.DataAnnotations;

namespace SurveySystem.Models
{
    public class UserSignInModel
    {
        [Required(ErrorMessage = "Lütfen e-posta adresini boş geçmeyiniz.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi girin.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lütfen şifrenizi boş geçmeyiniz.")]
        public string Password { get; set; }      
    }
}
