using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mercado.nu.Models.Entities
{
    public class QuestionToMarketResearch
    {
        public string QuestionId { get; set; }
        public Question Question { get; set; }

        public string MarketResearchId { get; set; }
        public MarketResearch MarketResearch { get; set; }
    }
}
