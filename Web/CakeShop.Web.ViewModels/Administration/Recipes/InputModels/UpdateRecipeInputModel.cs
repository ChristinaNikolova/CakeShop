namespace CakeShop.Web.ViewModels.Administration.Recipes.InputModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CakeShop.Common;
    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;
    using CakeShop.Web.Infrastructure.ValidationAttributes;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class UpdateRecipeInputModel : IMapFrom<Recipe>
    {
        public string Id { get; set; }

        [Required]
        [StringLength(DataValidation.RecipeTitleMaxLenght, ErrorMessage = GlobalConstants.ErrorMessage, MinimumLength = DataValidation.RecipeTitleMinLenght)]
        public string Title { get; set; }

        [Required]
        [StringLength(DataValidation.RecipeContentMaxLenght, ErrorMessage = GlobalConstants.ErrorMessage, MinimumLength = DataValidation.RecipeContentMinLenght)]

        public string Content { get; set; }

        public string Picture { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "New Picture")]
        public IFormFile NewPicture { get; set; }

        [Range(typeof(int), DataValidation.MinPortionsValue, DataValidation.MaxPortionsValue)]
        public int Portions { get; set; }

        [Range(typeof(int), DataValidation.MinPreparationTimeValue, DataValidation.MaxPreparationTimeValue)]
        [Display(Name = "Preparation time (in minutes)")]
        public int PreparationTime { get; set; }

        [Range(typeof(int), DataValidation.MinCookingTimeValue, DataValidation.MaxCookingTimeValue)]
        [Display(Name = "Cooking time (in minutes)")]
        public int CookingTime { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        [Required]
        [Display(Name = "Category")]
        [ValidateSelectedDropDownOption]
        public string CategoryId { get; set; }
    }
}
