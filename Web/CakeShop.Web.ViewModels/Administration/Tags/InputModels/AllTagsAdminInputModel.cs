namespace CakeShop.Web.ViewModels.Administration.Tags.InputModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CakeShop.Common;
    using CakeShop.Web.ViewModels.Administration.Tags.ViewModels;

    public class AllTagsAdminInputModel
    {
        public IEnumerable<TagAdminViewModel> Tags { get; set; }

        [Required]
        [StringLength(DataValidation.TagNameMaxLenght, ErrorMessage = GlobalConstants.ErrorMessage, MinimumLength = DataValidation.TagNameMinLenght)]
        public string Name { get; set; }
    }
}
