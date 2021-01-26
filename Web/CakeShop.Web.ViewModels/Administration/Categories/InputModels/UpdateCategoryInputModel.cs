namespace CakeShop.Web.ViewModels.Administration.Categories.InputModels
{
    using System.ComponentModel.DataAnnotations;

    using CakeShop.Common;
    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class UpdateCategoryInputModel : IMapFrom<Category>
    {
        public string Id { get; set; }

        [Required]
        [StringLength(DataValidation.CategotyNameMaxLenght, ErrorMessage = GlobalConstants.ErrorMessage, MinimumLength = DataValidation.CategotyNameMinLenght)]
        public string Name { get; set; }

        public string Picture { get; set; }

        [Display(Name = "New Picture")]
        [DataType(DataType.Upload)]
        public IFormFile NewPicture { get; set; }

        [Required]
        [StringLength(DataValidation.CategotyDescriptionMaxLenght, ErrorMessage = GlobalConstants.ErrorMessage, MinimumLength = DataValidation.CategotyDescriptionMinLenght)]
        public string Description { get; set; }
    }
}
