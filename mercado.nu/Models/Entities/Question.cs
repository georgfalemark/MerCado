using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mercado.nu.Models.Entities
{
    public class Question
    {
        public Guid QuestionId { get; set; }
        public string ActualQuestion { get; set; }
        public Guid ChaptersId { get; set; }
        public Chapters Chapter { get; set; }
        public List<QuestionToMarketResearch> MarketResearches { get; set; }
        public List<Answer> Answers { get; set; }
        public List<QuestionOption> QuestionOptions { get; set; }
    }
}
