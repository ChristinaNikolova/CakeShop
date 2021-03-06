﻿namespace CakeShop.Web.Controllers
{
    using System.Threading.Tasks;

    using CakeShop.Common;
    using CakeShop.Data.Models;
    using CakeShop.Services.Data.DessertOrders;
    using CakeShop.Services.Data.Orders;
    using CakeShop.Services.Data.Users;
    using CakeShop.Web.ViewModels.DessertOrders.ViewModels;
    using CakeShop.Web.ViewModels.Desserts.ViewModels;
    using CakeShop.Web.ViewModels.Orders.InputModels;
    using CakeShop.Web.ViewModels.Orders.ViewModels;
    using CakeShop.Web.ViewModels.Users.ViewModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Rotativa.AspNetCore;

    public class OrdersController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IOrdersService ordersService;
        private readonly IUsersService usersService;
        private readonly IDessertOrdersService dessertOrdersService;

        public OrdersController(
            UserManager<ApplicationUser> userManager,
            IOrdersService ordersService,
            IUsersService usersService,
            IDessertOrdersService dessertOrdersService)
        {
            this.userManager = userManager;
            this.ordersService = ordersService;
            this.usersService = usersService;
            this.dessertOrdersService = dessertOrdersService;
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
                DessertsInBasket = await this.dessertOrdersService.GetDessertsInBasketAsync<DessertBasketViewModel>(userId),
                IsAlreadyPaid = false,
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
        public async Task<ActionResult<RemoveFromOrderViewModel>> RemoveFromBasket([FromBody] RemoveFromOrderInputModel input)
        {
            if (string.IsNullOrWhiteSpace(input.Id))
            {
                return this.RedirectToAction(nameof(this.SeeBasket));
            }

            var userId = this.userManager.GetUserId(this.User);

            var dessertsInBasket = await this.ordersService.RemoveFromBasketAsync<DessertBasketViewModel>(input.Id, userId);

            var totalPrice = await this.ordersService.GetTotalPriceCurrentOrderByUserAsync(userId);
            var orderId = await this.ordersService.GetOrderIdByUserAsync(userId);
            var quantities = await this.dessertOrdersService.GetTotalQuantitiesCurrentOrderAsync(orderId);

            return new RemoveFromOrderViewModel
            {
                DessertsInBasket = dessertsInBasket,
                TotalPrice = totalPrice,
                Quantities = quantities,
            };
        }

        public async Task<IActionResult> Checkout()
        {
            var userId = this.userManager.GetUserId(this.User);
            var orderId = await this.ordersService.GetOrderIdByUserAsync(userId);

            var model = new CheckoutInputModel()
            {
                User = await this.usersService.GetUserDataAsync<UserCheckoutViewModel>(userId),
                Desserts = await this.dessertOrdersService.GetDessertsInBasketAsync<DessertBaseViewModel>(userId),
                TotalPrice = await this.ordersService.GetTotalPriceCurrentOrderByUserAsync(userId),
                Quantities = await this.dessertOrdersService.GetTotalQuantitiesCurrentOrderAsync(orderId),
            };

            return this.View(model);
        }

        public async Task<IActionResult> Buy(CheckoutInputModel input)
        {
            var userId = this.userManager.GetUserId(this.User);
            var orderId = await this.ordersService.GetOrderIdByUserAsync(userId);
            var totalPrice = await this.ordersService.GetTotalPriceCurrentOrderByUserAsync(userId);

            if (!this.ModelState.IsValid)
            {
                input.User = await this.usersService.GetUserDataAsync<UserCheckoutViewModel>(userId);
                input.Desserts = await this.dessertOrdersService.GetDessertsInBasketAsync<DessertBaseViewModel>(userId);
                input.TotalPrice = totalPrice;
                input.Quantities = await this.dessertOrdersService.GetTotalQuantitiesCurrentOrderAsync(orderId);

                return this.View(input);
            }

            await this.ordersService.AddDetailsToCurrentOrderAsync(orderId, input.DeliveryAddress, input.Notes);

            return this.Redirect($"/Paypal/CreatePayment?totalPrice={totalPrice}");
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

        public async Task<IActionResult> GenerateOrderPdf(string id)
        {
            var model = await this.ordersService.GetOrderDetailsAsync<OrderPDFViewModel>(id);
            model.User = await this.usersService.GetUserDataByOrderIdAsync<UserOrderDetailsViewModel>(id);
            model.DessertsInBasket = await this.dessertOrdersService.GetDessertsCurrentOrderAsync<DessertBasketViewModel>(id);

            return new ViewAsPdf(model);
        }
    }
}
