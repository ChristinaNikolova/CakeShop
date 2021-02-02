namespace CakeShop.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using CakeShop.Common;
    using CakeShop.Data.Models;
    using CakeShop.Services.Data.RecipeIngredients;
    using CakeShop.Services.Data.RecipeLikes;
    using CakeShop.Services.Data.Recipes;
    using CakeShop.Services.Data.Users;
    using CakeShop.Services.Messaging;
    using CakeShop.Web.ViewModels.Administration.RecipeIngredients.ViewModels;
    using CakeShop.Web.ViewModels.Comments.InputModels;
    using CakeShop.Web.ViewModels.RecipeLikes.ViewModels;
    using CakeShop.Web.ViewModels.Recipes.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Rotativa.AspNetCore;

    public class RecipesController : BaseController
    {
        private readonly IRecipesService recipesService;
        private readonly IUsersService usersService;
        private readonly IRecipeIngredientsService recipeIngredientsService;
        private readonly IRecipeLikesService recipeLikesService;
        private readonly IEmailSender emailSender;
        private readonly UserManager<ApplicationUser> userManager;

        public RecipesController(
            IRecipesService recipesService,
            IUsersService usersService,
            IRecipeIngredientsService recipeIngredientsService,
            IRecipeLikesService recipeLikesService,
            IEmailSender emailSender,
            UserManager<ApplicationUser> userManager)
        {
            this.recipesService = recipesService;
            this.usersService = usersService;
            this.recipeIngredientsService = recipeIngredientsService;
            this.recipeLikesService = recipeLikesService;
            this.emailSender = emailSender;
            this.userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> GetAll(int currentPage = 1)
        {
            var recipesCount = await this.recipesService.GetTotalCountRecipesAsync();

            var pageCount = (int)Math.Ceiling((double)recipesCount / GlobalConstants.RecipesPerPage);

            var model = new AllRecipesViewModel()
            {
                Repices = await this.recipesService.GetAllAsync<RecipeViewModel>(GlobalConstants.RecipesPerPage, (currentPage - 1) * GlobalConstants.RecipesPerPage),
                CurrentPage = currentPage,
                PagesCount = pageCount,
            };

            return this.View(model);
        }

        public async Task<IActionResult> RecipeDetails(string id)
        {
            var userId = this.userManager.GetUserId(this.User);

            var model = await this.recipesService.GetDetailsAsync<RecipeDetailsViewModel>(id);

            model.RepiceIngredients = await this.recipeIngredientsService.GetAllCurrentRecipeAsync<RecipeIngredientViewModel>(id);
            model.AddCommentInputModel = new AddCommentInputModel() { Id = id, };
            model.IsFavourite = await this.recipeLikesService.IsFavouriteAsync(id, userId);

            return this.View(model);
        }

        public async Task<IActionResult> GeneratePdf(string id)
        {
            var model = await this.recipesService.GetDetailsAsync<RecipePDFViewModel>(id);

            return new ViewAsPdf(model);
        }

        public async Task<IActionResult> Send(string id)
        {
            var userId = this.userManager.GetUserId(this.User);
            var userEmail = await this.usersService.GetUserEmailByIdAsync(userId);

            var model = await this.recipesService.GetDetailsAsync<RecipePDFViewModel>(id);

            var emailAttachments = PDFController.PreparePdfFile<RecipePDFViewModel>(model, GlobalConstants.GenerateRecipePdfViewName, GlobalConstants.GenerateRecipePdfViewName, this.ControllerContext);

            await this.emailSender.SendEmailAsync(
                        GlobalConstants.CakeShopEmail,
                        GlobalConstants.SystemName,
                        userEmail,
                        GlobalConstants.SendRecipeAsPdfTitleMessage,
                        string.Format(GlobalConstants.SendRecipeAsPdf),
                        attachments: emailAttachments);

            this.TempData["InfoMessage"] = GlobalConstants.SuccessSendRecipeMessage;

            return this.RedirectToAction(nameof(this.RecipeDetails), new { id });
        }

        [HttpPost]
        public async Task<ActionResult<LikeRecipeViewModel>> Like([FromBody] string recipeId)
        {
            var userId = this.userManager.GetUserId(this.User);

            var isAdded = await this.recipeLikesService.LikeRecipeAsync(recipeId, userId);
            var recipeLikesCount = await this.recipeLikesService.GetLikesCountAsync(recipeId);

            return new LikeRecipeViewModel { IsAdded = isAdded, RecipeLikesCount = recipeLikesCount };
        }

        [AllowAnonymous]
        public async Task<IActionResult> AllByCategory(string id)
        {
            var model = new AllRecipesViewModel()
            {
                Repices = await this.recipesService.GetByCategoryAsync<RecipeViewModel>(id),
            };

            return this.View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<AllRecipesViewModel>> OrderByCriteria([FromBody] string criteria)
        {
            var recipes = await this.recipesService.OrderRecipesByCriteriaAsync<RecipeViewModel>(criteria);

            return new AllRecipesViewModel { Repices = recipes };
        }

        [HttpPost]
        public async Task<ActionResult<AllUserFavouriteRecipesViewModel>> RemoveFromFavouriteRecipes([FromBody] string recipeId)
        {
            var userId = this.userManager.GetUserId(this.User);

            var favouriteRecipes = await this.recipeLikesService.UnlikeRecipeAsync<UserFavouriteRecipeViewModel>(recipeId, userId);

            return new AllUserFavouriteRecipesViewModel { FavouriteRecipes = favouriteRecipes };
        }
    }
}
