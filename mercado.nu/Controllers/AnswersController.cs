﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mercado.nu.Data;
using mercado.nu.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using mercado.nu.Models;
using mercado.nu.Models.Evaluations;
using mercado.nu.Models.ViewModels;

namespace mercado.nu.Controllers
{
    public class AnswersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly DataAccessQuestions _dataAccessQuestions;
        private readonly UserManager<ApplicationUser> _userManager;

        public AnswersController(ApplicationDbContext context, DataAccessQuestions dataAccessQuestions, UserManager<ApplicationUser> userManager )
        {
            _context = context;
            _dataAccessQuestions = dataAccessQuestions;
            _userManager = userManager;
        }

        // GET: Answers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Answers.Include(a => a.MarketResearch).Include(a => a.Person).Include(a => a.Question);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Answers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _context.Answers
                .Include(a => a.MarketResearch)
                .Include(a => a.Person)
                .Include(a => a.Question)
                .FirstOrDefaultAsync(m => m.AnswerId == id);
            if (answer == null)
            {
                return NotFound();
            }

            return View(answer);
        }

        // GET: Answers/Create
        public IActionResult Create()
        {
            ViewData["MarketResearchId"] = new SelectList(_context.MarketResearches, "MarketResearchId", "MarketResearchId");
            ViewData["PersonId"] = new SelectList(_context.Persons, "PersonId", "PersonId");
            ViewData["QuestionId"] = new SelectList(_context.Questions, "QuestionId", "QuestionId");
            return View();
        }

        // POST: Answers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnswerId,PersonId,QuestionId,MarketResearchId,Value")] Answer answer)
        {
            if (ModelState.IsValid)
            {
                answer.AnswerId = Guid.NewGuid();
                _context.Add(answer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MarketResearchId"] = new SelectList(_context.MarketResearches, "MarketResearchId", "MarketResearchId", answer.MarketResearchId);
            ViewData["PersonId"] = new SelectList(_context.Persons, "PersonId", "PersonId", answer.PersonId);
            ViewData["QuestionId"] = new SelectList(_context.Questions, "QuestionId", "QuestionId", answer.QuestionId);
            return View(answer);
        }

        // GET: Answers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _context.Answers.FindAsync(id);
            if (answer == null)
            {
                return NotFound();
            }
            ViewData["MarketResearchId"] = new SelectList(_context.MarketResearches, "MarketResearchId", "MarketResearchId", answer.MarketResearchId);
            ViewData["PersonId"] = new SelectList(_context.Persons, "PersonId", "PersonId", answer.PersonId);
            ViewData["QuestionId"] = new SelectList(_context.Questions, "QuestionId", "QuestionId", answer.QuestionId);
            return View(answer);
        }

        // POST: Answers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AnswerId,PersonId,QuestionId,MarketResearchId,Value")] Answer answer)
        {
            if (id != answer.AnswerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(answer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnswerExists(answer.AnswerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MarketResearchId"] = new SelectList(_context.MarketResearches, "MarketResearchId", "MarketResearchId", answer.MarketResearchId);
            ViewData["PersonId"] = new SelectList(_context.Persons, "PersonId", "PersonId", answer.PersonId);
            ViewData["QuestionId"] = new SelectList(_context.Questions, "QuestionId", "QuestionId", answer.QuestionId);
            return View(answer);
        }

        // GET: Answers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _context.Answers
                .Include(a => a.MarketResearch)
                .Include(a => a.Person)
                .Include(a => a.Question)
                .FirstOrDefaultAsync(m => m.AnswerId == id);
            if (answer == null)
            {
                return NotFound();
            }

            return View(answer);
        }

        // POST: Answers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var answer = await _context.Answers.FindAsync(id);
            _context.Answers.Remove(answer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnswerExists(Guid id)
        {
            return _context.Answers.Any(e => e.AnswerId == id);
        }

        public async Task<IActionResult> ResponseAsync(IFormCollection answer)
        {

            ApplicationUser applicationUser = await _userManager.GetUserAsync(HttpContext.User);
            var userId = applicationUser.Id;

            var listOfAnswers = new List<Answer>();

            for (int i = 0; i < (answer.Count - 2)/2; i++)
            {
                var answerGuid = Guid.NewGuid();
                string questionId = $"{i} questionId";
                string value = $"{i}";
                listOfAnswers.Add(new Answer { AnswerId = answerGuid, MarketResearchId = Guid.Parse(answer["marketId"]), PersonId = userId, QuestionId = Guid.Parse(answer[questionId]), Value = answer[value],  });
            }

            int result = await _dataAccessQuestions.AddAnswers(listOfAnswers);


            Responders responders = _context.Responders.SingleOrDefault(x => x.MarketResearchId == listOfAnswers[0].MarketResearchId && x.PersonId == listOfAnswers[0].PersonId);
            responders.MarketResearchCompleted = true;
            _context.Responders.Update(responders);
            await _context.SaveChangesAsync();

            var thankyouVm = new ThankYouResponderVm();
            //thankyouVm.Organization= await _context.Organizations.Include(x=>x.MarketResearches).SingleAsync(x=>x.OrganizationId==)
            thankyouVm.Person = await _context.Persons.SingleAsync(x => x.PersonId == userId);
            //thankyouVm.MarketResearch=await _context.MarketResearches.SingleAsync(x=>x.MarketResearchId)
            return View(thankyouVm);

        }

        public IActionResult Evaluation(Guid marketResearchId)
        {
            List<Answer> answers = _dataAccessQuestions.GetAnswersForMarketResearch(marketResearchId);

            var listMultiQuestionId = answers.Where(x => x.Question.QuestionType == QuestionTypes.Flervalsfråga).Select(x => x.QuestionId).ToList();

            var listValueTypes = new List<List<string>>();

            foreach (var multiquestion in listMultiQuestionId)
            {
                var valueTypes = _context.QuestionOptions.Where(x => x.QuestionId == multiquestion).Select(x => x.Value).ToList();
                valueTypes.Add(multiquestion.ToString());
                listValueTypes.Add(valueTypes);
            }

            var evaluation = new Evaluation();

            var getEvaluation = evaluation.GetEvaluation(answers, listValueTypes);

            var viewModelSummary = new MarketResearchSummeryVm();

            viewModelSummary.SummaryOfMarketResearch = getEvaluation;

            return View(viewModelSummary);
        }

        public IActionResult ChoseQuestions(Guid marketResearchId)
        {
            var listOfQuestions = _dataAccessQuestions.GetQuestionsForMarketResearch(marketResearchId);

            var questionList = new List<SelectListItem>();

            foreach (var item in listOfQuestions)
            {
                questionList.Add(new SelectListItem
                {
                    Text = $"Fråga {item.Question.QuestionNumber.ToString()}. {item.Question.QuestionType}. {item.Question.ActualQuestion}",
                    Value = item.QuestionId.ToString()
                });
            }

            var viewModelChoseQuestion = new ChoseQuestionsVm();
            viewModelChoseQuestion.QuestionList = questionList;

            return View(viewModelChoseQuestion);
        }

        public IActionResult ShowQuestionComparison(ChoseQuestionsVm choseQuestions)
        {
            var questionOneData = _dataAccessQuestions.GetDataForQuestion(choseQuestions.QuestionOne);
            var questionTwoData = _dataAccessQuestions.GetDataForQuestion(choseQuestions.QuestionTwo);
            
            var questionHeadingsOne = _dataAccessQuestions.GetHeadings(choseQuestions.QuestionOne);
            var questionHeadingsTwo = _dataAccessQuestions.GetHeadings(choseQuestions.QuestionTwo);

            ShowQuestionComparisonVm viewModelShowComparison = new ShowQuestionComparisonVm();
            bool[] binaryOptions = { true, false };

            List<List<int>> dataForTable = new List<List<int>>();

            if (questionOneData[0] == QuestionTypes.Binärfråga)
            {
                
                foreach (bool optionOne in binaryOptions)
                {
                        string[] headingsOne = questionHeadingsOne[0].Split('-');

                    if (questionTwoData[0] == QuestionTypes.Binärfråga)
                    {
                        
                        string[] headingsTwo = questionHeadingsTwo[0].Split('-');

                        List<int> arrayList = new List<int>();
                        foreach (bool optionTwo in binaryOptions)
                        {
                            int numberOfAnswers = _dataAccessQuestions.CalculateAnswers(choseQuestions, optionOne.ToString(), optionTwo.ToString());
                            arrayList.Add(numberOfAnswers);
                        }
                        viewModelShowComparison.HeadingsVertical = headingsOne;
                        viewModelShowComparison.HeadingsHorizontal = headingsTwo;
                        dataForTable.Add(arrayList);
                    }
                    else
                    {
                        List<int> arrayList = new List<int>();
                        foreach (string headingTwo in questionHeadingsTwo)
                        {
                            int numberOfAnswers = _dataAccessQuestions.CalculateAnswers(choseQuestions, optionOne.ToString(), headingTwo);
                            arrayList.Add(numberOfAnswers);
                        }
                        viewModelShowComparison.HeadingsVertical = headingsOne;
                        viewModelShowComparison.HeadingsHorizontal = questionHeadingsTwo.ToArray();
                        dataForTable.Add(arrayList);
                    }

                }
            }
            else
            {
                foreach (string headingOne in questionHeadingsOne)
                {

                    if (questionTwoData[0] == QuestionTypes.Binärfråga)
                    {
                        string[] headingsTwo = questionHeadingsTwo[0].Split('-');

                        List<int> arrayList = new List<int>();
                        foreach (bool optionTwo in binaryOptions)
                        {
                            int numberOfAnswers = _dataAccessQuestions.CalculateAnswers(choseQuestions, headingOne, optionTwo.ToString());
                            arrayList.Add(numberOfAnswers);
                        }
                        viewModelShowComparison.HeadingsVertical = questionHeadingsOne.ToArray();
                        viewModelShowComparison.HeadingsHorizontal = headingsTwo;
                        dataForTable.Add(arrayList);
                    }
                    else
                    {
                        List<int> arrayList = new List<int>();

                        foreach (string headingTwo in questionHeadingsTwo)
                        {
                            int numberOfAnswers = _dataAccessQuestions.CalculateAnswers(choseQuestions, headingOne, headingTwo);
                            arrayList.Add(numberOfAnswers);
                        }
                        viewModelShowComparison.HeadingsVertical = questionHeadingsOne.ToArray();
                        viewModelShowComparison.HeadingsHorizontal = questionHeadingsTwo.ToArray();
                        dataForTable.Add(arrayList);
                    }
                }
            }

            viewModelShowComparison.DataArrayForComparison = dataForTable;

            return View(viewModelShowComparison);
        }
    }
}
