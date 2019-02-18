using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mercado.nu.Models.Entities
{
    public class Answer
    {
        public Guid QuestionId { get; set; }
        public Question Question { get; set; }

        public Guid MarketResearchId { get; set; }
        public MarketResearch MarketResearch { get; set; }

        public Guid PersonId { get; set; }
        public Person Person { get; set; }

        public string Value { get; set; }
    }
}
