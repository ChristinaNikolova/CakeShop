namespace CakeShop.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using CakeShop.Common;
    using CakeShop.Data.Models;
    using CakeShop.Services.Data.Orders;
    using CakeShop.Services.Data.Recipes;
    using CakeShop.Services.Data.Users;
    using CakeShop.Web.ViewModels.Recipes.ViewModels;
    using CakeShop.Web.ViewModels.Users.ViewModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class UsersController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUsersService usersService;
        private readonly IOrdersService ordersService;
        private readonly IRecipesService recipesService;

        public UsersController(
            UserManager<ApplicationUser> userManager,
            IUsersService usersService,
            IOrdersService ordersService,
            IRecipesService recipesService)
        {
            this.userManager = userManager;
            this.usersService = usersService;
            this.ordersService = ordersService;
            this.recipesService = recipesService;
        }

        public async Task<IActionResult> MyOrders(int currentPage = 1)
        {
            var userId = this.userManager.GetUserId(this.User);

            var totalOrdersCount = await this.ordersService.GetOrdersCountCurrentUserAsync(userId);

            var pageCount = (int)Math.Ceiling((double)totalOrdersCount / GlobalConstants.OrdersPerPage);

            var model = new AllUserOrdersBaseViewModel()
            {
                UserOrders = await this.ordersService.GetUserOrdersListAsync<UserOrderBaseViewModel>(userId, GlobalConstants.OrdersPerPage, (currentPage - 1) * GlobalConstants.OrdersPerPage),
                CurrentPage = currentPage,
                PagesCount = pageCount,
            };

            return this.View(model);
        }

        public async Task<IActionResult> FavouriteRecipes()
        {
            var userId = this.userManager.GetUserId(this.User);

            var model = new AllUserFavouriteRecipesViewModel()
            {
                FavouriteRecipes = await this.recipesService.GetUserFavouriteRecipesAsync<UserFavouriteRecipeViewModel>(userId),
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> BellNotification()
        {
            var userId = this.userManager.GetUserId(this.User);

            if (userId == null)
            {
                return false;
            }

            var hasToShowBell = await this.usersService.CheckForBellAsync(userId);

            return hasToShowBell;
        }
    }
}
