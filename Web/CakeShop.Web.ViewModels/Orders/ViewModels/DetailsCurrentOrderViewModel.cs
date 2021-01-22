namespace CakeShop.Web.ViewModels.Orders.ViewModels
{
    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;
    using CakeShop.Web.ViewModels.Users.ViewModels;

    public class DetailsCurrentOrderViewModel : UserOrderBaseViewModel, IMapFrom<Order>
    {
        public string DeliveryAddress { get; set; }

        public string Notes { get; set; }
    }
}
