namespace CakeShop.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using CakeShop.Common;

    public class RepiceIngredient
    {
        [Required]
        public string RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }

        [Required]
        public string IngredientId { get; set; }

        public virtual Ingredient Ingredient { get; set; }

        [Required]
        [MaxLength(DataValidation.RepiceIngredientQuantityMaxLenght)]
        public string Quantity { get; set; }
    }
}
