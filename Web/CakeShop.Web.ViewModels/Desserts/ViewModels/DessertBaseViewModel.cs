namespace CakeShop.Web.ViewModels.Desserts.ViewModels
{
    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;

    public class DessertBaseViewModel : IMapFrom<DessertOrder>
    {
        public string DessertName { get; set; }

        public decimal DessertPrice { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice
            => this.DessertPrice * this.Quantity;
    }
}
