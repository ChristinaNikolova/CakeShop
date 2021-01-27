namespace CakeShop.Web.ViewModels.Administration.Recipes.ViewModels
{
    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;

    public class RecipeAdminBaseViewModel : IMapFrom<Recipe>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Picture { get; set; }

        public string CategoryName { get; set; }
    }
}
