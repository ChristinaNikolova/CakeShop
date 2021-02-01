namespace CakeShop.Web.ViewModels.DessertOrders.ViewModels
{
    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;

    public class WaitingForReviewDessertViewModel : IMapFrom<DessertOrder>
    {
        public string DessertId { get; set; }

        public string DessertName { get; set; }

        public decimal DessertPrice { get; set; }

        public string FormattedPrice
            => this.DessertPrice.ToString("F2");

        public string DessertPicture { get; set; }

        public string DessertCategoryName { get; set; }

        public string DessertCategoryId { get; set; }

        public string OrderId { get; set; }
    }
}
