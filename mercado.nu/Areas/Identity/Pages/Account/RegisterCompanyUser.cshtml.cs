using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using mercado.nu.Data;
using mercado.nu.Models;
using mercado.nu.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace mercado.nu.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterCompanyUserModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IEmailSender _emailSender;
        private readonly AuthService _auth;

        public RegisterCompanyUserModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            ApplicationDbContext applicationDbContext,
            IEmailSender emailSender,
            AuthService auth)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _applicationDbContext = applicationDbContext;
            _emailSender = emailSender;
            _auth = auth;
        }


        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "L�senord")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Bekr�fta l�senord")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            public Person Person { get; set; }
            public Organization Organization { get; set; }
            public List<SelectListItem> Organizations { get; set; }
            public List<SelectListItem> Genders { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            //Denna metod fungerar som denna vy:ns kontroller!
            Input = new InputModel();
            var y = _applicationDbContext.Organizations.Select(x => new SelectListItem() { Text = x.Name, Value = x.OrganizationId.ToString() });
            Input.Organizations = y.ToList();


            string[] arr = Enum.GetNames(typeof(Gender));
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (var item in arr)
            {
                var yy = new SelectListItem() { Text = item, Value = item };
                list.Add(yy);
            }
            Input.Genders = list;

        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                Guid myGuid = Guid.NewGuid();

                Input.Person.PersonId = myGuid;

                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email, Id = myGuid, PersonId = myGuid, Person = Input.Person };
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    bool addRole = await _auth.AddRole("CompanyUser");
                    var addRoleToUser = await _auth.AddRoleToUser("CompanyUser", Input.Person.PersonId.ToString());

                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _signInManager.SignInAsync(user, isPersistent: false);



                    //Kontrollera ifall anv�ndare verkligen tillh�r f�retaget!
                    var xy = Input.Organization.OrganizationId;
                    Person p1 = _applicationDbContext.Persons.Single(x => x.OrganizationId == xy);
                    ApplicationUser userEmail = await _userManager.FindByIdAsync(p1.PersonId.ToString());
                    string email = userEmail.Email;

                    await _emailSender.SendEmailAsync(email, "Confirm your collugue",
                        $"Please confirm {user.Email} as your collugue by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
