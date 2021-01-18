namespace CakeShop.Web.Controllers
{
    using System.Threading.Tasks;

    using CakeShop.Common;
    using CakeShop.Data.Models;
    using CakeShop.Services.Data.Orders;
    using CakeShop.Web.ViewModels.DessertOrders.ViewModels;
    using CakeShop.Web.ViewModels.Orders.InputModels;
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

        [HttpPost]
        public async Task<ActionResult<UserBasketViewModel>> GetTotalPrice()
        {
            //to do without user
            var userId = this.userManager.GetUserId(this.User);

            var totalPrice = await this.ordersService.GetTotalPriceCurrentOrderByUserAsync(userId);

            return new UserBasketViewModel { TotalPrice = totalPrice };
        }
    }

    public class UserBasketViewModel
    {
        public decimal TotalPrice { get; set; }

        public string FormatTotalPrice
            => this.TotalPrice.ToString("F2");
    }
}
