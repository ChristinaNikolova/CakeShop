namespace CakeShop.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CakeShop.Common;
    using CakeShop.Data.Models;
    using CakeShop.Services.Data.RecipeIngredients;
    using CakeShop.Services.Data.Recipes;
    using CakeShop.Services.Data.Users;
    using CakeShop.Services.Messaging;
    using CakeShop.Web.ViewModels.Administration.RecipeIngredients.ViewModels;
    using CakeShop.Web.ViewModels.Comments.InputModels;
    using CakeShop.Web.ViewModels.Recipes.ViewModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Rotativa.AspNetCore;

    public class RecipesController : BaseController
    {
        private readonly IRecipesService recipesService;
        private readonly IUsersService usersService;
        private readonly IRecipeIngredientsService recipeIngredientsService;
        private readonly IEmailSender emailSender;
        private readonly UserManager<ApplicationUser> userManager;

        public RecipesController(
            IRecipesService recipesService,
            IUsersService usersService,
            IRecipeIngredientsService recipeIngredientsService,
            IEmailSender emailSender,
            UserManager<ApplicationUser> userManager)
        {
            this.recipesService = recipesService;
            this.usersService = usersService;
            this.recipeIngredientsService = recipeIngredientsService;
            this.emailSender = emailSender;
            this.userManager = userManager;
        }

        public async Task<IActionResult> GetAll()
        {
            var model = new AllRecipesViewModel()
            {
                Repices = await this.recipesService.GetAllAsync<RecipeViewModel>(),
            };

            return this.View(model);
        }

        public async Task<IActionResult> GetRecipeDetails(string id)
        {
            var model = await this.recipesService.GetDetailsAsync<RecipeDetailsViewModel>(id);

            model.RepiceIngredients = await this.recipeIngredientsService.GetAllCurrentRecipeAsync<RecipeIngredientViewModel>(id);
            model.AddCommentInputModel = new AddCommentInputModel() { Id = id, };

            return this.View(model);
        }

        public async Task<IActionResult> GenerateRecipePdf(string id)
        {
            var model = await this.recipesService.GetDetailsAsync<RecipePDFViewModel>(id);

            return new ViewAsPdf(model);
        }

        public async Task<IActionResult> SendRecipe(string id)
        {
            var userId = this.userManager.GetUserId(this.User);
            var userEmail = await this.usersService.GetUserEmailByIdAsync(userId);

            var model = await this.recipesService.GetDetailsAsync<RecipePDFViewModel>(id);

            var emailAttachments = this.PreparePdfFile(model);

            await this.emailSender.SendEmailAsync(
                        GlobalConstants.CakeShopEmail,
                        GlobalConstants.SystemName,
                        userEmail,
                        GlobalConstants.SendRecipeAsPdfTitleMessage,
                        string.Format(GlobalConstants.SendRecipeAsPdf),
                        attachments: emailAttachments);

            this.TempData["InfoMessage"] = GlobalConstants.SuccessSendRecipeMessage;

            return this.RedirectToAction(nameof(this.GetRecipeDetails), new { id });
        }

        private List<EmailAttachment> PreparePdfFile(RecipePDFViewModel model)
        {
            var recipeAsBytes = new Rotativa.AspNetCore
                .ViewAsPdf(GlobalConstants.GenerateRecipePdfViewNameName, model)
                .BuildFile(this.ControllerContext)
                .GetAwaiter()
                .GetResult();

            var recipeAsEmailAttachment = new EmailAttachment()
            {
                Content = recipeAsBytes,
                FileName = GlobalConstants.GenerateRecipePdfViewNameName,
                MimeType = GlobalConstants.PdfMimeType,
            };

            var emailAttachments = new List<EmailAttachment>() { recipeAsEmailAttachment };

            return emailAttachments;
        }
    }
}
