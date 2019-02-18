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
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Answer> Answer { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<MarketResearch> MarketResearch { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<Persons> Persons { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Questions> Questions { get; set; }
        public DbSet<QuestionType> QuestionType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Questions>().HasKey(x => new { x.MarketResearchId, x.QuestionId });
            modelBuilder.Entity<Persons>().HasKey(x => new { x.PersonId, x.MarketResearchId });
            modelBuilder.Entity<Answer>().HasKey(x => new { x.MarketResearchId, x.PersonId, x.QuestionId });
            base.OnModelCreating(modelBuilder);
        }

    }
}
