using mercado.nu.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace mercado.nu.Models
{
    public class Person
    {

        //private bool age;

        public Guid PersonId { get; set; }

        [Display(Name = "Förnamn")]
        public string FirstName { get; set; }

        [Display(Name = "Efternamn")]
        public string LastName { get; set; }

        [Display(Name = "Födelsedag")]
        public DateTime BirthDay { get; set; }

        //[Display(Name = "Ålder")]
        //public int Age
        //{
        //    get { return age; }
        //    set
        //    {
        //            age = int.Parse(DateTime.Now-Age);
        //    }
        //}

        [Display(Name = "Kön")]
        public Gender Gender { get; set; }

        [Display(Name = "Gatuadress")]
        public string Street { get; set; }

        [Display(Name = "Gatunummer")]
        public string StreetNumber { get; set; }

        [Display(Name = "Stad")]
        public string City { get; set; }
        
        [Display(Name = "Postnummer")]
        public string PostNumber { get; set; }

        [Display(Name = "Land")]
        public string Country { get; set; }

        public Guid? OrganizationId { get; set; }
        public List<Organization> ContactPersonOrganizations { get; set; }

        public List<Responders> Responders { get; set; }

        public ApplicationUser User { get; set; }

        public List<Answer> Answer { get; set; }

        public List<MarketResearch> MarketResearches { get; set; }

    }
}
