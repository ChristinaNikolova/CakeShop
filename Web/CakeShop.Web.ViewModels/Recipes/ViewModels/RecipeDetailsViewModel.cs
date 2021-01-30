namespace CakeShop.Web.ViewModels.Recipes.ViewModels
{
    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;
    using CakeShop.Web.ViewModels.Comments.InputModels;

    public class RecipeDetailsViewModel : RecipePDFViewModel, IMapFrom<Recipe>
    {
        public AddCommentInputModel AddCommentInputModel { get; set; }

        public bool IsFavourite { get; set; }
    }
}
