using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mercado.nu.Data;
using mercado.nu.Models;
using mercado.nu.Models.ViewModels;
using mercado.nu.Models.Entities;

namespace mercado.nu
{
    public class MarketResearchesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly DataAccessQuestions _accessQuestions;


        public MarketResearchesController(ApplicationDbContext context, DataAccessQuestions dataAccessQuestions )
        {
            _context = context;
            _accessQuestions = dataAccessQuestions;
        }

        // GET: MarketResearches
        public async Task<IActionResult> Index()
        {
            List<string> questionType = await _context.QuestionTypes.Select(x => x.QuestionType).ToListAsync();
            if (questionType.Count == 0)
            {
                await _accessQuestions.SeedQuestionTypes();
            }


            return View(await _context.MarketResearches.ToListAsync());
        }

        // GET: MarketResearches/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marketResearch = await _context.MarketResearches
                .FirstOrDefaultAsync(m => m.MarketResearchId == id);
            if (marketResearch == null)
            {
                return NotFound();
            }

            return View(marketResearch);
        }

        // GET: MarketResearches/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MarketResearches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MarketResearchId,Name,Description,Purpose,MinAge,MaxAge,Gender,Area,CreationDate,StartDate,EndDate")] MarketResearch marketResearch)
        {
            if (ModelState.IsValid)
            {
                marketResearch.MarketResearchId = Guid.NewGuid();
                _context.Add(marketResearch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(marketResearch);
        }

        public IActionResult CreateQuestionsToMarketResearch(Guid id)
        {


            var questionToMarketResearch = new AddQuestionToMarketResearchVm
            {
                QuestionTypes = _context.QuestionTypes.Select(x=>x).ToList(),
                CurrentMarketResearchId = id
            };
            
            return RedirectToAction("Create", "Questions", questionToMarketResearch);
        }


        // GET: MarketResearches/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marketResearch = await _context.MarketResearches.FindAsync(id);
            if (marketResearch == null)
            {
                return NotFound();
            }
            return View(marketResearch);
        }

        // POST: MarketResearches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("MarketResearchId,Name,Description,Purpose,MinAge,MaxAge,Gender,Area,CreationDate,StartDate,EndDate")] MarketResearch marketResearch)
        {
            if (id != marketResearch.MarketResearchId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(marketResearch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MarketResearchExists(marketResearch.MarketResearchId))
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
            return View(marketResearch);
        }

        // GET: MarketResearches/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marketResearch = await _context.MarketResearches
                .FirstOrDefaultAsync(m => m.MarketResearchId == id);
            if (marketResearch == null)
            {
                return NotFound();
            }

            return View(marketResearch);
        }

        // POST: MarketResearches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var marketResearch = await _context.MarketResearches.FindAsync(id);
            _context.MarketResearches.Remove(marketResearch);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MarketResearchExists(Guid id)
        {
            return _context.MarketResearches.Any(e => e.MarketResearchId == id);
        }
    }
}
