namespace CakeShop.Web.Controllers
{
    using System.Threading.Tasks;

    using CakeShop.Services.Data.Categories;
    using CakeShop.Web.ViewModels.Categories.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class ShopController : BaseController
    {
        private readonly ICategoriesService categoriesService;

        public ShopController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public async Task<IActionResult> GetAllCategories()
        {
            var model = new AllCategoriesViewModel()
            {
                Categories = await this.categoriesService.GetAllAsync<CategoryViewModel>(),
            };

            return this.View(model);
        }
    }
}
