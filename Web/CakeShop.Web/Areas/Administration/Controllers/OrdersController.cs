namespace CakeShop.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using CakeShop.Common;
    using CakeShop.Services.Data.Orders;
    using CakeShop.Web.ViewModels.Administration.Orders.ViewModels;
    using CakeShop.Web.ViewModels.Orders.ViewModels;
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

        public async Task<IActionResult> GetOrderDetails(string id)
        {
            var model = new AllDessertsBasketViewModel()
            {
                DessertsInBasket = await this.ordersService.GetDessertsCurrentOrderAsync<DessertBasketViewModel>(id),
                IsAlreadyPaid = true,
            };

            return this.View(model);
        }

        public async Task<IActionResult> Approve(string id)
        {
            await this.ordersService.ChangeStatusAsync(id, GlobalConstants.ApprovedStatus);

            return this.RedirectToAction(nameof(this.GetOrderDetails), new { Id = id });
        }

        public async Task<IActionResult> Cancel(string id)
        {
            await this.ordersService.ChangeStatusAsync(id, GlobalConstants.CancelledStatus);

            return this.RedirectToAction(nameof(this.GetOrderDetails), new { Id = id });
        }

        public async Task<IActionResult> Delivery(string id)
        {
            await this.ordersService.ChangeStatusAsync(id, GlobalConstants.DeliveredStatus);

            return this.RedirectToAction(nameof(this.GetOrderDetails), new { Id = id });
        }

        public async Task<IActionResult> Process(string id)
        {
            await this.ordersService.ChangeStatusAsync(id, GlobalConstants.ProcessingStatus);

            return this.RedirectToAction(nameof(this.GetOrderDetails), new { Id = id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await this.ordersService.DeleteAsync(id);

            this.TempData["InfoMessage"] = GlobalConstants.SuccessDeleteMessage;

            return this.RedirectToAction(nameof(this.GetAll));
        }
    }
}
