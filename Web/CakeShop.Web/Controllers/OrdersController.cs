namespace CakeShop.Web.Controllers
{
    using System.Threading.Tasks;

    using CakeShop.Common;
    using CakeShop.Data.Models;
    using CakeShop.Services.Data.Orders;
    using CakeShop.Web.ViewModels.DessertOrders.ViewModels;
    using CakeShop.Web.ViewModels.Orders.InputModels;
    using CakeShop.Web.ViewModels.Orders.ViewModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class OrdersController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IOrdersService ordersService;

        public OrdersController(
            UserManager<ApplicationUser> userManager,
            IOrdersService ordersService)
        {
            this.userManager = userManager;
            this.ordersService = ordersService;
        }

        [HttpPost]
        public async Task<ActionResult<DessertOrderViewModel>> AddToBasket([FromBody] AddToOrderInputModel input)
        {
            var userId = this.userManager.GetUserId(this.User);

            if (!this.ModelState.IsValid)
            {
                var totalPriceByError = await this.ordersService.GetTotalPriceCurrentOrderByUserAsync(userId);

                return new DessertOrderViewModel
                {
                    TotalPrice = totalPriceByError,
                    Message = GlobalConstants.ErrorDessertQuantity,
                    IsSuccess = false,
                };
            }

            var orderId = await this.ordersService.AddToBasketAsync(userId, input.DessertId, input.Quantity);

            var totalPrice = await this.ordersService.GetTotalPriceCurrentOrderByOrderAsync(orderId);

            return new DessertOrderViewModel
            {
                TotalPrice = totalPrice,
                Message = GlobalConstants.AddedToBasketSuccessfully,
                IsSuccess = true,
            };
        }

        public async Task<IActionResult> SeeBasket()
        {
            var userId = this.userManager.GetUserId(this.User);

            var model = new AllDessertsBasketViewModel()
            {
                DessertsInBasket = await this.ordersService.GetDessertsInBasketAsync<DessertBasketViewModel>(userId),
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<ActionResult<UserBasketViewModel>> GetTotalPrice()
        {
            var userId = this.userManager.GetUserId(this.User);

            if (userId == null)
            {
                return new UserBasketViewModel { TotalPrice = GlobalConstants.EmptyBasketPrice };
            }

            var totalPrice = await this.ordersService.GetTotalPriceCurrentOrderByUserAsync(userId);

            return new UserBasketViewModel { TotalPrice = totalPrice };
        }

        [HttpPost]
        public async Task<ActionResult<AllDessertsBasketViewModel>> RemoveFromBasket([FromBody] RemoveFromOrderInputModel input)
        {
            if (string.IsNullOrWhiteSpace(input.Id))
            {
                return this.RedirectToAction(nameof(this.SeeBasket));
            }

            var userId = this.userManager.GetUserId(this.User);

            var dessertsInBasket = await this.ordersService.RemoveFromBasketAsync<DessertBasketViewModel>(input.Id, userId);

            return new AllDessertsBasketViewModel { DessertsInBasket = dessertsInBasket };
        }
    }
}
