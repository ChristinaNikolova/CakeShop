namespace CakeShop.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using CakeShop.Common;
    using CakeShop.Services.Data.Categories;
    using CakeShop.Services.Data.Desserts;
    using CakeShop.Web.ViewModels.Administration.Desserts.InputModels;
    using CakeShop.Web.ViewModels.Administration.Desserts.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class DessertsController : AdministrationController
    {
        private readonly IDessertsService dessertsService;
        private readonly ICategoriesService categoriesService;

        public DessertsController(
            IDessertsService dessertsService,
            ICategoriesService categoriesService)
        {
            this.dessertsService = dessertsService;
            this.categoriesService = categoriesService;
        }

        public async Task<IActionResult> GetAll()
        {
            var model = new AllDessertsAdminViewModel()
            {
                Desserts = await this.dessertsService.GetAllAsync<DessertAdminViewModel>(),
            };

            return this.View(model);
        }

        public async Task<IActionResult> Update(string id)
        {
            var model = await this.dessertsService.GetDetailsForUpdateAsync<UpdateDessertInputModel>(id);
            model.Categories = await this.categoriesService.GetAllAsSelectListItemAsync();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await this.dessertsService.DeleteAsync(id);

            this.TempData["InfoMessage"] = GlobalConstants.SuccessDeleteMessage;

            return this.RedirectToAction(nameof(this.GetAll));
        }
    }
}
