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
using Microsoft.Extensions.Logging;

namespace mercado.nu.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IEmailSender _emailSender;
        private readonly AuthService _auth;
        private readonly DataAccessQuestions _dataAccessQuestions;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            ApplicationDbContext applicationDbContext,
            IEmailSender emailSender,
            AuthService auth,
             DataAccessQuestions dataAccessQuestions)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _applicationDbContext = applicationDbContext;
            _emailSender = emailSender;
            _auth = auth;
            _dataAccessQuestions = dataAccessQuestions;
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
            [Display(Name = "Lösenord")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Bekräfta lösenord")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            public Person Person { get; set; }
            public List<SelectListItem> Genders { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            Input = new InputModel();

            string[] arr = Enum.GetNames(typeof(Gender));
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (var item in arr)
            {
                var y = new SelectListItem() { Text = item, Value = item };
                list.Add(y);
            }
            Input.Genders = list;

            ReturnUrl = returnUrl;
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
                    bool addRole = await _auth.AddRole("User");
                    var addRoleToUser = await _auth.AddRoleToUser("User", Input.Person.PersonId.ToString());

                    _logger.LogInformation("User created a new account with password.");
                    //await _dataAccessQuestions.GetRespondersToMarketResearchFromRegistredUser(user.Id); //Kan man göra såhär?
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
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
