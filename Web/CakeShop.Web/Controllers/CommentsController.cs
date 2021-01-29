namespace CakeShop.Web.Controllers
{
    using System.Threading.Tasks;

    using CakeShop.Data.Models;
    using CakeShop.Services.Data.Comments;
    using CakeShop.Web.ViewModels.Comments.InputModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class CommentsController : BaseController
    {
        private readonly ICommentsService commentsService;
        private readonly UserManager<ApplicationUser> userManager;

        public CommentsController(
            ICommentsService commentsService,
            UserManager<ApplicationUser> userManager)
        {
            this.commentsService = commentsService;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCommentInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect($"/Recipes/GetRecipeDetails/{input.Id}");
            }

            var userId = this.userManager.GetUserId(this.User);

            await this.commentsService.AddAsync(input.Id, input.Content, userId);

            return this.Redirect($"/Recipes/GetRecipeDetails/{input.Id}");
        }
    }
}
