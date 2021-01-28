namespace CakeShop.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using CakeShop.Common;
    using CakeShop.Services.Data.Orders;
    using CakeShop.Web.ViewModels.Administration.Orders.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class OrdersController : AdministrationController
    {
        private readonly IOrdersService ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

        public async Task<IActionResult> GetAll()
        {
            var model = new AllOrdersViewModel()
            {
                ProcessingOrders = await this.ordersService.GetAllAsync<OrderViewModel>(GlobalConstants.ProcessingStatus),
                ApprovedOrders = await this.ordersService.GetAllAsync<OrderViewModel>(GlobalConstants.ApprovedStatus),
                CancelledOrders = await this.ordersService.GetAllAsync<OrderViewModel>(GlobalConstants.CancelledStatus),
                DeliveredOrders = await this.ordersService.GetAllAsync<OrderViewModel>(GlobalConstants.DeliveredStatus),
            };

            return this.View(model);
        }
    }
}
