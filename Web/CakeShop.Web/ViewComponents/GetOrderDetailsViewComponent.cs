namespace CakeShop.Web.ViewComponents
{
    using System.Threading.Tasks;

    using CakeShop.Services.Data.Orders;
    using CakeShop.Web.ViewModels.Orders.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent]
    public class GetOrderDetailsViewComponent : ViewComponent
    {
        private readonly IOrdersService ordersService;

        public GetOrderDetailsViewComponent(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string orderId)
        {
            var model = await this.ordersService.GetOrderDetailsAsync<DetailsCurrentOrderViewModel>(orderId);

            return this.View(model);
        }
    }
}
