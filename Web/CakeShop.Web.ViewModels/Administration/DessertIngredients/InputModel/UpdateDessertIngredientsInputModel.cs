namespace CakeShop.Web.ViewModels.Administration.DessertIngredients.InputModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CakeShop.Common;
    using CakeShop.Web.ViewModels.Administration.DessertIngredients.ViewModels;
    using CakeShop.Web.ViewModels.Desserts.ViewModels;

    public class UpdateDessertIngredientsInputModel
    {
        public IEnumerable<DessertIngredientViewModel> DessertIngredients { get; set; }

        public DessertViewModel Dessert { get; set; }

        [Required]
        [StringLength(DataValidation.IngredientNameMaxLenght, ErrorMessage = GlobalConstants.ErrorMessage, MinimumLength = DataValidation.IngredientNameMinLenght)]
        public string Name { get; set; }
    }
}
