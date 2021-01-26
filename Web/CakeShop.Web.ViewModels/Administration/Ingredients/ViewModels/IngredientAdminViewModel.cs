namespace CakeShop.Web.ViewModels.Administration.Ingredients.ViewModels
{
    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;

    public class IngredientAdminViewModel : IMapFrom<Ingredient>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int DessertIngredientsCount { get; set; }

        public int RepiceIngredientsCount { get; set; }
    }
}
