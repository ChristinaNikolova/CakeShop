namespace CakeShop.Web.ViewModels.Desserts.ViewModels
{
    using System.Collections.Generic;

    public class AllFavouriteDessertsViewModel
    {
        public IEnumerable<DessertViewModel> FavouriteDesserts { get; set; }
    }
}
