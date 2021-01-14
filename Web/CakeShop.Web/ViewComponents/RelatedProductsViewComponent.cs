namespace CakeShop.Web.ViewComponents
{
    using System.Threading.Tasks;

    using CakeShop.Services.Data.Desserts;
    using CakeShop.Web.ViewModels.Desserts.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent]
    public class RelatedProductsViewComponent : ViewComponent
    {
        private readonly IDessertsService dessertsService;

        public RelatedProductsViewComponent(IDessertsService dessertsService)
        {
            this.dessertsService = dessertsService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string categoryId, string[] tagTagNames)
        {
            var model = new AllDessertsByCategoryViewModel()
            {
                Desserts = await this.dessertsService.GetAllWithCurrentTagsAsync<DessertViewModel>(categoryId, tagTagNames),
            };

            return this.View(model);
        }
    }
}
