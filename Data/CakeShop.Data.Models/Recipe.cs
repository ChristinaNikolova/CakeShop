namespace CakeShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CakeShop.Common;
    using CakeShop.Data.Common.Models;

    public class Recipe : BaseDeletableModel<string>
    {
        public Recipe()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Author = GlobalConstants.SystemName;
            this.Comments = new HashSet<Comment>();
            this.RecipeLikes = new HashSet<RecipeLike>();
        }

        [Required]
        [MaxLength(DataValidation.RecipeTitleMaxLenght)]
        public string Title { get; set; }

        [Required]
        [MaxLength(DataValidation.RecipeContentMaxLenght)]

        public string Content { get; set; }

        [Required]

        public string Picture { get; set; }

        [Required]
        public string Author { get; set; }

        public int Portions { get; set; }

        public TimeSpan PreparationTime { get; set; }

        public TimeSpan CookingTime { get; set; }

        [Required]
        public string CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<RecipeLike> RecipeLikes { get; set; }
    }
}
