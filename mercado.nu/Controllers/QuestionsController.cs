using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mercado.nu.Data;
using mercado.nu.Models.Entities;
using mercado.nu.Models;
using mercado.nu.Models.ViewModels;

namespace mercado.nu
{
    public class QuestionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly DataAccessQuestions _dataAccessQuestion;

        public QuestionsController(ApplicationDbContext context, DataAccessQuestions dataAccessQuestion)
        {
            _context = context;
            _dataAccessQuestion = dataAccessQuestion;
        }


        public IActionResult ShowQuestionsForMarketResearch(Guid marketResearchId)
        {
            var viewModelAllQuestions = GetQuestions(marketResearchId);

            return View(viewModelAllQuestions);
        }

        // GET: Questions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Questions.Include(q => q.Chapter);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Questions/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions
                .Include(q => q.Chapter)
                .FirstOrDefaultAsync(m => m.QuestionId == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // GET: Questions/Create
        public IActionResult Create(AddQuestionToMarketResearchVm addQuestionToMarketResearchVm)
        {
           
            var listOfChapters = _dataAccessQuestion.GetChapters(addQuestionToMarketResearchVm.CurrentMarketResearchId);

            var selectChapters = GetSelectChapters(listOfChapters);

            addQuestionToMarketResearchVm.Chapters = selectChapters;

            return View(addQuestionToMarketResearchVm);
        }

        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuestionId,ActualQuestion,ChaptersId")] Question question)
        {
            if (ModelState.IsValid)
            {
                question.QuestionId = Guid.NewGuid();
                _context.Add(question);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChaptersId"] = new SelectList(_context.Chapters, "ChaptersId", "ChaptersId", question.ChaptersId);
            return View(question);
        }

        // GET: Questions/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }
            ViewData["ChaptersId"] = new SelectList(_context.Chapters, "ChaptersId", "Name", question.ChaptersId);
            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("QuestionId,ActualQuestion,ChaptersId")] Question question)
        {
            if (id != question.QuestionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(question);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(question.QuestionId))
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
            ViewData["ChaptersId"] = new SelectList(_context.Chapters, "ChaptersId", "Name", question.ChaptersId);
            return View(question);
        }

        // GET: Questions/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions
                .Include(q => q.Chapter)
                .FirstOrDefaultAsync(m => m.QuestionId == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var question = await _context.Questions.FindAsync(id);
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionExists(Guid id)
        {
            return _context.Questions.Any(e => e.QuestionId == id);
        }

        public IActionResult AddChapter(Guid marketReaserchId)
        {
            var viewModelAddChapter = new AddChapterVm();

            viewModelAddChapter.MarketResearchId = marketReaserchId;

            return View("AddChapter", viewModelAddChapter);
        }

        public async  Task<IActionResult> SaveChapter(Guid marketResearchId, Chapters chapters)
        {
                if (!ModelState.IsValid)
            {
                return View("AddChapter");
            }

            await _dataAccessQuestion.SaveChapter(marketResearchId, chapters);

            AddQuestionToMarketResearchVm viewModel = new AddQuestionToMarketResearchVm();

            viewModel.CurrentMarketResearchId = marketResearchId;

            var listofchapters = _dataAccessQuestion.GetChapters(marketResearchId);

            var selectchapters = GetSelectChapters(listofchapters);

            viewModel.Chapters = selectchapters;

            return View("Create", viewModel);
        }

        private List<SelectListItem> GetSelectChapters(List<Chapters> listOfChapters)
        {
            var selectChapters = new List<SelectListItem>();

            foreach (var chapter in listOfChapters)
            {
                selectChapters.Add(new SelectListItem
                {
                    Text = chapter.Name,
                    Value = chapter.ChaptersId.ToString()
                });

            }

            return selectChapters;
        }

        public async Task <IActionResult> SetOptionsForQuestionType(AddQuestionToMarketResearchVm questionToMarketResearchVm)
        {
            var vm = new AddQuestionToMarketResearchVm();
            questionToMarketResearchVm.GradeChoices = vm.SetGradeChoicesList();
            questionToMarketResearchVm.BinaryChoice = vm.SetBinaryChoiceList();
            await _dataAccessQuestion.saveQuestion(questionToMarketResearchVm);
            return View("Create", questionToMarketResearchVm);
        }

        public async Task<IActionResult> CreateQuestionType(AddQuestionToMarketResearchVm questionToMarketResearchVm, bool buttonstate)
        {
            string qType = questionToMarketResearchVm.QuestionTypes.First().ToString();
            switch (qType)
            {
                case "Graderingsfråga" :
                    {
                        var vm = new AddQuestionToMarketResearchVm();
                        questionToMarketResearchVm.GradeChoices = vm.SetGradeChoicesList();
                        

                        for (int i = 0; i < questionToMarketResearchVm.HighGrade; i++)
                        {

                            var questionOption = new QuestionOption();
                            questionOption.Value = (i + 1).ToString();

                            questionOption.QuestionId = questionToMarketResearchVm.Question.QuestionId;
                            if (i==0)
                            {
                                var listitem = questionToMarketResearchVm.GradeChoices[questionToMarketResearchVm.TypeChoice];
                                string[] headingsInArray = listitem.Text.Split('-');
                                questionOption.QuestionOptionHeading = headingsInArray[0].Trim();
                            }
                            else if(i== questionToMarketResearchVm.HighGrade - 1)
                            {
                                var listitem = questionToMarketResearchVm.GradeChoices[questionToMarketResearchVm.TypeChoice];
                                string[] headingsInArray = listitem.Text.Split('-');
                                questionOption.QuestionOptionHeading = headingsInArray[1].Trim();
                            }

                           await _dataAccessQuestion.AddQuestionOption(questionOption, questionToMarketResearchVm);
                        }
                    break;
                    }
                case "Binärfråga":
                    {
                        var vm = new AddQuestionToMarketResearchVm();
                        questionToMarketResearchVm.BinaryChoice = vm.SetGradeChoicesList();
                        var questionOption = new QuestionOption();
                       var bin= questionToMarketResearchVm.BinaryChoice[questionToMarketResearchVm.TypeChoice];
                        questionOption.QuestionOptionHeading = bin.Text.ToString();
                        questionOption.Value = bin.Text.ToString();
                        await _dataAccessQuestion.AddQuestionOption(questionOption, questionToMarketResearchVm);
                        questionToMarketResearchVm.QuestionTypes = null;
                        break;
                    }
                case "Flervalsfråga":
                    {
                        string x = questionToMarketResearchVm.Alternative.ToString();
                        List<string> queOptList = new List<string>();
                        queOptList.Add(x);
                        if (buttonstate)
                        {

                            var questionOption = new QuestionOption();
                            questionOption.QuestionOptionHeading = questionToMarketResearchVm.Alternative.ToString();
                            questionOption.Value = questionToMarketResearchVm.Alternative.ToString();

                           
                           await _dataAccessQuestion.AddQuestionOptionForFlerval(questionOption, questionToMarketResearchVm);
                            questionToMarketResearchVm.QuestionTypes = null;

                            return View("Create", questionToMarketResearchVm);
                        }
                        else
                        {

                            var questionOption = new QuestionOption();
                            questionOption.QuestionOptionHeading = questionToMarketResearchVm.Alternative.ToString();
                            questionOption.Value = questionToMarketResearchVm.Alternative.ToString();
                            await _dataAccessQuestion.AddQuestionOptionForFlerval( questionOption, questionToMarketResearchVm);
                            ViewData["listOfAlternatives"] = queOptList;
                            return View("Create", questionToMarketResearchVm);
                        }
                        
                    }
                case "Textfråga":
                    {

                        break;
                    }
                default:
                    break;
            }

            return View("Create", questionToMarketResearchVm);

        }

        public IActionResult ShowQuestionsForResponder(Responders responders)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var viewModelAllQuestions = GetQuestions(responders.MarketResearchId);
          
            return View(viewModelAllQuestions);

        }

        public ViewAllQuestionsForMarketResearchVm GetQuestions(Guid marketReseachId)
        {

            var allQuestionsForMarketResearch = _dataAccessQuestion.GetQuestionsForMarketResearch(marketReseachId);

            var viewModelAllQuestions = new ViewAllQuestionsForMarketResearchVm();
            var listOfQuestions = new List<Question>();

            foreach (var item in allQuestionsForMarketResearch)
            {
                listOfQuestions.Add(new Question
                {
                    ActualQuestion = item.Question.ActualQuestion,
                    QuestionId = item.Question.QuestionId,
                    QuestionOptions = item.Question.QuestionOptions,
                    QuestionType = item.Question.QuestionType,
                    MarketResearches = new List<QuestionToMarketResearch> { new QuestionToMarketResearch { MarketResearchId = marketReseachId, QuestionId = item.Question.QuestionId } },
                });
            }

            viewModelAllQuestions.Questions = listOfQuestions;
            

            return viewModelAllQuestions;
        }
    }
}
