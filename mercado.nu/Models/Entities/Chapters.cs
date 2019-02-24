using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mercado.nu.Models.Entities
{
    public class Chapters
    {
        [Display(Name = "Avsnitt")]
        public Guid ChaptersId { get; set; }
        [Display(Name = "Avsnitt")]
        public string Name { get; set; }
        public List<Question> ListOfQuestion { get; set; }
        public MarketResearch MarketResearch { get; set; }
    }
}
