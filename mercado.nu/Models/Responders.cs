using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mercado.nu.Models
{
    public class Responders
    {
        public Guid PersonId { get; set; }
        public Person Persons { get; set; }

        public Guid MarketResearchId { get; set; }
        public MarketResearch MarketResearchs { get; set; }
    }
}
