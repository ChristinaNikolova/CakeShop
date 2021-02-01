namespace CakeShop.Web.ViewModels.Reviews.InputModels
{
    using System.ComponentModel.DataAnnotations;

    using CakeShop.Common;
    using CakeShop.Web.Infrastructure.ValidationAttributes;
    using CakeShop.Web.ViewModels.Desserts.ViewModels;

    public class AddReviewInputModel
    {
        [Required]
        [StringLength(DataValidation.ReviewContentMaxLenght, ErrorMessage = GlobalConstants.ErrorMessage, MinimumLength = DataValidation.ReviewContentMinLenght)]
        public string Content { get; set; }

        [Range(typeof(int), DataValidation.ReviewMinPoints, DataValidation.ReviewMaxPoints)]
        [Display(Name = "Stars")]
        public int Points { get; set; }

        public DessertAddReviewViewModel Dessert { get; set; }

        public string OrderId { get; set; }
    }
}
