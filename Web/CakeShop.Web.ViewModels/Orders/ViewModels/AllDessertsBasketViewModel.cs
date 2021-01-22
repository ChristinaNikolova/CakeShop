namespace CakeShop.Web.ViewModels.Orders.ViewModels
{
    using System.Collections.Generic;

    public class AllDessertsBasketViewModel
    {
        public IEnumerable<DessertBasketViewModel> DessertsInBasket { get; set; }

        public bool IsAlreadyPaid { get; set; }
    }
}
