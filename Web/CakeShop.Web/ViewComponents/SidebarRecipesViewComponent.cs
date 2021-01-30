namespace CakeShop.Web.ViewComponents
{
    using System.Threading.Tasks;

    using CakeShop.Common;
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

            if (criteria == GlobalConstants.CriteriaRecent)
            {
                model.Recipes = await this.recipesService.GetRecentRecipesAsync<SidebarRecipeViewModel>();
                model.Criteria = GlobalConstants.CriteriaRecent;
            }
            else
            {
                model.Recipes = await this.recipesService.GetPopulartRecipesAsync<SidebarRecipeViewModel>();
                model.Criteria = GlobalConstants.CriteriaPopular;
            }

            return this.View(model);
        }
    }
}
