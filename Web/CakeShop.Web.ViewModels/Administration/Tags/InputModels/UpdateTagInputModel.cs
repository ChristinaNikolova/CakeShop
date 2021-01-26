namespace CakeShop.Web.ViewModels.Administration.Tags.InputModels
{
    using System.ComponentModel.DataAnnotations;

    using CakeShop.Common;
    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;

    public class UpdateTagInputModel : IMapFrom<Tag>
    {
        public string Id { get; set; }

        [Required]
        [StringLength(DataValidation.TagNameMaxLenght, ErrorMessage = GlobalConstants.ErrorMessage, MinimumLength = DataValidation.TagNameMinLenght)]
        public string Name { get; set; }
    }
}
