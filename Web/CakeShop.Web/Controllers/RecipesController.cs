namespace CakeShop.Web.Controllers
{
    using System.Threading.Tasks;

    using CakeShop.Services.Data.RecipeIngredients;
    using CakeShop.Services.Data.Recipes;
    using CakeShop.Web.ViewModels.Administration.RecipeIngredients.ViewModels;
    using CakeShop.Web.ViewModels.Comments.InputModels;
    using CakeShop.Web.ViewModels.Recipes.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class RecipesController : BaseController
    {
        private readonly IRecipesService recipesService;
        private readonly IRecipeIngredientsService recipeIngredientsService;

        public RecipesController(
            IRecipesService recipesService,
            IRecipeIngredientsService recipeIngredientsService)
        {
            this.recipesService = recipesService;
            this.recipeIngredientsService = recipeIngredientsService;
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
    }
}
