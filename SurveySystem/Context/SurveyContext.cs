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
        public DbSet<QuestionResponse> QuestionResponse { get; set; }
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

            modelBuilder.Entity<SurveyResponse>()
                .HasOne(sr => sr.Survey)
                .WithMany(s => s.SurveyResponses)
                .HasForeignKey(sr => sr.SurveyId);

            modelBuilder.Entity<SurveyResponse>()
                .HasOne(sr => sr.Question)
                .WithMany()
                .HasForeignKey(sr => sr.QuestionId);

    //        modelBuilder.Entity<SurveyResponse>()
    //.HasOne(sr => sr.SurveyScore)
    //.WithMany(ss => ss.SurveyResponses)
    //.HasForeignKey(sr => sr.SurveyScoreId)
    //.OnDelete(DeleteBehavior.NoAction);


            //modelBuilder.Entity<Question>()
            //    .HasOne(q => q.User)
            //    .WithMany(u => u.Questions)
            //    .HasForeignKey(q => q.UserId);

            //modelBuilder.Entity<Survey>()
            //    .HasKey(s => s.Id);

            //modelBuilder.Entity<QuestionResponse>()
            //    .HasOne(qr => qr.Survey)
            //    .WithMany(s => s.QuestionResponses)
            //    .HasForeignKey(qr => qr.SurveyId);

            //modelBuilder.Entity<QuestionResponse>()
            //    .HasOne(qr => qr.Question)
            //    .WithMany()
            //    .HasForeignKey(qr => qr.QuestionId);

            ////modelBuilder.Entity<SurveyScore>()
            ////    .HasOne(ss => ss.Survey)
            ////    .WithMany(s => s.SurveyScores)
            ////    .HasForeignKey(ss => ss.RelatedSurveyId);

            //modelBuilder.Entity<SurveyResponse>()
            //    .HasOne(sr => sr.Survey)
            //    .WithMany(s => s.SurveyResponses)
            //    .HasForeignKey(sr => sr.SurveyId);

            //modelBuilder.Entity<SurveyResponse>()
            //    .HasOne(sr => sr.Question)
            //    .WithMany()
            //    .HasForeignKey(sr => sr.QuestionId);

            ////modelBuilder.Entity<SurveyResponse>()
            ////    .HasOne(sr => sr.SurveyScore)
            ////    .WithMany(ss => ss.SurveyResponses)
            ////    .HasForeignKey(sr => sr.SurveyScoreId)
            ////    .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
