using System.ComponentModel.DataAnnotations;

namespace SurveySystem.Models
{
    public class UserCreateModel
    {        
        public string? UserName { get; set; }        

        [EmailAddress(ErrorMessage = "Lütfen geçerli bir email formatı giriniz.")]
        [Required(ErrorMessage = "Email girilmesi zorunludur.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre girilmesi zorunludur.")]
        public string Password { get; set; }

        [Compare("Password",ErrorMessage ="Şifreler eşleşmiyor.")]
        [Required(ErrorMessage = "Şifre tekrar girilmesi zorunludur.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Kullanıcı adı girilmesi zorunludur.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Kullanıcı soyadı girilmesi zorunludur.")]
        public string LastName { get; set; }
    }
}
