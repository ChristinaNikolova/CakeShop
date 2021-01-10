namespace CakeShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CakeShop.Common;
    using CakeShop.Data.Common.Models;

    public class Cupcake : BaseDeletableModel<string>
    {
        public Cupcake()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CupCakeTags = new HashSet<CupcakeTag>();
            this.CupcakeIngredients = new HashSet<CupcakeIngredient>();
            this.CupcakeOrders = new HashSet<CupcakeOrder>();
            this.CupcakeLikes = new HashSet<CupcakeLike>();
            this.Reviews = new HashSet<Review>();
        }

        [Required]
        [MaxLength(DataValidation.CupcakeNameMaxLenght)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        [Required]
        public string Picture { get; set; }

        [Required]
        [MaxLength(DataValidation.CupcakeDescriptionMaxLenght)]
        public string Description { get; set; }

        [Required]
        public string CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<CupcakeTag> CupCakeTags { get; set; }

        public virtual ICollection<CupcakeIngredient> CupcakeIngredients { get; set; }

        public virtual ICollection<CupcakeOrder> CupcakeOrders { get; set; }

        public virtual ICollection<CupcakeLike> CupcakeLikes { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
