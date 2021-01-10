namespace CakeShop.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CupcakeOrder
    {
        [Required]
        public string CupcakeId { get; set; }

        public virtual Cupcake Cupcake { get; set; }

        [Required]
        public string OrderId { get; set; }

        public virtual Order Order { get; set; }

        public int Quantity { get; set; }
    }
}
