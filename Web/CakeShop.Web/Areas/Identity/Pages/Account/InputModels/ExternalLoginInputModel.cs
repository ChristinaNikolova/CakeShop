namespace CakeShop.Web.Areas.Identity.Pages.Account.InputModels
{
    using System.ComponentModel.DataAnnotations;

    using CakeShop.Common;

    public class ExternalLoginInputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Address")]
        [StringLength(DataValidation.UserAddressMaxLenght, ErrorMessage = GlobalConstants.ErrorMessage, MinimumLength = DataValidation.UserAddressMinLenght)]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Phone")]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
