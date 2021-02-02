namespace CakeShop.Web.ViewComponents
{
    using System.Threading.Tasks;

    using CakeShop.Services.Data.DessertOrders;
    using CakeShop.Services.Data.Orders;
    using CakeShop.Web.ViewModels.Orders.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent]
    public class GetTotalPriceAndQuantitiesCurrentOrderViewComponent : ViewComponent
    {
        private readonly IDessertOrdersService dessertOrdersService;
        private readonly IOrdersService ordersService;

        public GetTotalPriceAndQuantitiesCurrentOrderViewComponent(
            IDessertOrdersService dessertOrdersService,
            IOrdersService ordersService)
        {
            this.dessertOrdersService = dessertOrdersService;
            this.ordersService = ordersService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string orderId)
        {
            var model = new OrderTotalPriceAndQuantitiesViewModel()
            {
                TotalPrice = await this.ordersService.GetTotalPriceCurrentOrderByOrderAsync(orderId),
                Quantities = await this.dessertOrdersService.GetTotalQuantitiesCurrentOrderAsync(orderId),
            };

            return this.View(model);
        }
    }
}
