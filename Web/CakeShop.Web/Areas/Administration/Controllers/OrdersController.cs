namespace CakeShop.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using CakeShop.Common;
    using CakeShop.Services.Data.DessertOrders;
    using CakeShop.Services.Data.Orders;
    using CakeShop.Services.Data.Users;
    using CakeShop.Services.Messaging;
    using CakeShop.Web.Controllers;
    using CakeShop.Web.ViewModels.Administration.Orders.ViewModels;
    using CakeShop.Web.ViewModels.Orders.ViewModels;
    using CakeShop.Web.ViewModels.Users.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class OrdersController : AdministrationController
    {
        private readonly IOrdersService ordersService;
        private readonly IUsersService usersService;
        private readonly IDessertOrdersService dessertOrdersService;
        private readonly IEmailSender emailSender;

        public OrdersController(
            IOrdersService ordersService,
            IUsersService usersService,
            IDessertOrdersService dessertOrdersService,
            IEmailSender emailSender)
        {
            this.ordersService = ordersService;
            this.usersService = usersService;
            this.dessertOrdersService = dessertOrdersService;
            this.emailSender = emailSender;
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
                DessertsInBasket = await this.dessertOrdersService.GetDessertsCurrentOrderAsync<DessertBasketViewModel>(id),
                IsAlreadyPaid = true,
            };

            return this.View(model);
        }

        public async Task<IActionResult> Approve(string id)
        {
            var userId = await this.ordersService.ChangeStatusAsync(id, GlobalConstants.ApprovedStatus);
            var userEmail = await this.usersService.GetUserEmailByIdAsync(userId);

            var model = await this.ordersService.GetOrderDetailsAsync<OrderPDFViewModel>(id);
            model.User = await this.usersService.GetUserDataByOrderIdAsync<UserOrderDetailsViewModel>(id);
            model.DessertsInBasket = await this.dessertOrdersService.GetDessertsCurrentOrderAsync<DessertBasketViewModel>(id);

            var emailAttachments = PDFController.PreparePdfFile<OrderPDFViewModel>(model, GlobalConstants.GenerateOrderPdfViewName, GlobalConstants.GenerateOrderPdfViewName, this.ControllerContext);

            await this.emailSender.SendEmailAsync(
                        GlobalConstants.CakeShopEmail,
                        GlobalConstants.SystemName,
                        userEmail,
                        GlobalConstants.OrderConfirmationTitle,
                        string.Format(GlobalConstants.OrderConfirmation),
                        attachments: emailAttachments);

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
