using mercado.nu.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mercado.nu.Models
{
    public class MarketResearch
    {
        private bool onGoing = false;

        public Guid MarketResearchId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Purpose { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public Gender Gender { get; set; }
        public string Area { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool OnGoing { get { return onGoing; } set
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
