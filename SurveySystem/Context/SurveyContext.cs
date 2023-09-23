using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Entities;
using SurveySystem.Models;

namespace SurveySystem.Context
{
    public class SurveyContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public SurveyContext(DbContextOptions<SurveyContext> options) : base(options)
        {

        }

        public DbSet<Question> Questions { get; set; }

      
    
    }
}
