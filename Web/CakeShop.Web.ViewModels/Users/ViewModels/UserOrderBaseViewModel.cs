namespace CakeShop.Web.ViewModels.Users.ViewModels
{
    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;
    using CakeShop.Web.ViewModels.Administration.Orders.ViewModels;

    public class UserOrderBaseViewModel : OrderViewModel, IMapFrom<Order>
    {
        public string OrderStatus { get; set; }
    }
}
