namespace CakeShop.Web.ViewModels.Administration.RecipeIngredients.ViewModels
{
    using System.Collections.Generic;

    public class AllRecipeIngredientsViewModel
    {
        public IEnumerable<RecipeIngredientViewModel> RecipeIngredients { get; set; }
    }
}
