namespace CakeShop.Web.ViewModels.Comments.InputModels
{
    using System.ComponentModel.DataAnnotations;

    using CakeShop.Common;

    public class AddCommentInputModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Comment")]
        [StringLength(DataValidation.CommentContentMaxLenght, ErrorMessage = GlobalConstants.ErrorMessage, MinimumLength = DataValidation.CommentContentMinLenght)]
        public string Content { get; set; }
    }
}
