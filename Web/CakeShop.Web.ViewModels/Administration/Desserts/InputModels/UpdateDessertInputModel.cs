namespace CakeShop.Web.ViewModels.Administration.Desserts.InputModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CakeShop.Common;
    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;
    using CakeShop.Web.Infrastructure.ValidationAttributes;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class UpdateDessertInputModel : IMapFrom<Dessert>, IMapTo<Dessert>
    {
        public string Id { get; set; }

        [Required]
        [StringLength(DataValidation.DessertNameMaxLenght, ErrorMessage = GlobalConstants.ErrorMessage, MinimumLength = DataValidation.DessertNameMinLenght)]
        public string Name { get; set; }

        [Range(typeof(decimal), DataValidation.DessertMinPrice, DataValidation.DessertMaxPrice)]
        public decimal Price { get; set; }

        [Required]
        [StringLength(DataValidation.DessertDescriptionMaxLenght, ErrorMessage = GlobalConstants.ErrorMessage, MinimumLength = DataValidation.DessertDescriptionMinLenght)]
        public string Description { get; set; }

        public string Picture { get; set; }

        [Display(Name = "New Picture")]
        [DataType(DataType.Upload)]
        public IFormFile NewPicture { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        [Required]
        [Display(Name = "Category")]
        [ValidateSelectedDropDownOption]
        public string CategoryId { get; set; }
    }
}
