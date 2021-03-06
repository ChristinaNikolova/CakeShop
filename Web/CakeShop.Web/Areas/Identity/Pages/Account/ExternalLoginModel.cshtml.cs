﻿namespace CakeShop.Web.Areas.Identity.Pages.Account
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using CakeShop.Common;
    using CakeShop.Data.Models;
    using CakeShop.Services.Messaging;
    using CakeShop.Web.Areas.Identity.Pages.Account.InputModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;

    [AllowAnonymous]
    public class ExternalLoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmailSender emailSender;
        private readonly ILogger<ExternalLoginModel> logger;

        public ExternalLoginModel(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ILogger<ExternalLoginModel> logger,
            IEmailSender emailSender)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.logger = logger;
            this.emailSender = emailSender;
        }

        [BindProperty]
        public ExternalLoginInputModel Input { get; set; }

        public string ProviderDisplayName { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public IActionResult OnGetAsync()
        {
            return this.RedirectToPage("./Login");
        }

        public IActionResult OnPost(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = this.Url.Page("./ExternalLogin", pageHandler: "Callback", values: new { returnUrl });
            var properties = this.signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

            return new ChallengeResult(provider, properties);
        }

        public async Task<IActionResult> OnGetCallbackAsync(string returnUrl = null, string remoteError = null)
        {
            returnUrl ??= this.Url.Content("~/");
            if (remoteError != null)
            {
                this.ErrorMessage = $"Error from external provider: {remoteError}";

                return this.RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            var info = await this.signInManager.GetExternalLoginInfoAsync();

            if (info == null)
            {
                this.ErrorMessage = "Error loading external login information.";

                return this.RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await this.signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

            if (result.Succeeded)
            {
                this.logger.LogInformation("{Name} logged in with {LoginProvider} provider.", info.Principal.Identity.Name, info.LoginProvider);

                return this.LocalRedirect(returnUrl);
            }

            if (result.IsLockedOut)
            {
                return this.RedirectToPage("./Lockout");
            }
            else
            {
                // If the user does not have an account, then ask the user to create an account.
                this.ReturnUrl = returnUrl;
                this.ProviderDisplayName = info.ProviderDisplayName;

                if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
                {
                    this.Input = new ExternalLoginInputModel
                    {
                        Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                    };
                }

                return this.Page();
            }
        }

        public async Task<IActionResult> OnPostConfirmationAsync(string returnUrl = null)
        {
            returnUrl ??= this.Url.Content("~/");
            var info = await this.signInManager.GetExternalLoginInfoAsync();

            if (info == null)
            {
                this.ErrorMessage = "Error loading external login information during confirmation.";

                return this.RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            if (this.ModelState.IsValid)
            {
                ApplicationUser user;

                var firstName = info.Principal.FindFirstValue(ClaimTypes.Name).Split(" ")[0];
                var lastName = info.Principal.FindFirstValue(ClaimTypes.Name).Split(" ").Last();
                var picture = string.Empty;

                if (info.LoginProvider == "Facebook")
                {
                    var identifier = info.Principal.FindFirstValue(ClaimTypes.NameIdentifier);
                    picture = $"https://graph.facebook.com/{identifier}/picture?type=large";
                }
                else if (info.LoginProvider == "Google")
                {
                    picture = info.Principal.FindFirstValue("picture");
                }

                user = new ApplicationUser
                {
                    UserName = this.Input.Email,
                    Email = this.Input.Email,
                    FirstName = firstName,
                    LastName = lastName,
                    Address = this.Input.Address,
                    PhoneNumber = this.Input.PhoneNumber,
                    Picture = picture,
                };

                var result = await this.userManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    result = await this.userManager.AddLoginAsync(user, info);

                    if (result.Succeeded)
                    {
                        var token = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
                        var confirmationLink = this.Url.Action("ConfirmEmail", "Account", new { code = token, userId = user.Id }, this.Request.Scheme);

                        await this.emailSender.SendEmailAsync(
                            GlobalConstants.CakeShopEmail,
                            GlobalConstants.SystemName,
                            user.Email,
                            GlobalConstants.ConfirmProfileTitleMessage,
                            string.Format(GlobalConstants.ConfirmProfileMessage, user.UserName, confirmationLink));

                        return this.RedirectToPage("RegisterConfirmation", new { email = this.Input.Email });
                    }
                }

                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            this.ProviderDisplayName = info.ProviderDisplayName;
            this.ReturnUrl = returnUrl;

            return this.Page();
        }
    }
}
