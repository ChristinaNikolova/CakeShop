namespace CakeShop.Web.ViewModels.Comments.ViewModels
{
    using System.Collections.Generic;

    public class AllCommentsCurrentRecipeViewModel
    {
        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}
