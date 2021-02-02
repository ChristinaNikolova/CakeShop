namespace CakeShop.Web.Areas.Identity.Pages.Account.InputModels
{
    using System.ComponentModel.DataAnnotations;

    using CakeShop.Common;
    using Microsoft.AspNetCore.Http;

    public class IndexInputModel
    {
        public string Email { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(DataValidation.UserFirstNameMaxLenght, ErrorMessage = GlobalConstants.ErrorMessage, MinimumLength = DataValidation.UserFirstNameMinLenght)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(DataValidation.UserLastNameMaxLenght, ErrorMessage = GlobalConstants.ErrorMessage, MinimumLength = DataValidation.UserLastNameMinLenght)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Address")]
        [StringLength(DataValidation.UserAddressMaxLenght, ErrorMessage = GlobalConstants.ErrorMessage, MinimumLength = DataValidation.UserAddressMinLenght)]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Phone")]
        [Phone]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "New Picture")]
        public IFormFile NewPicture { get; set; }
    }
}
