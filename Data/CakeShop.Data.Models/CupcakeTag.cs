namespace CakeShop.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CupcakeTag
    {
        [Required]
        public string CupcakeId { get; set; }

        public virtual Cupcake Cupcake { get; set; }

        [Required]
        public string TagId { get; set; }

        public virtual Tag Tag { get; set; }
    }
}
