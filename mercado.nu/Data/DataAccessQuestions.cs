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
            question.QuestionType = questionToMarketResearchVm.QuestionTypes[0];


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
        
            
            questionOption.QuestionId= questionToMarketResearchVm.Question.QuestionId;
            _questionContext.Add(questionOption);
            await _questionContext.SaveChangesAsync();
          
        }

        internal async Task GetRespondersToMarketResearch(MarketResearch marketResearch)
        {

            var responders = _questionContext.Persons.Where(x => 
            ( marketResearch.Gender == null || marketResearch.Gender == x.Gender) &&
            //( marketResearch.MinAge == null || marketResearch.MinAge < x.Age) &&
            //( marketResearch.MaxAge == null || marketResearch.MaxAge > x.Age) &&
            (marketResearch.OnGoing ==true) &&
            (marketResearch.Area == null || marketResearch.Area == x.City)).ToList(); //Ska lägga till ålder

            foreach (var responder in responders)
            {
                var resp = new Responders();
                resp.Persons = responder;
                resp.MarketResearchs = marketResearch;
                _questionContext.Add(resp);
              await _questionContext.SaveChangesAsync();
            }
             



        }

        internal async Task AddQuestionOption(AddQuestionToMarketResearchVm questionToMarketResearchVm, int questionType)
        {
            var question = _questionContext.Questions.Single(x => x.QuestionId == questionToMarketResearchVm.Question.QuestionId); //Blir det rätt
            //question.QuestionType = questionToMarketResearchVm.Question.QuestionType; 
            question.QuestionType = (QuestionTypes)questionType;
            //question.QuestionType = _questionContext.Questions questionType;
            _questionContext.Update(question);
            await _questionContext.SaveChangesAsync();
        }
        internal async Task SetNumberOnQuestion( AddQuestionToMarketResearchVm questionToMarketResearchVm)
        {
            var question = _questionContext.Questions.Single(x => x.QuestionId == questionToMarketResearchVm.Question.QuestionId);
            
            question.QuestionNumber = _questionContext.GetQuestionToMarketResearches.Where(x => x.MarketResearchId == questionToMarketResearchVm.CurrentMarketResearchId).Include(x => x.Question).Max(x => x.Question.QuestionNumber) + 1;
            _questionContext.Update(question);
            await _questionContext.SaveChangesAsync();
        }

        internal async Task SetQuestionTypeOnQuestion(AddQuestionToMarketResearchVm questionToMarketResearchVm)
        {
            var question = _questionContext.Questions.Single(x => x.QuestionId == questionToMarketResearchVm.Question.QuestionId);
            question.QuestionType= questionToMarketResearchVm.QuestionTypes.First();

            _questionContext.Update(question);
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

        internal List<Answer> GetAnswersForMarketResearch(Guid marketResearchId)
        {
            var questions = _questionContext.Answers.Where(x => x.MarketResearchId == marketResearchId).Include(x => x.Question).ThenInclude(x => x.QuestionOptions).ToList();
            return questions;
        }
    }
}
