using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mercado.nu.Models.Entities
{
    public class Chapters
    {
        public Guid ChaptersId { get; set; }
        public string Name { get; set; }
        public List<Question> ListOfQuestion { get; set; }
        public MarketResearch MarketResearch { get; set; }
    }
}
