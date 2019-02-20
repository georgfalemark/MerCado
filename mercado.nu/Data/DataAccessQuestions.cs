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

            //var marketResearch = _questionContext.MarketResearches.Single(x => x.MarketResearchId == marketResearchId);
            //marketResearch.Chapters.Add(chapters);
            //_questionContext.Update(marketResearch);
            //await _questionContext.SaveChangesAsync();
        }

        internal List<Chapters> GetChapters(Guid marketResearchId)
        {
            var chapters = _questionContext.Chapters.Where(x => x.MarketResearchID == marketResearchId).ToList();
            return chapters;
        }

        internal async Task SeedQuestionTypes()
        {
            var qt1 = new QuestionTypes { QuestionType = "YesOrNo", QuestionTypesId = Guid.NewGuid() };
            var qt2 = new QuestionTypes { QuestionType = "TextShort", QuestionTypesId = Guid.NewGuid() };
            var qt3 = new QuestionTypes { QuestionType = "TextLong", QuestionTypesId = Guid.NewGuid() };
            var qt4 = new QuestionTypes { QuestionType = "Multiple", QuestionTypesId = Guid.NewGuid() };
            var qt5 = new QuestionTypes { QuestionType = "Range", QuestionTypesId = Guid.NewGuid() };

            _questionContext.AddRange(qt1, qt2, qt3, qt4, qt5);
            await _questionContext.SaveChangesAsync();
        }



    }
}
