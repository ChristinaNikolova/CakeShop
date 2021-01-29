namespace CakeShop.Web.ViewComponents
{
    using System.Threading.Tasks;

    using CakeShop.Services.Data.Comments;
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent]
    public class GetCommentsCurrentRecipeViewComponent : ViewComponent
    {
        private readonly ICommentsService commentsService;

        public GetCommentsCurrentRecipeViewComponent(ICommentsService commentsService)
        {
            this.commentsService = commentsService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string recipeId)
        {
            //var model = new AllCommentsCurrentRecipeViewModel()
            //{
            //    Comments = await this.commentsService.GetCommentsCurrentRecipeAsync<CommentViewModel>(recipeId),
            //};

            return this.View();
        }
    }
}
