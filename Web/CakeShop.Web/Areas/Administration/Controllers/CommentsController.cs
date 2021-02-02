namespace CakeShop.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using CakeShop.Common;
    using CakeShop.Services.Data.Comments;
    using CakeShop.Web.ViewModels.Administration.Comments.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class CommentsController : AdministrationController
    {
        private readonly ICommentsService commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            this.commentsService = commentsService;
        }

        public async Task<IActionResult> GetAll()
        {
            var model = new AllCommentsAdminViewModel()
            {
                Comments = await this.commentsService.GetAllUnapprovedAsync<CommentAdminViewModel>(),
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await this.commentsService.DeleteAsync(id);

            this.TempData["InfoMessage"] = GlobalConstants.SuccessDeleteMessage;

            return this.RedirectToAction(nameof(this.GetAll));
        }

        [HttpPost]
        public async Task<IActionResult> Approve(string id)
        {
            await this.commentsService.ApproveAsync(id);

            this.TempData["InfoMessage"] = GlobalConstants.SuccessUpdateMessage;

            return this.RedirectToAction(nameof(this.GetAll));
        }
    }
}
