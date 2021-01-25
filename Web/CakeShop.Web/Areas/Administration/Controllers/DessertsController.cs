namespace CakeShop.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using CakeShop.Common;
    using CakeShop.Services.Data.Categories;
    using CakeShop.Services.Data.DessertIngredients;
    using CakeShop.Services.Data.Desserts;
    using CakeShop.Services.Data.DessertTags;
    using CakeShop.Web.ViewModels.Administration.DessertIngredients.InputModel;
    using CakeShop.Web.ViewModels.Administration.DessertIngredients.InputModels;
    using CakeShop.Web.ViewModels.Administration.DessertIngredients.ViewModels;
    using CakeShop.Web.ViewModels.Administration.Desserts.InputModels;
    using CakeShop.Web.ViewModels.Administration.Desserts.ViewModels;
    using CakeShop.Web.ViewModels.Administration.DessertTags.InputModels;
    using CakeShop.Web.ViewModels.Administration.DessertTags.ViewModels;
    using CakeShop.Web.ViewModels.Desserts.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class DessertsController : AdministrationController
    {
        private readonly IDessertsService dessertsService;
        private readonly ICategoriesService categoriesService;
        private readonly IDessertIngredientsService dessertIngredientsService;
        private readonly IDessertTagsService dessertTagsService;

        public DessertsController(
            IDessertsService dessertsService,
            ICategoriesService categoriesService,
            IDessertIngredientsService dessertIngredientsService,
            IDessertTagsService dessertTagsService)
        {
            this.dessertsService = dessertsService;
            this.categoriesService = categoriesService;
            this.dessertIngredientsService = dessertIngredientsService;
            this.dessertTagsService = dessertTagsService;
        }

        public async Task<IActionResult> GetAll()
        {
            var model = new AllDessertsAdminViewModel()
            {
                Desserts = await this.dessertsService.GetAllAsync<DessertAdminViewModel>(),
            };

            return this.View(model);
        }

        public async Task<IActionResult> Add()
        {
            var model = new AddDessertInputModel()
            {
                Categories = await this.categoriesService.GetAllAsSelectListItemAsync(),
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddDessertInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.Categories = await this.categoriesService.GetAllAsSelectListItemAsync();

                return this.View(input);
            }

            await this.dessertsService.AddAsync(input.Name, input.Picture, input.Price, input.Description, input.CategoryId);

            this.TempData["InfoMessage"] = GlobalConstants.SuccessAddedMessage;

            return this.RedirectToAction(nameof(this.GetAll));
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

        public async Task<IActionResult> AddIngredientToDessert(UpdateDessertIngredientsInputModel input)
        {
            var dessertId = input.Dessert.Id;

            if (!this.ModelState.IsValid)
            {
                input.DessertIngredients = await this.dessertIngredientsService.GetAllCurrentDessertAsync<DessertIngredientViewModel>(dessertId);
                input.Dessert = await this.dessertsService.GetDetailsAsync<DessertViewModel>(dessertId);

                return this.View(input);
            }

            var isAdded = await this.dessertIngredientsService.AddIngredientToDessertAsync(dessertId, input.Name);

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

        [HttpPost]
        public async Task<ActionResult<AllDessertIngredientsViewModel>> RemoveIngredientFromDessert([FromBody] RemoveIngredientFromDessertInputModel input)
        {
            await this.dessertIngredientsService.RemoveIngredientFromDessertAsync(input.DessertId, input.IngredientName);

            var dessertIngredients = await this.dessertIngredientsService.GetAllCurrentDessertAsync<DessertIngredientViewModel>(input.DessertId);

            return new AllDessertIngredientsViewModel { DessertIngredients = dessertIngredients };
        }

        public async Task<IActionResult> UpdateDessertTags(string id)
        {
            var model = new UpdateDessertTagsInputModel()
            {
                DessertTags = await this.dessertTagsService.GetAllCurrentDessertAsync<DessertTagViewModel>(id),
                Dessert = await this.dessertsService.GetDetailsAsync<DessertViewModel>(id),
            };

            return this.View(model);
        }

        public async Task<IActionResult> AddTagToDessert(UpdateDessertTagsInputModel input)
        {
            var dessertId = input.Dessert.Id;

            if (!this.ModelState.IsValid)
            {
                input.DessertTags = await this.dessertTagsService.GetAllCurrentDessertAsync<DessertTagViewModel>(dessertId);
                input.Dessert = await this.dessertsService.GetDetailsAsync<DessertViewModel>(dessertId);

                return this.View(input);
            }

            var isAdded = await this.dessertTagsService.AddTagToDessertAsync(dessertId, input.Name);

            if (!isAdded)
            {
                this.TempData["ErrorMessage"] = GlobalConstants.ProblemWithAddingTag;
            }
            else
            {
                this.TempData["InfoMessage"] = GlobalConstants.SuccessAddedMessage;
            }

            return this.RedirectToAction(nameof(this.UpdateDessertTags), new { Id = dessertId });
        }

        [HttpPost]
        public async Task<ActionResult<AllDessertTagsViewModel>> RemoveTagFromDessert([FromBody] RemoveTagFromDessertInputModel input)
        {
            await this.dessertTagsService.RemoveTagFromDessertAsync(input.DessertId, input.TagName);

            var dessertTags = await this.dessertTagsService.GetAllCurrentDessertAsync<DessertTagViewModel>(input.DessertId);

            return new AllDessertTagsViewModel { DessertTags = dessertTags };
        }
    }
}
