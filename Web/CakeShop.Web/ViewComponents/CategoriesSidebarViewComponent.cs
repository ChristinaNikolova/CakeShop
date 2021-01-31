namespace CakeShop.Web.ViewComponents
{
    using System.Threading.Tasks;

    using CakeShop.Services.Data.Categories;
    using CakeShop.Web.ViewModels.Categories.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent]
    public class CategoriesSidebarViewComponent : ViewComponent
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesSidebarViewComponent(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new AllCategorySidebarViewModel()
            {
                Categories = await this.categoriesService.GetAllAsync<CategorySidebarViewModel>(),
            };

            return this.View(model);
        }
    }
}
