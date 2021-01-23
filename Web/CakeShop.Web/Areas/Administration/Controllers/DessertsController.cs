namespace CakeShop.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;
    using CakeShop.Common;
    using CakeShop.Services.Data.Desserts;
    using CakeShop.Web.ViewModels.Administration.Desserts.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class DessertsController : AdministrationController
    {
        private readonly IDessertsService dessertsService;

        public DessertsController(IDessertsService dessertsService)
        {
            this.dessertsService = dessertsService;
        }

        public async Task<IActionResult> GetAll()
        {
            var model = new AllDessertsAdminViewModel()
            {
                Desserts = await this.dessertsService.GetAllAsync<DessertAdminViewModel>(),
            };

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
