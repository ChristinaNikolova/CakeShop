namespace CakeShop.Web.ViewModels.DessertOrders.ViewModels
{
    public class UserBasketViewModel
    {
        public decimal TotalPrice { get; set; }

        public string FormatTotalPrice
            => this.TotalPrice.ToString("F2");
    }
}
