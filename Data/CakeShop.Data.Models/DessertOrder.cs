namespace CakeShop.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class DessertOrder
    {
        [Required]
        public string DessertId { get; set; }

        public virtual Dessert Dessert { get; set; }

        [Required]
        public string OrderId { get; set; }

        public virtual Order Order { get; set; }

        public int Quantity { get; set; }
    }
}
