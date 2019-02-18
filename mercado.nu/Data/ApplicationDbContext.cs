using mercado.nu.Models;
using mercado.nu.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mercado.nu.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Responders> Responders { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Chapters> MyProperty { get; set; }
        public DbSet<MarketResearch> MarketResearches { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionOption> QuestionOptions { get; set; }
        public DbSet<QuestionTypes> QuestionTypes { get; set; }
        public DbSet<QuestionToMarketResearch> GetQuestionToMarketResearches { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {


        }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QuestionToMarketResearch>().HasKey(x => new { x.MarketResearchId, x.QuestionId });
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Responders>().HasKey(x => new { x.MarketResearchId, x.PersonId });
            base.OnModelCreating(modelBuilder);
        }

    }
}
