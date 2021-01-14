namespace CakeShop.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using CakeShop.Common;
    using CakeShop.Services.Data.Categories;
    using CakeShop.Services.Data.Desserts;
    using CakeShop.Web.ViewModels.Categories.ViewModels;
    using CakeShop.Web.ViewModels.Desserts.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class ShopController : BaseController
    {
        private readonly ICategoriesService categoriesService;
        private readonly IDessertsService dessertsService;

        public ShopController(
            ICategoriesService categoriesService,
            IDessertsService dessertsService)
        {
            this.categoriesService = categoriesService;
            this.dessertsService = dessertsService;
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
            var model = await this.dessertsService.GetDetailsAsync<DessertDetailsViewModel>(id);

            return this.View(model);
        }
    }
}
