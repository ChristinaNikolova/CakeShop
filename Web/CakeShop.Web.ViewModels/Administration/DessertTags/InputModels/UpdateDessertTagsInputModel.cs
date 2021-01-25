namespace CakeShop.Web.ViewModels.Administration.DessertTags.InputModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CakeShop.Common;
    using CakeShop.Web.ViewModels.Administration.DessertTags.ViewModels;
    using CakeShop.Web.ViewModels.Desserts.ViewModels;

    public class UpdateDessertTagsInputModel
    {
        public IEnumerable<DessertTagViewModel> DessertTags { get; set; }

        public DessertViewModel Dessert { get; set; }

        [Required]
        [StringLength(DataValidation.TagNameMaxLenght, ErrorMessage = GlobalConstants.ErrorMessage, MinimumLength = DataValidation.TagNameMinLenght)]
        public string Name { get; set; }
    }
}
