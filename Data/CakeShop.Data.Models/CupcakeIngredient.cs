namespace CakeShop.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CupcakeIngredient
    {
        [Required]
        public string CupcakeId { get; set; }

        public virtual Cupcake Cupcake { get; set; }

        [Required]
        public string IngredientId { get; set; }

        public virtual Ingredient Ingredient { get; set; }
    }
}
