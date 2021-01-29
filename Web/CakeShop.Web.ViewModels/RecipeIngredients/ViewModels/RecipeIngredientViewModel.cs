namespace CakeShop.Web.ViewModels.RecipeIngredients.ViewModels
{
    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;

    public class RecipeIngredientViewModel : IMapFrom<RepiceIngredient>
    {
        public string IngredientName { get; set; }

        public string Quantity { get; set; }
    }
}
