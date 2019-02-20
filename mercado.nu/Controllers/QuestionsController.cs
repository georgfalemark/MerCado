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

        public IActionResult SetOptionsForQuestionType(AddQuestionToMarketResearchVm questionToMarketResearchVm)
        {

            return View("Create", questionToMarketResearchVm);
        }
    }
}
