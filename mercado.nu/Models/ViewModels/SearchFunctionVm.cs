using mercado.nu.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mercado.nu.Models.ViewModels
{
    public class SearchFunctionVm
    {
        public List<MarketResearch> MarketResearches { get; set; }
        public List<Organization> Organizations { get; set; }
        public List<Question> Questions { get; set; }
    }
}
