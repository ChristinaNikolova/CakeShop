namespace CakeShop.Web.Controllers
{
    using System.Threading.Tasks;

    using CakeShop.Data.Models;
    using CakeShop.Services.Data.Desserts;
    using CakeShop.Web.ViewModels.DessertLikes.ViewModels;
    using CakeShop.Web.ViewModels.Desserts.ViewModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class DessertsController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDessertsService dessertsService;

        public DessertsController(
            UserManager<ApplicationUser> userManager,
            IDessertsService dessertsService)
        {
            this.userManager = userManager;
            this.dessertsService = dessertsService;
        }

        [HttpPost]
        public async Task<ActionResult<LikeDessertViewModel>> Like([FromBody] string dessertId)
        {
            var userId = this.userManager.GetUserId(this.User);

            var isAdded = await this.dessertsService.LikeDessertAsync(dessertId, userId);

            return new LikeDessertViewModel { IsAdded = isAdded };
        }

        public async Task<IActionResult> FavouriteDesserts()
        {
            var userId = this.userManager.GetUserId(this.User);

            var model = new AllFavouriteDessertsViewModel()
            {
                FavouriteDesserts = await this.dessertsService.GetUserFavouriteDessertsAsync<DessertViewModel>(userId),
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<ActionResult<AllFavouriteDessertsViewModel>> RemoveFromFavouriteDesserts([FromBody] string dessertId)
        {
            var userId = this.userManager.GetUserId(this.User);

            var favouriteDessets = await this.dessertsService.UnlikeDessertAsync<DessertViewModel>(dessertId, userId);

            return new AllFavouriteDessertsViewModel { FavouriteDesserts = favouriteDessets };
        }
    }
}
