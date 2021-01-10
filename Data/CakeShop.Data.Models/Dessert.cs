namespace CakeShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CakeShop.Common;
    using CakeShop.Data.Common.Models;

    public class Dessert : BaseDeletableModel<string>
    {
        public Dessert()
        {
            this.Id = Guid.NewGuid().ToString();
            this.DessertTags = new HashSet<DessertTag>();
            this.DessertIngredients = new HashSet<DessertIngredient>();
            this.DessertOrders = new HashSet<DessertOrder>();
            this.DessertLikes = new HashSet<DessertLike>();
            this.Reviews = new HashSet<Review>();
        }

        [Required]
        [MaxLength(DataValidation.DessertNameMaxLenght)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        [Required]
        public string Picture { get; set; }

        [Required]
        [MaxLength(DataValidation.DessertDescriptionMaxLenght)]
        public string Description { get; set; }

        [Required]
        public string CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<DessertTag> DessertTags { get; set; }

        public virtual ICollection<DessertIngredient> DessertIngredients { get; set; }

        public virtual ICollection<DessertOrder> DessertOrders { get; set; }

        public virtual ICollection<DessertLike> DessertLikes { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
