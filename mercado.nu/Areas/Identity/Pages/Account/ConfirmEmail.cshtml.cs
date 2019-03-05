using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using mercado.nu.Data;
using mercado.nu.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace mercado.nu.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _applicationDbContext;

        public ConfirmEmailModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);



            var addPersonToOrganization = AddPersonToOrganization(userId);

            if (addPersonToOrganization == false)
            {
                return NotFound("Kunde inte lägga till användaren");
            }

            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Error confirming email for user with ID '{userId}':");
            }

            return Page();
        }

        private bool AddPersonToOrganization(string userId)
        {

            Guid? person_Confirming_Id = null;
            if (_signInManager.IsSignedIn(User))
            {
                var personConfirmingId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                person_Confirming_Id = Guid.Parse(personConfirmingId);

                if (personConfirmingId == userId)
                    return false;
            }
            else
            {
                return false;
            }

            var organization = _applicationDbContext.Organizations.SingleOrDefault(x => x.ContactPerson.PersonId == person_Confirming_Id);
            var person = _applicationDbContext.Persons.Single(x => x.PersonId == Guid.Parse(userId));
            person.OrganizationId = organization.OrganizationId;

            _applicationDbContext.Update(person);
            _applicationDbContext.SaveChanges();

            return true;
        }
    }
}
