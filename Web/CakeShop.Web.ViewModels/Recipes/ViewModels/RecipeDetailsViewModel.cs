namespace CakeShop.Web.ViewModels.Recipes.ViewModels
{
    using System.Collections.Generic;

    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;
    using CakeShop.Web.ViewModels.Administration.RecipeIngredients.ViewModels;

    public class RecipeDetailsViewModel : RecipeViewModel, IMapFrom<Recipe>
    {
        public int Portions { get; set; }

        public int PreparationTime { get; set; }

        public int CookingTime { get; set; }

        public IEnumerable<RecipeIngredientViewModel> RepiceIngredients { get; set; }
    }
}
