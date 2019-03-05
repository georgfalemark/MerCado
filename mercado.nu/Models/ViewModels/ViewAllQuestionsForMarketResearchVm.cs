using mercado.nu.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mercado.nu.Models.ViewModels
{
    public class ViewAllQuestionsForMarketResearchVm
    {
        public List<Question> Questions { get; set; }
        public Guid CurrentMarketResearchId { get; set; }
    }
}
