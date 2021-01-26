namespace CakeShop.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;
    using CakeShop.Common;
    using CakeShop.Services.Data.Ingredients;
    using CakeShop.Web.ViewModels.Administration.Ingredients.InputModels;
    using CakeShop.Web.ViewModels.Administration.Ingredients.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class IngredientsController : AdministrationController
    {
        private readonly IIngredientsService ingredientsService;

        public IngredientsController(IIngredientsService ingredientsService)
        {
            this.ingredientsService = ingredientsService;
        }

        public async Task<IActionResult> GetAll()
        {
            var model = new AllIngredientsAdminInputModel()
            {
                Ingredients = await this.ingredientsService.GetAllAsync<IngredientAdminViewModel>(),
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AllIngredientsAdminInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.Ingredients = await this.ingredientsService.GetAllAsync<IngredientAdminViewModel>();

                return this.View(input);
            }

            var isAdded = await this.ingredientsService.AddAsync(input.Name);

            if (isAdded)
            {
                this.TempData["InfoMessage"] = GlobalConstants.SuccessAddedMessage;
            }
            else
            {
                this.TempData["ErrorMessage"] = GlobalConstants.AlreadyExistingIngredient;
            }

            return this.RedirectToAction(nameof(this.GetAll));
        }

        public async Task<IActionResult> Update(string id)
        {
            var model = await this.ingredientsService.GetDetailsForUpdateAsync<UpdateIngredientInputModel>(id);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateIngredientInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.ingredientsService.UpdateAsync(input.Id, input.Name);

            this.TempData["InfoMessage"] = GlobalConstants.SuccessUpdateMessage;

            return this.RedirectToAction(nameof(this.GetAll));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await this.ingredientsService.DeleteAsync(id);

            this.TempData["InfoMessage"] = GlobalConstants.SuccessDeleteMessage;

            return this.RedirectToAction(nameof(this.GetAll));
        }
    }
}
