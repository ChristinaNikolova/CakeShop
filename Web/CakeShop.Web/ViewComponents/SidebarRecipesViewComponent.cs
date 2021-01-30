namespace CakeShop.Web.ViewComponents
{
    using System.Threading.Tasks;

    using CakeShop.Services.Data.Recipes;
    using CakeShop.Web.ViewModels.Recipes.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent]
    public class SidebarRecipesViewComponent : ViewComponent
    {
        private readonly IRecipesService recipesService;

        public SidebarRecipesViewComponent(IRecipesService recipesService)
        {
            this.recipesService = recipesService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string criteria)
        {
            var model = new AllSidebarRecipesViewModel();
            //refacrot
            if (criteria == "recent")
            {
                model.Recipes = await this.recipesService.GetRecentRecipesAsync<SidebarRecipeViewModel>();
            }
            else
            {
                model.Recipes = await this.recipesService.GetPopulartRecipesAsync<SidebarRecipeViewModel>();
            }

            return this.View(model);
        }
    }
}
