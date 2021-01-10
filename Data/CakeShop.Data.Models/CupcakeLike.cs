namespace CakeShop.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CupcakeLike
    {
        [Required]
        public string CupcakeId { get; set; }

        public virtual Cupcake Cupcake { get; set; }

        [Required]
        public string ClientId { get; set; }

        public virtual ApplicationUser Client { get; set; }
    }
}
