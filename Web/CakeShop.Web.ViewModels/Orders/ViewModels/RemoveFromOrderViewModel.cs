namespace CakeShop.Web.ViewModels.Orders.ViewModels
{
    public class RemoveFromOrderViewModel : AllDessertsBasketViewModel
    {
        public decimal TotalPrice { get; set; }

        public string FormattedTotalPrice
            => "$" + this.TotalPrice.ToString("F2");

        public int Quantities { get; set; }

        public string FormattedQuantities
            => this.Quantities + " pcs";
    }
}
