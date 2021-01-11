namespace CakeShop.Web.Areas.Identity.Pages.Account
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using CakeShop.Common;
    using CakeShop.Data.Models;
    using CakeShop.Services.Messaging;
    using CakeShop.Web.Areas.Identity.Pages.Account.InputModels;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Extensions.Logging;

    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<RegisterModel> logger;
        private readonly IEmailSender emailSender;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.emailSender = emailSender;
        }

        [BindProperty]
        public RegisterInputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public async Task OnGetAsync(string returnUrl = null)
        {
            this.ReturnUrl = returnUrl;
            this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= this.Url.Content("~/");
            this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (this.ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    Email = this.Input.Email,
                    UserName = this.Input.UserName,
                    FirstName = this.Input.FirstName,
                    LastName = this.Input.LastName,
                    Address = this.Input.Address,
                    PhoneNumber = this.Input.PhoneNumber,
                };

                var result = await this.userManager.CreateAsync(user, this.Input.Password);

                if (result.Succeeded)
                {
                    this.logger.LogInformation("User created a new account with password.");
                    var token = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = this.Url.Action("ConfirmEmail", "Account", new { code = token, userId = user.Id }, this.Request.Scheme);

                    await this.emailSender.SendEmailAsync(
                        GlobalConstants.CakeShopEmail,
                        GlobalConstants.SystemName,
                        user.Email,
                        GlobalConstants.ConfirmProfileTitleMessage,
                        string.Format(GlobalConstants.ConfirmProfileMessage, user.UserName, confirmationLink));

                    return this.RedirectToPage("RegisterConfirmation", new { email = this.Input.Email, returnUrl });
                }

                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return this.Page();
        }
    }
}
