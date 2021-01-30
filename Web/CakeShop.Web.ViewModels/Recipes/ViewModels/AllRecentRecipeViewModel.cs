namespace CakeShop.Web.ViewModels.Recipes.ViewModels
{
    using System.Collections.Generic;

    public class AllRecentRecipeViewModel
    {
        public IEnumerable<RecentRecipeViewModel> Recipes { get; set; }
    }
}
