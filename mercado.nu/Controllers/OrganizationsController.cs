using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mercado.nu.Data;
using mercado.nu.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace mercado.nu.Controllers
{
    [Authorize]
    public class OrganizationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public OrganizationsController(ApplicationDbContext context, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }

        // GET: Organizations
        public async Task<IActionResult> Index()
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

            Organization organization = _context.Organizations.Include(x => x.ContactPerson).SingleOrDefault(x => x.OrganizationId == organizationID);
            return View(organization);
        }

        // GET: Organizations/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organization = await _context.Organizations
                .Include(o => o.ContactPerson)
                .FirstOrDefaultAsync(m => m.OrganizationId == id);
            if (organization == null)
            {
                return NotFound();
            }

            return View(organization);
        }

        // GET: Organizations/Create
        public IActionResult Create()
        {
            ViewData["ContactPersonId"] = new SelectList(_context.Persons, "PersonId", "PersonId");
            return View();
        }

        // POST: Organizations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrganizationId,Name,StreetName,StreetNumber,City,Country,ContactPersonId,PhoneNumber,Email,PathLogoImage")] Organization organization)
        {
            if (ModelState.IsValid)
            {
                organization.OrganizationId = Guid.NewGuid();

                Guid? guidId = null;
                if (_signInManager.IsSignedIn(User))
                {
                    var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                    guidId = Guid.Parse(userId);
                }
                organization.ContactPersonId = (Guid) guidId;

                _context.Add(organization);

                Person person = _context.Persons.SingleOrDefault(x => x.PersonId == organization.ContactPersonId);
                person.OrganizationId = organization.OrganizationId;

                _context.Update(person);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContactPersonId"] = new SelectList(_context.Persons, "PersonId", "PersonId", organization.ContactPersonId);
            return View(organization);
        }

        // GET: Organizations/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organization = await _context.Organizations.FindAsync(id);
            if (organization == null)
            {
                return NotFound();
            }
            ViewData["ContactPersonId"] = new SelectList(_context.Persons, "PersonId", "PersonId", organization.ContactPersonId);
            return View(organization);
        }

        // POST: Organizations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("OrganizationId,Name,StreetName,StreetNumber,City,Country,ContactPersonId,PhoneNumber,Email,PathLogoImage")] Organization organization)
        {
            if (id != organization.OrganizationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(organization);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrganizationExists(organization.OrganizationId))
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
            ViewData["ContactPersonId"] = new SelectList(_context.Persons, "PersonId", "PersonId", organization.ContactPersonId);
            return View(organization);
        }

        // GET: Organizations/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organization = await _context.Organizations
                .Include(o => o.ContactPerson)
                .FirstOrDefaultAsync(m => m.OrganizationId == id);
            if (organization == null)
            {
                return NotFound();
            }

            return View(organization);
        }

        // POST: Organizations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var organization = await _context.Organizations.FindAsync(id);
            _context.Organizations.Remove(organization);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrganizationExists(Guid id)
        {
            return _context.Organizations.Any(e => e.OrganizationId == id);
        }
    }
}
