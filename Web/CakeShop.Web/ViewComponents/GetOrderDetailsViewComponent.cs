namespace CakeShop.Web.ViewComponents
{
    using System.Threading.Tasks;

    using CakeShop.Services.Data.Orders;
    using CakeShop.Services.Data.Users;
    using CakeShop.Web.ViewModels.Orders.ViewModels;
    using CakeShop.Web.ViewModels.Users.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent]
    public class GetOrderDetailsViewComponent : ViewComponent
    {
        private readonly IOrdersService ordersService;
        private readonly IUsersService usersService;

        public GetOrderDetailsViewComponent(
            IOrdersService ordersService,
            IUsersService usersService)
        {
            this.ordersService = ordersService;
            this.usersService = usersService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string orderId)
        {
            var model = await this.ordersService.GetOrderDetailsAsync<DetailsCurrentOrderViewModel>(orderId);
            model.User = await this.usersService.GetUserDataByOrderIdAsync<UserOrderDetailsViewModel>(orderId);

            return this.View(model);
        }
    }
}
