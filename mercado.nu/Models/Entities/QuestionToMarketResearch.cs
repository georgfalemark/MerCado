using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mercado.nu.Models.Entities
{
    public class QuestionToMarketResearch
    {
        public Guid QuestionId { get; set; }
        public Question Question { get; set; }

        public Guid MarketResearchId { get; set; }
        public MarketResearch MarketResearch { get; set; }
    }
}
