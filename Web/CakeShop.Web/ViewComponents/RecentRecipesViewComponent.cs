namespace CakeShop.Web.ViewComponents
{
    using System.Threading.Tasks;

    using CakeShop.Services.Data.Recipes;
    using CakeShop.Web.ViewModels.Recipes.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent]
    public class RecentRecipesViewComponent : ViewComponent
    {
        private readonly IRecipesService recipesService;

        public RecentRecipesViewComponent(IRecipesService recipesService)
        {
            this.recipesService = recipesService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new AllRecentRecipeViewModel()
            {
                Recipes = await this.recipesService.GetRecentRecipesAsync<RecentRecipeViewModel>(),
            };

            return this.View(model);
        }
    }
}
