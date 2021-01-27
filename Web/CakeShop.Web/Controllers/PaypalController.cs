namespace CakeShop.Web.Controllers
{
    using System.Threading.Tasks;
    using CakeShop.Common;
    using CakeShop.Data.Models;
    using CakeShop.Services.Data.Orders;
    using CakeShop.Services.Paypal;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class PaypalController : BaseController
    {
        private readonly IPaypalService paypalService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IOrdersService ordersService;

        public PaypalController(
            IPaypalService paypalService,
            UserManager<ApplicationUser> userManager,
            IOrdersService ordersService)
        {
            this.paypalService = paypalService;
            this.userManager = userManager;
            this.ordersService = ordersService;
        }

        public async Task<IActionResult> CreatePayment([FromQuery] decimal totalPrice)
        {
            var result = await this.paypalService.CreatePayment(totalPrice);

            foreach (var link in result.links)
            {
                if (link.rel.Equals("approval_url"))
                {
                    return this.Redirect(link.href);
                }
            }

            return this.NotFound();
        }

        public async Task<IActionResult> SuccessedPayment(string paymentId, string token, string payerId)
        {
            await this.paypalService.ExecutePayment(payerId, paymentId, token);

            var userId = this.userManager.GetUserId(this.User);

            var orderId = await this.ordersService.FinishOrderAsync(userId);

            this.TempData["InfoMessage"] = GlobalConstants.PaymentSuccess;

            return this.Redirect($"/Orders/GetOrderDetails/{orderId}");
        }
    }
}
