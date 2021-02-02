namespace CakeShop.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using CakeShop.Common;
    using CakeShop.Data.Models;
    using CakeShop.Services.Data.Categories;
    using CakeShop.Services.Data.DessertLikes;
    using CakeShop.Services.Data.Desserts;
    using CakeShop.Services.Data.Orders;
    using CakeShop.Web.ViewModels.Categories.ViewModels;
    using CakeShop.Web.ViewModels.Desserts.InputModels;
    using CakeShop.Web.ViewModels.Desserts.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ShopController : BaseController
    {
        private readonly ICategoriesService categoriesService;
        private readonly IDessertsService dessertsService;
        private readonly IOrdersService ordersService;
        private readonly IDessertLikesService dessertLikesService;
        private readonly UserManager<ApplicationUser> userManager;

        public ShopController(
            ICategoriesService categoriesService,
            IDessertsService dessertsService,
            IOrdersService ordersService,
            IDessertLikesService dessertLikesService,
            UserManager<ApplicationUser> userManager)
        {
            this.categoriesService = categoriesService;
            this.dessertsService = dessertsService;
            this.ordersService = ordersService;
            this.dessertLikesService = dessertLikesService;
            this.userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> GetAllCategories()
        {
            var model = new AllCategoriesViewModel()
            {
                Categories = await this.categoriesService.GetAllAsync<CategoryViewModel>(),
            };

            return this.View(model);
        }

        [AllowAnonymous]
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
            model.IsFavourite = await this.dessertLikesService.IsFavouriteAsync(id, userId);

            return this.View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<OrderDessertsViewModel>> OrderByCriteria([FromBody] OrderDessertsInputModel input)
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
    }
}
