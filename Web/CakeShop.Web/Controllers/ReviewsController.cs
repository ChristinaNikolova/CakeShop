namespace CakeShop.Web.Controllers
{
    using System.Threading.Tasks;

    using CakeShop.Data.Models;
    using CakeShop.Services.Data.Desserts;
    using CakeShop.Services.Data.Orders;
    using CakeShop.Web.ViewModels.DessertOrders.ViewModels;
    using CakeShop.Web.ViewModels.Desserts.ViewModels;
    using CakeShop.Web.ViewModels.Reviews.InputModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ReviewsController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IOrdersService ordersService;
        private readonly IDessertsService dessertsService;

        public ReviewsController(
            UserManager<ApplicationUser> userManager,
            IOrdersService ordersService,
            IDessertsService dessertsService)
        {
            this.userManager = userManager;
            this.ordersService = ordersService;
            this.dessertsService = dessertsService;
        }

        public async Task<IActionResult> GetAll()
        {
            var userId = this.userManager.GetUserId(this.User);

            var model = new AllWaitingForReviewDessertsViewModel()
            {
                Desserts = await this.ordersService.GetDessertsForReviewAsync<WaitingForReviewDessertViewModel>(userId),
            };

            return this.View(model);
        }

        public async Task<IActionResult> Add(string dessertId, string orderId)
        {
            var model = new AddReviewInputModel()
            {
                Dessert = await this.dessertsService.GetDetailsAsync<DessertAddReviewViewModel>(dessertId),
                OrderId = orderId,
            };

            return this.View(model);
        }

        //[HttpPost]
        //public async Task<IActionResult> Add(AddDessertInputModel input)
        //{
        //    if (!this.ModelState.IsValid)
        //    {
        //        input.Categories = await this.categoriesService.GetAllAsSelectListItemAsync();

        //        return this.View(input);
        //    }

        //    await this.dessertsService.AddAsync(input.Name, input.Picture, input.Price, input.Description, input.CategoryId);

        //    this.TempData["InfoMessage"] = GlobalConstants.SuccessAddedMessage;

        //    return this.RedirectToAction(nameof(this.GetAll));
        //}
    }
}
