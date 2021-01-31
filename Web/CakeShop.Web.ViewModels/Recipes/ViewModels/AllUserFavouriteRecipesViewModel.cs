namespace CakeShop.Web.ViewModels.Recipes.ViewModels
{
    using System.Collections.Generic;

    public class AllUserFavouriteRecipesViewModel
    {
        public IEnumerable<UserFavouriteRecipeViewModel> FavouriteRecipes { get; set; }
    }
}
