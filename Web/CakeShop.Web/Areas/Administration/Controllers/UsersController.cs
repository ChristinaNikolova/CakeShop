namespace CakeShop.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using CakeShop.Services.Data.Users;
    using CakeShop.Web.ViewModels.Administration.Users.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class UsersController : AdministrationController
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public async Task<IActionResult> GetAll()
        {
            var model = new AllUsersAdminInputModel()
            {
                Users = await this.usersService.GetAllAsync<UserAdminViewModel>(),
            };

            return this.View(model);
        }
    }
}
