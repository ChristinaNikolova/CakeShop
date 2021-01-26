namespace CakeShop.Web.ViewModels.Administration.Categories.InputModels
{
    using System.ComponentModel.DataAnnotations;

    using CakeShop.Common;
    using Microsoft.AspNetCore.Http;

    public class AddCategoryInputModel
    {
        [Required]
        [StringLength(DataValidation.CategotyNameMaxLenght, ErrorMessage = GlobalConstants.ErrorMessage, MinimumLength = DataValidation.CategotyNameMinLenght)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        public IFormFile Picture { get; set; }

        [Required]
        [StringLength(DataValidation.CategotyDescriptionMaxLenght, ErrorMessage = GlobalConstants.ErrorMessage, MinimumLength = DataValidation.CategotyDescriptionMinLenght)]
        public string Description { get; set; }
    }
}
