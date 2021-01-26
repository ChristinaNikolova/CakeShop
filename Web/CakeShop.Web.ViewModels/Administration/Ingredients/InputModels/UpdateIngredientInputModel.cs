namespace CakeShop.Web.ViewModels.Administration.Ingredients.InputModels
{
    using System.ComponentModel.DataAnnotations;

    using CakeShop.Common;
    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;

    public class UpdateIngredientInputModel : IMapFrom<Ingredient>
    {
        public string Id { get; set; }

        [Required]
        [StringLength(DataValidation.IngredientNameMaxLenght, ErrorMessage = GlobalConstants.ErrorMessage, MinimumLength = DataValidation.IngredientNameMinLenght)]
        public string Name { get; set; }
    }
}
