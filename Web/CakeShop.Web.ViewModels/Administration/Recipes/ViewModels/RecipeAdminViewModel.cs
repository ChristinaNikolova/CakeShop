namespace CakeShop.Web.ViewModels.Administration.Recipes.ViewModels
{
    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;

    public class RecipeAdminViewModel : RecipeAdminBaseViewModel, IMapFrom<Recipe>
    {
        public int CommentsCount { get; set; }

        public int RecipeLikesCount { get; set; }
    }
}
