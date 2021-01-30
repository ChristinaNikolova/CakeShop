namespace CakeShop.Web.ViewModels.Recipes.ViewModels
{
    using System.Collections.Generic;

    public class AllSidebarRecipesViewModel
    {
        public IEnumerable<SidebarRecipeViewModel> Recipes { get; set; }

        public string Criteria { get; set; }
    }
}
