using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mercado.nu.Models.Entities
{
    public class Question
    {
        public Guid QuestionId { get; set; }
        public int QuestionNumber { get; set; }
        [Display(Name = "Fråga")]
        public string ActualQuestion { get; set; }
        public Guid ChaptersId { get; set; }
        [Display(Name = "Avsnitt")]
        public Chapters Chapter { get; set; }
        [Display(Name = "Frågetyp")]
        public QuestionTypes QuestionType { get; set; }
        public List<QuestionToMarketResearch> MarketResearches { get; set; }
        public List<Answer> Answers { get; set; }
        public List<QuestionOption> QuestionOptions { get; set; }
    }
}
