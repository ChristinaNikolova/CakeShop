namespace CakeShop.Web.ViewModels.Administration.Comments.ViewModels
{
    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;
    using CakeShop.Web.ViewModels.Comments.ViewModels;

    public class CommentAdminViewModel : CommentViewModel, IMapFrom<Comment>
    {
        public string Id { get; set; }

        public string RecipeId { get; set; }

        public string RecipeTitle { get; set; }
    }
}
