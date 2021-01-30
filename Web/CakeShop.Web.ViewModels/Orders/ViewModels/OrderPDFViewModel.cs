namespace CakeShop.Web.ViewModels.Orders.ViewModels
{
    using System.Collections.Generic;

    public class OrderPDFViewModel : DetailsCurrentOrderViewModel
    {
        public IEnumerable<DessertBasketViewModel> DessertsInBasket { get; set; }
    }
}
