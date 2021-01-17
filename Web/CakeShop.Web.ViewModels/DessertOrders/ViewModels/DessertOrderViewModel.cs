namespace CakeShop.Web.ViewModels.DessertOrders.ViewModels
{
    public class DessertOrderViewModel
    {
        public decimal TotalPrice { get; set; }

        public string FormatTotalPrice
            => this.TotalPrice.ToString("F2");

        public string Message { get; set; }

        public bool IsSuccess { get; set; }
    }
}
