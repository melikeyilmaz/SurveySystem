using System.ComponentModel.DataAnnotations;

namespace SurveySystem.Models
{
    public class UserSignInModel
    {
         [Required(ErrorMessage = "Lütfen e-posta adresini boş geçmeyiniz.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi girin.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lütfen şifrenizi giriniz.")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }

        //public string ReturnUrl { get; set; }
    }
}
