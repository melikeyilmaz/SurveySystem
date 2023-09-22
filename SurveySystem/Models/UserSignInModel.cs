using System.ComponentModel.DataAnnotations;

namespace SurveySystem.Models
{
    public class UserSignInModel
    {
        [Required(ErrorMessage="Kullanıcı adı girilmesi gereklidir.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Şifre girilmesi gereklidir.")]
        public string Password { get; set; }

        //public string ReturnUrl { get; set; }
    }
}
