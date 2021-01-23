using CakeShop.Services.Data.Desserts;
using CakeShop.Web.ViewModels.Administration.Desserts.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CakeShop.Web.Areas.Administration.Controllers
{
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
    }
}
