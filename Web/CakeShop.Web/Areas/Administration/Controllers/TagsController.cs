namespace CakeShop.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using CakeShop.Common;
    using CakeShop.Services.Data.Tags;
    using CakeShop.Web.ViewModels.Administration.Tags.InputModels;
    using CakeShop.Web.ViewModels.Administration.Tags.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class TagsController : AdministrationController
    {
        private readonly ITagsService tagsService;

        public TagsController(ITagsService tagsService)
        {
            this.tagsService = tagsService;
        }

        public async Task<IActionResult> GetAll()
        {
            var model = new AllTagsAdminInputModel()
            {
                Tags = await this.tagsService.GetAllAsync<TagAdminViewModel>(),
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AllTagsAdminInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.Tags = await this.tagsService.GetAllAsync<TagAdminViewModel>();

                return this.View(input);
            }

            var isAdded = await this.tagsService.AddAsync(input.Name);

            if (isAdded)
            {
                this.TempData["InfoMessage"] = GlobalConstants.SuccessAddedMessage;
            }
            else
            {
                this.TempData["ErrorMessage"] = GlobalConstants.AlreadyExistingTag;
            }

            return this.RedirectToAction(nameof(this.GetAll));
        }

        public async Task<IActionResult> Update(string id)
        {
            var model = await this.tagsService.GetDetailsForUpdateAsync<UpdateTagInputModel>(id);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateTagInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.tagsService.UpdateAsync(input.Id, input.Name);

            this.TempData["InfoMessage"] = GlobalConstants.SuccessUpdateMessage;

            return this.RedirectToAction(nameof(this.GetAll));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await this.tagsService.DeleteAsync(id);

            this.TempData["InfoMessage"] = GlobalConstants.SuccessDeleteMessage;

            return this.RedirectToAction(nameof(this.GetAll));
        }
    }
}
