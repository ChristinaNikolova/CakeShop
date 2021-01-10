namespace CakeShop.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class RecipeLike
    {
        [Required]
        public string ClientId { get; set; }

        public virtual ApplicationUser Client { get; set; }

        [Required]
        public string RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
