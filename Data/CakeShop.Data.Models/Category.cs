namespace CakeShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CakeShop.Common;
    using CakeShop.Data.Common.Models;

    public class Category : BaseDeletableModel<string>
    {
        public Category()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Cupcakes = new HashSet<Cupcake>();
            this.Recipes = new HashSet<Recipe>();
        }

        [Required]
        [MaxLength(DataValidation.CategotyNameMaxLenght)]
        public string Name { get; set; }

        public virtual ICollection<Cupcake> Cupcakes { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
