namespace CakeShop.Web.ViewModels.Administration.Recipes.ViewModels
{
    using System.Collections.Generic;

    public class AllRecipesAdminViewModel
    {
        public IEnumerable<RecipeAdminViewModel> Recipes { get; set; }
    }
}
