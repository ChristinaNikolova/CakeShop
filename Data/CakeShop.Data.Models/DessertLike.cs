namespace CakeShop.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class DessertLike
    {
        [Required]
        public string DessertId { get; set; }

        public virtual Dessert Dessert { get; set; }

        [Required]
        public string ClientId { get; set; }

        public virtual ApplicationUser Client { get; set; }
    }
}
