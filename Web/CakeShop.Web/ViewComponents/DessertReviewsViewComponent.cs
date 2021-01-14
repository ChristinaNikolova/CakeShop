namespace CakeShop.Web.ViewComponents
{
    using System.Threading.Tasks;

    using CakeShop.Services.Data.Reviews;
    using CakeShop.Web.ViewModels.Reviews.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent]
    public class DessertReviewsViewComponent : ViewComponent
    {
        private readonly IReviewsService reviewsService;

        public DessertReviewsViewComponent(IReviewsService reviewsService)
        {
            this.reviewsService = reviewsService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string dessertId)
        {
            var model = new AllReviewsViewModel()
            {
                Reviews = await this.reviewsService.GetReviewsCurrentDessertAsync<ReviewViewModel>(dessertId),
            };

            return this.View(model);
        }
    }
}
