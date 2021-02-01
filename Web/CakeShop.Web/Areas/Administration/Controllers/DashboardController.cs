namespace CakeShop.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using CakeShop.Services.Data.Comments;
    using CakeShop.Services.Data.Orders;
    using CakeShop.Web.ViewModels.Administration.Dashboard.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdministrationController
    {
        private readonly IOrdersService ordersService;
        private readonly ICommentsService commentsService;

        public DashboardController(
            IOrdersService ordersService,
            ICommentsService commentsService)
        {
            this.ordersService = ordersService;
            this.commentsService = commentsService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new AdminStatisticViewModel()
            {
                NewOrdersCount = await this.ordersService.GetProcessingOrdersCountAsync(),
                NewCommentsCount = await this.commentsService.GetNewCommentsCountAsync(),
            };

            return this.View(model);
        }
    }
}
