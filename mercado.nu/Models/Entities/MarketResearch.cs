using mercado.nu.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mercado.nu.Models
{
    public class MarketResearch
    {
        private bool onGoing = false;

        public Guid MarketResearchId { get; set; }
        [Required(ErrorMessage = "Du måste skriva in ett förnamn")]
        [Display(Name = "Undersökning")]
        public string Name { get; set; }

        [Display(Name = "Beskrivning")]
        [Required(ErrorMessage = "Du måste skriva in ett efternamn")]
        public string Description { get; set; }

        [Display(Name = "Undersökningens syfte")]
        [Required(ErrorMessage = "Du måste skriva in syfte")]
        public string Purpose { get; set; }

        [Display(Name = "Lägsta ålder")]
        [Required(ErrorMessage = "Du måste skriva in ålder")]
        public int MinAge { get; set; }

        [Display(Name = "Högsta ålder")]
        [Required(ErrorMessage = "Du måste skriva in ålder")]

        public int MaxAge { get; set; }

        [Display(Name = "Kön")]
        [Required(ErrorMessage = "Du måste skriva in kön")]

        public Gender Gender { get; set; }

        [Display(Name = "Område")]
        [Required(ErrorMessage = "Du måste skriva in ett vart respondenterna bor")]

        public string Area { get; set; }

        public DateTime CreationDate
        { get
            {
                DateTime creationDate = DateTime.Now;
                return creationDate.Date;
            }
        }
        [Display(Name = "Planerat startdatum")]
        [Required(ErrorMessage = "Skriv in när undersökningen skall starta")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Planerat slutdatum")]
        [Required(ErrorMessage = "Skriv in när undersökningen skall sluta")]
        public DateTime EndDate { get; set; }
        public bool OnGoing
        {
            get {

                if ((StartDate < DateTime.Now) && (EndDate > DateTime.Now))
                {
                    onGoing = true;
                }
                else
                {
                    onGoing = false;
                }
                return onGoing;
            }
           
        }

        public List<Responders> Responders { get; set; }

        [Display(Name = "Hur många ska den skickas ut till?")]
        [Required(ErrorMessage = "Skriv in hur många som undersökningen skall skickas ut till")]
        public int NumberOfResponders { get; set; }

        public List<Chapters> Chapters { get; set; }

        public List<QuestionToMarketResearch> Questions { get; set; }

        public List<Answer> Answers { get; set; }

        public Organization Organization { get; set; }

    }
}
