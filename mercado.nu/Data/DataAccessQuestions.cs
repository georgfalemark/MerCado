using mercado.nu.Models;
using mercado.nu.Models.Entities;
using mercado.nu.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
            
            var chapters = _questionContext.Chapters.Where(x => x.MarketResearch.MarketResearchId == marketResearchId).Include(x => x.MarketResearch).ToList();
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
       
        }

        internal async Task SetMarketResearchToPersonAndOrganizationAndSave(MarketResearch marketResearch, Guid userId)
        {
            var person =await _questionContext.Persons.Include(x=>x.MarketResearches).FirstAsync(x => x.PersonId == userId);
            var organization =await _questionContext.Organizations.Include(x=>x.MarketResearches).FirstAsync(x => x.OrganizationId == person.OrganizationId);
            if (marketResearch.StartDate>=DateTime.Now&& marketResearch.EndDate <= DateTime.Now)
            {
                marketResearch.OnGoing = true;
            }
            person.MarketResearches.Add(marketResearch);
            organization.MarketResearches.Add(marketResearch);

            _questionContext.Update(person);
            _questionContext.Update(organization);

            await _questionContext.SaveChangesAsync();
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
                resp.MarketResearchCompleted = false;
                _questionContext.Add(resp);
              await _questionContext.SaveChangesAsync();
            }
        }

        //internal Task GetRespondersToMarketResearchFromRegistredUser(Guid id)
        //{
        //    var respondent = _questionContext.Persons.FirstAsync(x => x.PersonId == id);

        //    var responders=_questionContext.MarketResearches.WhereAsync(x =>
        //    (_questionContext.MarketResearches.Gender == null || marketResearch.Gender == x.Gender) &&
        //    //( marketResearch.MinAge == null || marketResearch.MinAge < x.Age) &&
        //    //( marketResearch.MaxAge == null || marketResearch.MaxAge > x.Age) &&
        //    (marketResearch.OnGoing == true) &&
        //    (marketResearch.OnGoing == true) &&
        //    (marketResearch.Area == null || marketResearch.Area == x.City)).ToList();
        //}

        internal async Task AddQuestionOption(AddQuestionToMarketResearchVm questionToMarketResearchVm, int questionType)
        {
            var question = _questionContext.Questions.Single(x => x.QuestionId == questionToMarketResearchVm.Question.QuestionId); //Blir det rätt
            question.QuestionType = (QuestionTypes)questionType;
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
            var marketResearchesForPerson = _questionContext.Responders.Where(x => x.PersonId == userId).Include(x => x.MarketResearchs).Include(x => x.Persons).ToList();
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

        internal List<QuestionTypes> GetDataForQuestion(Question question)
        {
            var data = _questionContext.Questions.Where(x => x.QuestionId == question.QuestionId).Select(x => x.QuestionType).ToList();
            return data;
        }

        internal List<string> GetHeadings(Question questionOne)
        {
            var headings = _questionContext.QuestionOptions.Where(x => x.QuestionId == questionOne.QuestionId).Select(x => x.Value).ToList();
            return headings;
        }

        internal int CalculateAnswers(ChoseQuestionsVm choseQuestions, string optionOne, string optionTwo)
        {
            var numberOfTwo = _questionContext.Answers.Where(x => x.Value == optionTwo && x.QuestionId == choseQuestions.QuestionTwo.QuestionId).Select(x => x.PersonId).ToList();
            var numberofOne = _questionContext.Answers.Where(x => x.Value == optionOne && x.QuestionId == choseQuestions.QuestionOne.QuestionId).Select(x => x.PersonId).ToList();
            var compareLists = numberOfTwo.Intersect(numberofOne).Count();

            return compareLists;
        }
    }
}
