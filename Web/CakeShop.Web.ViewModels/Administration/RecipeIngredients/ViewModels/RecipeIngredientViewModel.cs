namespace CakeShop.Web.ViewModels.Administration.RecipeIngredients.ViewModels
{
    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;

    public class RecipeIngredientViewModel : IMapFrom<RepiceIngredient>
    {
        public string IngredientId { get; set; }

        public string IngredientName { get; set; }

        public string Quantity { get; set; }
    }
}
