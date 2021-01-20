namespace CakeShop.Web.ViewModels.Orders.ViewModels
{
    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;

    public class DessertBasketViewModel : IMapFrom<DessertOrder>
    {
        public string Id { get; set; }

        public string OrderId { get; set; }

        public string DessertId { get; set; }

        public string DessertName { get; set; }

        public string DessertPicture { get; set; }

        public decimal DessertPrice { get; set; }

        public string FormattedDessertPrice
            => this.DessertPrice.ToString("F2");

        public decimal TotalPrice
            => this.DessertPrice * this.Quantity;

        public string FormattedTotalPrice
            => this.TotalPrice.ToString("F2");

        public int Quantity { get; set; }
    }
}
