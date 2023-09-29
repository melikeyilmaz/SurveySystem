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
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Survey> QuestionResponse { get; set; }
        public DbSet<SurveyScore> SurveyScores { get; set; }
        public DbSet<SurveyResponse> SurveyResponses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Question>()
                .HasOne(q => q.User)
                .WithMany(u => u.Questions)
                .HasForeignKey(q => q.UserId);

            modelBuilder.Entity<Survey>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<QuestionResponse>()
                .HasOne(qr => qr.Survey)
                .WithMany(s => s.QuestionResponses)
                .HasForeignKey(qr => qr.SurveyId);

            modelBuilder.Entity<QuestionResponse>()
                .HasOne(qr => qr.Question)
                .WithMany()
                .HasForeignKey(qr => qr.QuestionId);


        


        }
    }
}
