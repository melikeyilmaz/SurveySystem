using System.ComponentModel.DataAnnotations;

namespace SurveySystem.Models
{
    public class UserCreateModel
    {
        [Required(ErrorMessage = "Kullanıcı adı-soyadı girilmesi zorunludur.")]
        public string UserName { get; set; }        

        [EmailAddress(ErrorMessage = "Lütfen geçerli bir email formatı giriniz.")]
        [Required(ErrorMessage = "Email girilmesi zorunludur.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre girilmesi zorunludur.")]
        public string Password { get; set; }

        [Compare("Password",ErrorMessage ="Şifreler eşleşmiyor.")]
        public string ConfirmPassword { get; set; }
    }
}
