using Microsoft.AspNetCore.Identity;
using SurveySystem.Models;

namespace SurveySystem.Entities
{
    public class AppUser:IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}
