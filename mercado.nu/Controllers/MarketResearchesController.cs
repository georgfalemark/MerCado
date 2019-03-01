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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;

namespace mercado.nu
{
    public class MarketResearchesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly DataAccessQuestions _accessQuestions;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;


        public MarketResearchesController(ApplicationDbContext context, DataAccessQuestions dataAccessQuestions, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _accessQuestions = dataAccessQuestions;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        //GET: MarketResearches
        public async Task<IActionResult> Index()
        {
            return View(await _context.MarketResearches.ToListAsync());
        }

        //GET: Pågående undersökningar för en viss organisation
        public async Task<IActionResult> GetMarketResearchesWithStatusOngoing()
        {
            Guid? guidId = null;
            if (_signInManager.IsSignedIn(User))
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                guidId = Guid.Parse(userId);
            }

            Guid? organizationID = null;
            foreach (var item in _context.Persons)
            {
                if (item.PersonId == guidId)
                {
                    organizationID = item.OrganizationId;
                }
            }

            List<MarketResearch> marketResearches = await _context.MarketResearches.Where(x => x.Organization.OrganizationId == organizationID && x.OnGoing == true).ToListAsync();

            return View(marketResearches);
        }

        //GET: Tidigare undersökningar för en viss organisation
        public async Task<IActionResult> GetMarketResearchesWithStatusOld()
        {
            Guid? guidId = null;
            if (_signInManager.IsSignedIn(User))
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                guidId = Guid.Parse(userId);
            }

            Guid? organizationID = null;
            foreach (var item in _context.Persons)
            {
                if (item.PersonId == guidId)
                {
                    organizationID = item.OrganizationId;
                }
            }

            List<MarketResearch> marketResearches = await _context.MarketResearches.Where(x => x.Organization.OrganizationId == organizationID && x.OnGoing == false).ToListAsync();

            return View(marketResearches);
        }


        public class Search
        {
            [Required(ErrorMessage = "Du måste söka på något")]
            [Display(Name = "Sökord")]
            public string SearchPhrase { get; set; }

            public string SearchAlternative { get; set; }
            public List<SelectListItem> SearchAlternatives { get; set; }
        }

        enum WhatToSearchFor
        {
            Organisation, Marknadsundersökning, Fråga
        };


        //En sökvy där man söker på något. 
        public IActionResult SearchFor()
        {
            var search = new Search();

            string[] arr = Enum.GetNames(typeof(WhatToSearchFor));
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (var item in arr)
            {
                var y = new SelectListItem() { Text = item, Value = item };
                list.Add(y);
            }

            search.SearchAlternatives = list;

            return View(search);
        }

        //En vy för sökresultat
        public async Task<IActionResult> GetSearchResult(Search search)
        {
            var searchFunctionVm = new SearchFunctionVm();
            string searchWord = search.SearchPhrase.ToLower();

            if (search.SearchAlternative == "Marknadsundersökning")
            {
                var marketresearch_Direct_Name = _context.MarketResearches.Where(x => x.Name.ToLower() == searchWord).ToList();
                var marketresearch_Contains_This = _context.MarketResearches.Where(x => x.Name.Contains($"{searchWord}")).ToList();

                if (marketresearch_Direct_Name.Count() > 0)
                {
                    searchFunctionVm.MarketResearches = marketresearch_Direct_Name;
                }
                else if (marketresearch_Contains_This.Count() > 0)
                {
                    searchFunctionVm.MarketResearches = marketresearch_Contains_This;
                }
                else
                {
                    return View();
                }

                return View(searchFunctionVm);

            }
            else if (search.SearchAlternative == "Fråga")
            {
                var question_Direct_Name = _context.Questions.Include(x => x.Answers).Where(x => x.ActualQuestion.ToLower() == searchWord).ToList();
                var question_Contains_This = _context.Questions.Include(x => x.Answers).Where(x => x.ActualQuestion.Contains($"{searchWord}")).ToList();

                if (question_Direct_Name.Count() > 0)
                {
                    searchFunctionVm.Questions = question_Direct_Name;
                }
                else if (question_Contains_This.Count() > 0)
                {
                    searchFunctionVm.Questions = question_Contains_This;
                }
                else
                {
                    return View();
                }

                return View(searchFunctionVm);

            }
            else if (search.SearchAlternative == "Organisation")
            {
                var organizations_Direct_Name = _context.Organizations.Include(x => x.MarketResearches).Where(x => x.Name.ToLower() == searchWord).ToList();
                var organization_Contains_This = _context.Organizations.Include(x => x.MarketResearches).Where(x => x.Name.Contains($"{searchWord}")).ToList();

                if (organizations_Direct_Name.Count() > 0)
                {
                    searchFunctionVm.Organizations = organizations_Direct_Name;
                }
                else if (organization_Contains_This.Count() > 0)
                {
                    searchFunctionVm.Organizations = organization_Contains_This;
                }
                else
                {
                    return View();
                }

                return View(searchFunctionVm);

            }
            else
            {
                return View();
            }

            throw new Exception();
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
        public async Task<IActionResult> Create([Bind("MarketResearchId,Name,Description,Purpose,MinAge,MaxAge,Gender,Area,CreationDate,StartDate,EndDate, NumberOfResponders, OnGoing")] MarketResearch marketResearch)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = await _userManager.GetUserAsync(HttpContext.User);
                var userId = applicationUser.Id;

                await _accessQuestions.SetMarketResearchToPersonAndOrganizationAndSave(marketResearch, userId);
                await _accessQuestions.GetRespondersToMarketResearch(marketResearch);
                return RedirectToAction(nameof(GetMarketResearchesWithStatusOngoing));
            }

            return View(marketResearch);
        }

        public IActionResult CreateQuestionsToMarketResearch(Guid id)
        {


            var questionToMarketResearch = new AddQuestionToMarketResearchVm
            {
                //QuestionTypes = _context.QuestionTypes.Select(x=>x).ToList(),
                CurrentMarketResearchId = id
            };

            return RedirectToAction("Create", "Questions", questionToMarketResearch);
        }

        public async Task<IActionResult> CreateQuestionsToMarketResearchDirectly([Bind("MarketResearchId,Name,Description,Purpose,MinAge,MaxAge,Gender,Area,CreationDate,StartDate,EndDate,NumberOfResponders, OnGoing")] MarketResearch marketResearch)
        {
            if (ModelState.IsValid)
            {
                marketResearch.MarketResearchId = Guid.NewGuid();

                ApplicationUser applicationUser = await _userManager.GetUserAsync(HttpContext.User);
                var userId = applicationUser.Id;

                await _accessQuestions.SetMarketResearchToPersonAndOrganizationAndSave(marketResearch, userId);

              
                await _accessQuestions.GetRespondersToMarketResearch(marketResearch);
                await _context.SaveChangesAsync();

            }

            var questionToMarketResearch = new AddQuestionToMarketResearchVm
            {

                CurrentMarketResearchId = marketResearch.MarketResearchId
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

        [Authorize]
        public async Task<IActionResult> MarketResearchForPerson()
        {
            ApplicationUser applicationUser = await _userManager.GetUserAsync(HttpContext.User);
            var userId = applicationUser.Id;

            var marketReaserchesForPerson = _accessQuestions.GetMarketResearchesForPerson(userId);
            

            var viewModelMarketResearchForPerson = new RespondersMarketResearchesVm();
            viewModelMarketResearchForPerson.Responders = marketReaserchesForPerson;
            if(marketReaserchesForPerson.Count == 0)
            {
                ViewData["Inga undersökningar"] = "Det finns inga aktiva undersökningar för dig.";
                return View(viewModelMarketResearchForPerson);
            }

            return View(viewModelMarketResearchForPerson);
        }

        public async Task<IActionResult> PersonsPreviousMarketResearches()
        {
            ApplicationUser applicationUser = await _userManager.GetUserAsync(HttpContext.User);
            var userId = applicationUser.Id;

            var getPrevoiusMarketResearches = _context.Responders.Where(x => x.PersonId == userId && x.MarketResearchCompleted == true).Include(x => x.MarketResearchs).Include(x => x.Persons).ToList();


            var viewModel = new RespondersMarketResearchesVm();
            viewModel.Responders = getPrevoiusMarketResearches;

            if (getPrevoiusMarketResearches.Count == 0)
            {
                ViewData["Inga undersökningar"] = "Det finns inga aktiva undersökningar för dig.";
                return View(viewModel);
            }

            return View(viewModel);
        }
    }
}
