using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mercado.nu.Data;
using mercado.nu.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using mercado.nu.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace mercado.nu.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public HomeController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _context = applicationDbContext;
        }

        public async Task<IActionResult> Index()
        {
            //if (_signInManager.IsSignedIn(User))
            //{
            //    ApplicationUser user = await _userManager.GetUserAsync(User);

            //    if (await _userManager.IsInRoleAsync(user, "CompanyUser") || await _userManager.IsInRoleAsync(user, "SuperCompanyUser"))
            //    {
            //        return RedirectToAction("Index", "Organizations");
            //    }
            //    else if (await _userManager.IsInRoleAsync(user, "User"))
            //    {
            //        return RedirectToAction("MarketResearchForPerson", "MarketResearches");
            //    }
            //    else
            //    {
            //        return RedirectToAction("/Account/Register", "Identity");
            //    }
            //}
            //else
            //{
            //    return RedirectToAction("/Account/Register", "Identity");
            //}

            Migrate();

            return View();
        }


        public IActionResult Migrate()
        {
            _context.Database.Migrate();
            ViewData["Message"] = "Database migrated";
            _context.SaveChanges();
            return View("Index");
        }

        public IActionResult Recreate()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            ViewData["Message"] = "Database recreated";
            return View("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
