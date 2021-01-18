namespace CakeShop.Web.ViewModels.Desserts.ViewModels
{
    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;

    public class DessertViewModel : IMapFrom<Dessert>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string FormattedPrice
            => this.Price.ToString("F2");

        public string Picture { get; set; }

        public string CategoryName { get; set; }

        public string CategoryId { get; set; }
    }
}
