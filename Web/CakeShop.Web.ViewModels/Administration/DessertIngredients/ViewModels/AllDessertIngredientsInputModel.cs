namespace CakeShop.Web.ViewModels.Administration.DessertIngredients.ViewModels
{
    using System.Collections.Generic;

    public class AllDessertIngredientsInputModel
    {
        public IEnumerable<DessertIngredientViewModel> DessertIngredients { get; set; }
    }
}
