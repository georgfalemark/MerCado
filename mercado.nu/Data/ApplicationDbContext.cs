﻿using mercado.nu.Models;
using mercado.nu.Models.Entities;
using Microsoft.AspNetCore.Identity;
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
        public DbSet<Chapters> Chapters { get; set; }
        public DbSet<MarketResearch> MarketResearches { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionOption> QuestionOptions { get; set; }
        public DbSet<QuestionTypes> QuestionTypes { get; set; }
        public DbSet<QuestionToMarketResearch> GetQuestionToMarketResearches { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet <Content> Contents { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {


        }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<QuestionToMarketResearch>().HasKey(x => new { x.MarketResearchId, x.QuestionId });
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Responders>().HasKey(x => new { x.MarketResearchId, x.PersonId });
            modelBuilder.Entity<Answer>().HasKey(x => new { x.MarketResearchId, x.PersonId, x.QuestionId });
            base.OnModelCreating(modelBuilder);
            //base.OnModelCreating(modelBuilder);


            //Testade med detta
            modelBuilder.Entity<Chapters>().HasKey(m => m.ChaptersId);

            modelBuilder.Entity<Chapters>().HasOne(m => m.MarketResearch)
                .WithMany(m => m.Chapters)
                .HasForeignKey(m => m.ChaptersId)
                .OnDelete(DeleteBehavior.Restrict);




            //modelBuilder.Entity<Organization>().HasOne(p => p.Contactperson).WithMany(b => b.ContactPersonOrganizations).HasForeignKey(p => p.ContactPersonId).HasConstraintName("ForeignKey_ContacPersonOrganisation_ContactPerson");
        }

    }
}
