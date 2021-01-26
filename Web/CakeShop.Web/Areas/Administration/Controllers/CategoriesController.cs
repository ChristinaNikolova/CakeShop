namespace CakeShop.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using CakeShop.Common;
    using CakeShop.Services.Data.Categories;
    using CakeShop.Web.ViewModels.Administration.Categories.InputModels;
    using CakeShop.Web.ViewModels.Administration.Categories.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesController : AdministrationController
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public async Task<IActionResult> GetAll()
        {
            var model = new AllCategoriesAdminViewModel()
            {
                Categories = await this.categoriesService.GetAllAsync<CategoryAdminViewModel>(),
            };

            return this.View(model);
        }

        public IActionResult Add()
        {
            var model = new AddCategoryInputModel();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCategoryInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var isAdded = await this.categoriesService.AddAsync(input.Name, input.Picture, input.Description);

            if (isAdded)
            {
                this.TempData["InfoMessage"] = GlobalConstants.SuccessAddedMessage;
            }
            else
            {
                this.TempData["ErrorMessage"] = GlobalConstants.AlreadyExistingCategory;
            }

            return this.RedirectToAction(nameof(this.GetAll));
        }

        public async Task<IActionResult> Update(string id)
        {
            var model = await this.categoriesService.GetDetailsForUpdateAsync<UpdateCategoryInputModel>(id);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateCategoryInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.Picture = await this.categoriesService.GetPictureAsync(input.Id);

                return this.View(input);
            }

            await this.categoriesService.UpdateAsync(input.Id, input.Name, input.NewPicture, input.Description);

            this.TempData["InfoMessage"] = GlobalConstants.SuccessUpdateMessage;

            return this.RedirectToAction(nameof(this.GetAll));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await this.categoriesService.DeleteAsync(id);

            this.TempData["InfoMessage"] = GlobalConstants.SuccessDeleteMessage;

            return this.RedirectToAction(nameof(this.GetAll));
        }
    }
}
