namespace CakeShop.Web.ViewModels.Administration.Ingredients.InputModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CakeShop.Common;
    using CakeShop.Web.ViewModels.Administration.Ingredients.ViewModels;

    public class AllIngredientsAdminInputModel
    {
        public IEnumerable<IngredientAdminViewModel> Ingredients { get; set; }

        [Required]
        [StringLength(DataValidation.IngredientNameMaxLenght, ErrorMessage = GlobalConstants.ErrorMessage, MinimumLength = DataValidation.IngredientNameMinLenght)]
        public string Name { get; set; }
    }
}
