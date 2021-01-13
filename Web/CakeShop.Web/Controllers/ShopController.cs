namespace CakeShop.Web.Controllers
{
    using System.Threading.Tasks;

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

        public async Task<IActionResult> GetAllCurrentCategory(string id)
        {
            var model = new AllDessertsViewModel()
            {
                Desserts = await this.dessertsService.GetAllCurrentCategoryAsync<DessertViewModel>(id),
            };

            return this.View(model);
        }
    }
}
