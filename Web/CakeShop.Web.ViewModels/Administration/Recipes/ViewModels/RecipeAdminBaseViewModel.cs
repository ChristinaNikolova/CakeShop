namespace CakeShop.Web.ViewModels.Administration.Recipes.ViewModels
{
    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;
    using CakeShop.Web.ViewModels.Recipes.ViewModels;

    public class RecipeAdminBaseViewModel : UserFavouriteRecipeViewModel, IMapFrom<Recipe>
    {
    }
}
