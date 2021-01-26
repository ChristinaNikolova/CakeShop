namespace CakeShop.Web.ViewModels.Administration.Recipes.ViewModels
{
    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;

    public class RecipeAdminViewModel : IMapFrom<Recipe>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Picture { get; set; }

        public string CategoryName { get; set; }

        public int CommentsCount { get; set; }

        public int RecipeLikesCount { get; set; }
    }
}
