namespace CakeShop.Web.ViewModels.Desserts.ViewModels
{
    using System.Collections.Generic;

    using CakeShop.Web.ViewModels.Common.ViewModels;

    public class AllDessertsByCategoryViewModel : PaginationViewModel
    {
        public IEnumerable<DessertViewModel> Desserts { get; set; }
    }
}
