using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Entities;

namespace SurveySystem.Context
{
    public class SurveyContext:IdentityDbContext<AppUser,AppRole,int>
    {
        public SurveyContext(DbContextOptions<SurveyContext> options):base(options)
        {
                
        }
    }
}
