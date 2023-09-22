using Microsoft.AspNetCore.Identity;

namespace SurveySystem.Entities
{
    public class AppRole:IdentityRole<int>
    {
        public DateTime CreatedTime { get; set; }
    }
}
