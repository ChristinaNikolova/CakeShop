namespace CakeShop.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using CakeShop.Common;
    using CakeShop.Services.Data.Categories;
    using CakeShop.Services.Data.Recipes;
    using CakeShop.Web.ViewModels.Administration.Recipes.InputModels;
    using CakeShop.Web.ViewModels.Administration.Recipes.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class RecipesController : AdministrationController
    {
        private readonly IRecipesService recipesService;
        private readonly ICategoriesService categoriesService;

        public RecipesController(
            IRecipesService recipesService,
            ICategoriesService categoriesService)
        {
            this.recipesService = recipesService;
            this.categoriesService = categoriesService;
        }

        public async Task<IActionResult> GetAll()
        {
            var model = new AllRecipesAdminViewModel()
            {
                Recipes = await this.recipesService.GetAllAsync<RecipeAdminViewModel>(),
            };

            return this.View(model);
        }

        public async Task<IActionResult> Add()
        {
            var model = new AddRecipeInputModel()
            {
                Categories = await this.categoriesService.GetAllAsSelectListItemAsync(),
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRecipeInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.Categories = await this.categoriesService.GetAllAsSelectListItemAsync();

                return this.View(input);
            }

            await this.recipesService.AddAsync(input.Title, input.Content, input.Picture, input.Portions, input.PreparationTime, input.CookingTime, input.CategoryId);

            this.TempData["InfoMessage"] = GlobalConstants.SuccessAddedMessage;

            return this.RedirectToAction(nameof(this.GetAll));
        }

        public async Task<IActionResult> Update(string id)
        {
            var model = await this.recipesService.GetDetailsForUpdateAsync<UpdateRecipeInputModel>(id);
            model.Categories = await this.categoriesService.GetAllAsSelectListItemAsync();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateRecipeInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.Categories = await this.categoriesService.GetAllAsSelectListItemAsync();
                input.Picture = await this.recipesService.GetPictureAsync(input.Id);

                return this.View(input);
            }

            await this.recipesService.UpdateAsync(input.Id, input.Title, input.Content, input.NewPicture, input.Portions, input.CookingTime, input.PreparationTime, input.CategoryId);

            this.TempData["InfoMessage"] = GlobalConstants.SuccessUpdateMessage;

            return this.RedirectToAction(nameof(this.GetAll));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await this.recipesService.DeleteAsync(id);

            this.TempData["InfoMessage"] = GlobalConstants.SuccessDeleteMessage;

            return this.RedirectToAction(nameof(this.GetAll));
        }
    }
}
