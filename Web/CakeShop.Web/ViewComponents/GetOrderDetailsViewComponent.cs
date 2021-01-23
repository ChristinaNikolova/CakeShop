namespace CakeShop.Web.ViewComponents
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using CakeShop.Data.Models;
    using CakeShop.Services.Data.Orders;
    using CakeShop.Services.Data.Users;
    using CakeShop.Web.ViewModels.Orders.ViewModels;
    using CakeShop.Web.ViewModels.Users.ViewModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent]
    public class GetOrderDetailsViewComponent : ViewComponent
    {
        private readonly IOrdersService ordersService;
        private readonly IUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;

        public GetOrderDetailsViewComponent(
            IOrdersService ordersService,
            IUsersService usersService,
            UserManager<ApplicationUser> userManager)
        {
            this.ordersService = ordersService;
            this.usersService = usersService;
            this.userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(string orderId)
        {
            var userId = this.userManager.GetUserId((ClaimsPrincipal)this.User);

            var model = await this.ordersService.GetOrderDetailsAsync<DetailsCurrentOrderViewModel>(orderId);
            model.User = await this.usersService.GetUserDataAsync<UserOrderDetailsViewModel>(userId);

            return this.View(model);
        }
    }
}
