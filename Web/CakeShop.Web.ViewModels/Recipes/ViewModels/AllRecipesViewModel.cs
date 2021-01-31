namespace CakeShop.Web.ViewModels.Recipes.ViewModels
{
    using System.Collections.Generic;

    using CakeShop.Web.ViewModels.Common.ViewModels;

    public class AllRecipesViewModel : PaginationViewModel
    {
        public IEnumerable<RecipeViewModel> Repices { get; set; }
    }
}
