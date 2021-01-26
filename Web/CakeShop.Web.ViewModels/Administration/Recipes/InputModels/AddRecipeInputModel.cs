namespace CakeShop.Web.ViewModels.Administration.Recipes.InputModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CakeShop.Common;
    using CakeShop.Web.Infrastructure.ValidationAttributes;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class AddRecipeInputModel
    {
        [Required]
        [StringLength(DataValidation.RecipeTitleMaxLenght, ErrorMessage = GlobalConstants.ErrorMessage, MinimumLength = DataValidation.RecipeTitleMinLenght)]
        public string Title { get; set; }

        [Required]
        [StringLength(DataValidation.CommentContentMaxLenght, ErrorMessage = GlobalConstants.ErrorMessage, MinimumLength = DataValidation.RecipeContentMinLenght)]

        public string Content { get; set; }

        [Required]
        [DataType(DataType.Upload)]

        public IFormFile Picture { get; set; }

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
