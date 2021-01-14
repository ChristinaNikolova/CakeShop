namespace CakeShop.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using CakeShop.Common;
    using CakeShop.Data.Common.Models;

    public class Review : BaseDeletableModel<string>
    {
        public Review()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(DataValidation.ReviewContentMaxLenght)]
        public string Content { get; set; }

        public int Points { get; set; }

        [Required]
        public string ClientId { get; set; }

        public virtual ApplicationUser Client { get; set; }

        [Required]
        public string DessertId { get; set; }

        public virtual Dessert Dessert { get; set; }
    }
}
