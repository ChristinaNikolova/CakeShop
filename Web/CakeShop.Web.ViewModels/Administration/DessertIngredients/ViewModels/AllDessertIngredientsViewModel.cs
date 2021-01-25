namespace CakeShop.Web.ViewModels.Administration.DessertIngredients.ViewModels
{
    using System.Collections.Generic;

    public class AllDessertIngredientsViewModel
    {
        public IEnumerable<DessertIngredientViewModel> DessertIngredients { get; set; }
    }
}
