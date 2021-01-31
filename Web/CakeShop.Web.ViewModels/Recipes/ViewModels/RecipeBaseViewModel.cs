namespace CakeShop.Web.ViewModels.Recipes.ViewModels
{
    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;

    public class RecipeBaseViewModel : IMapFrom<Recipe>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Picture { get; set; }
    }
}
