namespace CakeShop.Web.ViewModels.Administration.Comments.ViewModels
{
    using System.Collections.Generic;

    public class AllCommentsAdminViewModel
    {
        public IEnumerable<CommentAdminViewModel> Comments { get; set; }
    }
}
