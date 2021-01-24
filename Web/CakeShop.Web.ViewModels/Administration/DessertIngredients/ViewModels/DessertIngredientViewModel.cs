namespace CakeShop.Web.ViewModels.Administration.DessertIngredients.ViewModels
{
    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;

    public class DessertIngredientViewModel : IMapFrom<DessertIngredient>
    {
        public string IngredientId { get; set; }

        public string IngredientName { get; set; }
    }
}
