namespace CakeShop.Web.Controllers
{
    using System.Threading.Tasks;

    using CakeShop.Data.Models;
    using CakeShop.Services.Data.Users;
    using CakeShop.Web.ViewModels.Users.ViewModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class UsersController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUsersService usersService;

        public UsersController(
            UserManager<ApplicationUser> userManager,
            IUsersService usersService)
        {
            this.userManager = userManager;
            this.usersService = usersService;
        }

        public async Task<IActionResult> MyOrders()
        {
            var userId = this.userManager.GetUserId(this.User);

            var model = new AllUserOrdersBaseViewModel()
            {
                UserOrders = await this.usersService.GetUserOrdersListAsync<UserOrderBaseViewModel>(userId),
            };

            return this.View(model);
        }
    }
}
