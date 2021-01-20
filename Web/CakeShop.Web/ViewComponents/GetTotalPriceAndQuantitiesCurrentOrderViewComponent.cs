namespace CakeShop.Web.ViewComponents
{
    using System.Threading.Tasks;

    using CakeShop.Services.Data.Orders;
    using CakeShop.Web.ViewModels.Orders.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent]
    public class GetTotalPriceAndQuantitiesCurrentOrderViewComponent : ViewComponent
    {
        private readonly IOrdersService ordersService;

        public GetTotalPriceAndQuantitiesCurrentOrderViewComponent(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string orderId)
        {
            var model = new OrderTotalPriceAndQuantitiesViewModel()
            {
                TotalPrice = await this.ordersService.GetTotalPriceCurrentOrderByOrderAsync(orderId),
                Quantities = await this.ordersService.GetTotalQuantitiesCurrentOrderAsync(orderId),
            };

            return this.View(model);
        }
    }
}
