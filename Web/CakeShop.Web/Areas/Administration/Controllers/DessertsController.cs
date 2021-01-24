namespace CakeShop.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using CakeShop.Common;
    using CakeShop.Services.Data.Categories;
    using CakeShop.Services.Data.DessertIngredients;
    using CakeShop.Services.Data.Desserts;
    using CakeShop.Web.ViewModels.Administration.DessertIngredients.InputModels;
    using CakeShop.Web.ViewModels.Administration.DessertIngredients.ViewModels;
    using CakeShop.Web.ViewModels.Administration.Desserts.InputModels;
    using CakeShop.Web.ViewModels.Administration.Desserts.ViewModels;
    using CakeShop.Web.ViewModels.Desserts.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class DessertsController : AdministrationController
    {
        private readonly IDessertsService dessertsService;
        private readonly ICategoriesService categoriesService;
        private readonly IDessertIngredientsService dessertIngredientsService;

        public DessertsController(
            IDessertsService dessertsService,
            ICategoriesService categoriesService,
            IDessertIngredientsService dessertIngredientsService)
        {
            this.dessertsService = dessertsService;
            this.categoriesService = categoriesService;
            this.dessertIngredientsService = dessertIngredientsService;
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
        public async Task<IActionResult> Update(UpdateDessertInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.Categories = await this.categoriesService.GetAllAsSelectListItemAsync();
                input.Picture = await this.dessertsService.GetPictureAsync(input.Id);

                return this.View(input);
            }

            await this.dessertsService.UpdateAsync(input.Id, input.Name, input.Description, input.Price, input.NewPicture, input.CategoryId);

            this.TempData["InfoMessage"] = GlobalConstants.SuccessUpdateMessage;

            return this.RedirectToAction(nameof(this.GetAll));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await this.dessertsService.DeleteAsync(id);

            this.TempData["InfoMessage"] = GlobalConstants.SuccessDeleteMessage;

            return this.RedirectToAction(nameof(this.GetAll));
        }

        public async Task<IActionResult> UpdateDessertIngredients(string id)
        {
            var model = new UpdateDessertIngredientsInputModel()
            {
                DessertIngredients = await this.dessertIngredientsService.GetAllCurrentDessertAsync<DessertIngredientViewModel>(id),
                Dessert = await this.dessertsService.GetDetailsAsync<DessertViewModel>(id),
            };

            return this.View(model);
        }

        public async Task<IActionResult> AddIngredient(UpdateDessertIngredientsInputModel input)
        {
            var dessertId = input.Dessert.Id;
            ;
            if (!this.ModelState.IsValid)
            {
                input.DessertIngredients = await this.dessertIngredientsService.GetAllCurrentDessertAsync<DessertIngredientViewModel>(dessertId);
                input.Dessert = await this.dessertsService.GetDetailsAsync<DessertViewModel>(dessertId);

                return this.View(input);
            }

            var isAdded = await this.dessertIngredientsService.AddAsync(dessertId, input.Name);

            if (!isAdded)
            {
                this.TempData["ErrorMessage"] = GlobalConstants.ProblemWithAddingIngredient;
            }
            else
            {
                this.TempData["InfoMessage"] = GlobalConstants.SuccessAddedMessage;
            }

            return this.RedirectToAction(nameof(this.UpdateDessertIngredients), new { Id = dessertId });
        }


        public async Task<IActionResult> UpdateDessertTags(string id)
        {
            //var model = new AllDessertsAdminViewModel()
            //{
            //    Desserts = await this.dessertsService.GetAllAsync<DessertAdminViewModel>(),
            //};

            //return this.View(model);
            return this.View();
        }
    }
}
