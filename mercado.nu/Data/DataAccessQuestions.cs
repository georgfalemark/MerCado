using mercado.nu.Models;
using mercado.nu.Models.Entities;
using mercado.nu.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
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

            var marketResearch = _questionContext.MarketResearches.Single(x => x.MarketResearchId == marketResearchId);
            chapters.MarketResearch = marketResearch;
            chapters.ChaptersId = Guid.NewGuid();
            _questionContext.Add(chapters);
            await _questionContext.SaveChangesAsync();

        }

        internal List<Chapters> GetChapters(Guid marketResearchId)
        {
            
            var chapters = _questionContext.Chapters.Where(x => x.MarketResearch.MarketResearchId == marketResearchId).ToList();
            return chapters;
        }

        internal List<QuestionToMarketResearch> GetQuestionsForMarketResearch(Guid marketResearchId)
        {
            var allQuestionsForMarketResearch = _questionContext.GetQuestionToMarketResearches.Where(x => x.MarketResearchId == marketResearchId).Include(x => x.Question).ThenInclude(y => y.QuestionOptions).ToList();
            return allQuestionsForMarketResearch;
        }

        internal async Task saveQuestion(AddQuestionToMarketResearchVm questionToMarketResearchVm)
        {

             var marketResearch = _questionContext.MarketResearches.Single(x => x.MarketResearchId == questionToMarketResearchVm.CurrentMarketResearchId);

            var question = questionToMarketResearchVm.Question;


            QuestionToMarketResearch questionToMarketResearch = new QuestionToMarketResearch
            {
                Question = question,
                MarketResearch = marketResearch
            };

            _questionContext.Add(questionToMarketResearch);
            await _questionContext.SaveChangesAsync();
           //return questionToMarketResearchVm.Question.QuestionId;
        }

        internal async Task AddQuestionOption(QuestionOption questionOption, AddQuestionToMarketResearchVm questionToMarketResearchVm)
        {

            //var qustionOptionList = new List<QuestionOption>(); Kan nog tas bort va?
            questionOption.QuestionId = questionToMarketResearchVm.Question.QuestionId;
            _questionContext.Add(questionOption);
            await _questionContext.SaveChangesAsync();
        }

     
        internal async Task AddQuestionOptionForFlerval(QuestionOption questionOption, AddQuestionToMarketResearchVm questionToMarketResearchVm)
        {
            questionOption.QuestionId = questionToMarketResearchVm.Question.QuestionId;
            _questionContext.Add(questionOption);
            await _questionContext.SaveChangesAsync();
        }

        internal List<Responders> GetMarketResearchesForPerson(Guid userId)
        {
            var marketResearchesForPerson = _questionContext.Responders.Where(x => x.PersonId == userId).Include(x => x.MarketResearchs).ToList();
            return marketResearchesForPerson;
        }

        internal async Task<int> AddAnswers(List<Answer> listOfAnswers)
        {
            foreach (var answer in listOfAnswers)
            {
                await _questionContext.AddAsync(answer);
                var result = await _questionContext.SaveChangesAsync();
            }
                return 1;
        }
    }
}
