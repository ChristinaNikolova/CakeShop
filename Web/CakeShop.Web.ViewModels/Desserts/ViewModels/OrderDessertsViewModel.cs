namespace CakeShop.Web.ViewModels.Desserts.ViewModels
{
    using System.Collections.Generic;

    public class OrderDessertsViewModel
    {
        public IEnumerable<DessertViewModel> OrderedDesserts { get; set; }

        public string TargetCriteria { get; set; }
    }
}
