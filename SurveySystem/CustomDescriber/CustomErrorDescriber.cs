using Microsoft.AspNetCore.Identity;

namespace SurveySystem.CustomDescriber
{
    public class CustomErrorDescriber:IdentityErrorDescriber
    {
        public override IdentityError PasswordTooShort(int length)
        {
            return new()
            {
                Code = "PasswordTooShort",
                Description = $"Şifre en az {length} karakterden oluşmalıdır."
            };
        }

        //public override IdentityError DuplicateUserName(string userName)
        //{
        //    return new()
        //    {
        //        Code = "DuplicateUserName",
        //        Description = $"Bu {userName} sistemde kayıtlı."
        //    };
        //}

        public override IdentityError DuplicateEmail(string email)
        {
            return new()
            {
                Code = "DuplicateEmail",
                Description = $"{email} sistemde kayıtlıdır. Sisteme kayıtlı olmayan bir e-posta adresi giriniz."
            };
        }
    }
}
