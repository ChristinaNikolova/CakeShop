namespace CakeShop.Web.Controllers
{
    using System.Threading.Tasks;

    using CakeShop.Common;
    using CakeShop.Data.Models;
    using CakeShop.Services.Data.DessertOrders;
    using CakeShop.Services.Data.Desserts;
    using CakeShop.Services.Data.Reviews;
    using CakeShop.Web.ViewModels.DessertOrders.ViewModels;
    using CakeShop.Web.ViewModels.Desserts.ViewModels;
    using CakeShop.Web.ViewModels.Reviews.InputModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ReviewsController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDessertsService dessertsService;
        private readonly IReviewsService reviewsService;
        private readonly IDessertOrdersService dessertOrdersService;

        public ReviewsController(
            UserManager<ApplicationUser> userManager,
            IDessertsService dessertsService,
            IReviewsService reviewsService,
            IDessertOrdersService dessertOrdersService)
        {
            this.userManager = userManager;
            this.dessertsService = dessertsService;
            this.reviewsService = reviewsService;
            this.dessertOrdersService = dessertOrdersService;
        }

        public async Task<IActionResult> GetAll()
        {
            var userId = this.userManager.GetUserId(this.User);

            var model = new AllWaitingForReviewDessertsViewModel()
            {
                Desserts = await this.dessertOrdersService.GetDessertsForReviewAsync<WaitingForReviewDessertViewModel>(userId),
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

        [HttpPost]
        public async Task<IActionResult> Add(AddReviewInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.Dessert = await this.dessertsService.GetDetailsAsync<DessertAddReviewViewModel>(input.Dessert.Id);

                return this.View(input);
            }

            var userId = this.userManager.GetUserId(this.User);

            await this.reviewsService.AddAsync(input.Content, input.Points, input.OrderId, input.Dessert.Id, userId);

            this.TempData["InfoMessage"] = GlobalConstants.SuccessAddedMessage;

            return this.Redirect($"/Orders/GetOrderDetails/{input.OrderId}");
        }
    }
}
