namespace CakeShop.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using CakeShop.Services.Data.Ingredients;
    using Microsoft.AspNetCore.Mvc;

    public class IngredientsController : AdministrationController
    {
        private readonly IIngredientsService ingredientsService;

        public IngredientsController(IIngredientsService ingredientsService)
        {
            this.ingredientsService = ingredientsService;
        }

        //public async Task<IActionResult> GetAll()
        //{
        //    var model = new AllDessertsAdminViewModel()
        //    {
        //        Desserts = await this.dessertsService.GetAllAsync<DessertAdminViewModel>(),
        //    };

        //    return this.View(model);
        //}
    }
}
