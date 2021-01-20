namespace CakeShop.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using CakeShop.Common;
    using CakeShop.Data.Models;
    using CakeShop.Services.Data.Categories;
    using CakeShop.Services.Data.Desserts;
    using CakeShop.Services.Data.Orders;
    using CakeShop.Web.ViewModels.Categories.ViewModels;
    using CakeShop.Web.ViewModels.Desserts.InputModels;
    using CakeShop.Web.ViewModels.Desserts.ViewModels;
    using CakeShop.Web.ViewModels.Orders.ViewModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ShopController : BaseController
    {
        private readonly ICategoriesService categoriesService;
        private readonly IDessertsService dessertsService;
        private readonly IOrdersService ordersService;
        private readonly UserManager<ApplicationUser> userManager;

        public ShopController(
            ICategoriesService categoriesService,
            IDessertsService dessertsService,
            IOrdersService ordersService,
            UserManager<ApplicationUser> userManager)
        {
            this.categoriesService = categoriesService;
            this.dessertsService = dessertsService;
            this.ordersService = ordersService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> GetAllCategories()
        {
            var model = new AllCategoriesViewModel()
            {
                Categories = await this.categoriesService.GetAllAsync<CategoryViewModel>(),
            };

            return this.View(model);
        }

        public async Task<IActionResult> GetAllCurrentCategory(string id, int currentPage = 1)
        {
            var dessertsCount = await this.dessertsService.GetTotalCountDessertsByCategoryAsync(id);

            var pageCount = (int)Math.Ceiling((double)dessertsCount / GlobalConstants.DessertsPerPage);

            var model = new AllDessertsByCategoryViewModel()
            {
                Desserts = await this.dessertsService.GetAllCurrentCategoryAsync<DessertViewModel>(id, GlobalConstants.DessertsPerPage, (currentPage - 1) * GlobalConstants.DessertsPerPage),
                CurrentPage = currentPage,
                PagesCount = pageCount,
            };

            return this.View(model);
        }

        public async Task<IActionResult> DessertDetails(string id)
        {
            var userId = this.userManager.GetUserId(this.User);

            var model = await this.dessertsService.GetDetailsAsync<DessertDetailsViewModel>(id);
            model.IsFavourite = await this.dessertsService.IsFavouriteAsync(id, userId);

            return this.View(model);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDessertsViewModel>> Order([FromBody] OrderDessertsInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.GetAllCurrentCategory), new { id = input.CategoryId });
            }

            var orderedDesserts = await this.dessertsService.OrderDessertsAsync<DessertViewModel>(input.TargetCriteria, input.CategoryId);

            return new OrderDessertsViewModel
            {
                OrderedDesserts = orderedDesserts,
                TargetCriteria = input.TargetCriteria,
            };
        }

        //orderController
        public async Task<IActionResult> SeeBasket()
        {
            var userId = this.userManager.GetUserId(this.User);

            var model = new AllDessertsBasketViewModel()
            {
                DessertsInBasket = await this.ordersService.GetDessertsInBasketAsync<DessertBasketViewModel>(userId),
            };

            return this.View(model);
        }
    }
}
