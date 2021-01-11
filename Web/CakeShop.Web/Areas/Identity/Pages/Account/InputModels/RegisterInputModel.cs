namespace CakeShop.Web.Areas.Identity.Pages.Account.InputModels
{
    using System.ComponentModel.DataAnnotations;

    using CakeShop.Common;

    public class RegisterInputModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Username")]
        [StringLength(DataValidation.UserFirstNameMaxLenght, ErrorMessage = GlobalConstants.ErrorMessage, MinimumLength = DataValidation.UserFirstNameMinLenght)]
        public string UserName { get; set; }

        [Required]
        [StringLength(DataValidation.UserPasswordMaxLenght, ErrorMessage = GlobalConstants.ErrorMessage, MinimumLength = DataValidation.UserPasswordMinLenght)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = GlobalConstants.ErrorMessagePasswords)]
        public string ConfirmPassword { get; set; }

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
    }
}
