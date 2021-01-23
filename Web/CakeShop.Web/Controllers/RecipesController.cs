namespace CakeShop.Web.Controllers
{
    using System.Threading.Tasks;

    using CakeShop.Services.Data.Recipes;
    using CakeShop.Web.ViewModels.Recipes.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class RecipesController : BaseController
    {
        private readonly IRecipesService recipesService;

        public RecipesController(IRecipesService recipesService)
        {
            this.recipesService = recipesService;
        }

        public async Task<IActionResult> GetAll()
        {
            var model = new AllRecipesViewModel()
            {
                Repices = await this.recipesService.GetAllAsync<RecipeViewModel>(),
            };

            return this.View(model);
        }
    }
}
