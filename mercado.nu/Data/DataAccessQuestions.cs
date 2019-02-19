using mercado.nu.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mercado.nu.Data
{
    public class DataAccessQuestions
    {
        public ApplicationDbContext _questionContext;

        public DataAccessQuestions(ApplicationDbContext questionContext)
        {
            _questionContext = questionContext;
        }

        internal async Task SaveChapter(Guid marketResearchId, Chapters chapters)
        {
            
            chapters.ChaptersId = Guid.NewGuid();
            chapters.MarketResearchID = marketResearchId;
            _questionContext.Add(chapters);
            await _questionContext.SaveChangesAsync();

            var marketResearch = _questionContext.MarketResearches.Single(x => x.MarketResearchId == marketResearchId);
            marketResearch.Chapters.Add(chapters);
            _questionContext.Update(marketResearch);
            await _questionContext.SaveChangesAsync();
        }


    }
}
