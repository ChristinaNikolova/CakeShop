namespace CakeShop.Web.ViewModels.Administration.RecipeIngredients.InputModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CakeShop.Common;
    using CakeShop.Web.ViewModels.Administration.RecipeIngredients.ViewModels;
    using CakeShop.Web.ViewModels.Administration.Recipes.ViewModels;

    public class UpdateRecipeIngredientsInputModel
    {
        public IEnumerable<RecipeIngredientViewModel> RecipeIngredients { get; set; }

        public RecipeAdminBaseViewModel Recipe { get; set; }

        [Required]
        [StringLength(DataValidation.IngredientNameMaxLenght, ErrorMessage = GlobalConstants.ErrorMessage, MinimumLength = DataValidation.IngredientNameMinLenght)]
        public string Name { get; set; }

        [Required]
        [StringLength(DataValidation.RepiceIngredientQuantityMaxLenght, ErrorMessage = GlobalConstants.ErrorMessage, MinimumLength = DataValidation.RepiceIngredientQuantityMinLenght)]
        public string Quantity { get; set; }
    }
}
