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
        [Display(Name = "Undersökning")]
        public string Name { get; set; }
        [Display(Name = "Beskrivning")]
        public string Description { get; set; }
        [Display(Name = "Undersökningens syfte")]
        public string Purpose { get; set; }
        [Display(Name = "Lägsta ålder")]
        public int MinAge { get; set; }
        [Display(Name = "Högsta ålder")]
        public int MaxAge { get; set; }
        [Display(Name = "Kön")]
        public Gender Gender { get; set; }
        [Display(Name = "Område")]
        public string Area { get; set; }
        [Display(Name = "Undersökning skapad")]
        public DateTime CreationDate { get; set; }
        [Display(Name = "Planerat startdatum")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Planerat slutdatum")]
        public DateTime EndDate { get; set; }
        public bool OnGoing
        {
            get { return onGoing; }
            set
            {
                if ((StartDate > DateTime.Now) && (EndDate < DateTime.Now))
                {
                    onGoing = true;
                }
            }
        }

        public List<Responders> Responders { get; set; }

        public List<Chapters> Chapters { get; set; }

        public List<QuestionToMarketResearch> Questions { get; set; }

        public List<Answer> Answers { get; set; }

    }
}
