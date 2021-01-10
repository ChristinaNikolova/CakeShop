namespace CakeShop.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class DessertTag
    {
        [Required]
        public string DessertId { get; set; }

        public virtual Dessert Dessert { get; set; }

        [Required]
        public string TagId { get; set; }

        public virtual Tag Tag { get; set; }
    }
}
