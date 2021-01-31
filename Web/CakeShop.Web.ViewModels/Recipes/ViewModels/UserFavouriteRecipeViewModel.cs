namespace CakeShop.Web.ViewModels.Recipes.ViewModels
{
    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;

    public class UserFavouriteRecipeViewModel : RecipeBaseViewModel, IMapFrom<Recipe>
    {
        public string CategoryName { get; set; }
    }
}
