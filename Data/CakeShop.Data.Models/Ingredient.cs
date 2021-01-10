namespace CakeShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CakeShop.Common;
    using CakeShop.Data.Common.Models;

    public class Ingredient : BaseDeletableModel<string>
    {
        public Ingredient()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CupcakeIngredients = new HashSet<CupcakeIngredient>();
        }

        [Required]
        [MaxLength(DataValidation.IngredientNameMaxLenght)]
        public string Name { get; set; }

        public virtual ICollection<CupcakeIngredient> CupcakeIngredients { get; set; }
    }
}
