namespace CakeShop.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class DessertIngredient
    {
        [Required]
        public string DessertId { get; set; }

        public virtual Dessert Dessert { get; set; }

        [Required]
        public string IngredientId { get; set; }

        public virtual Ingredient Ingredient { get; set; }
    }
}
