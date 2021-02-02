namespace CakeShop.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using CakeShop.Common;
    using CakeShop.Data.Common.Models;
    using CakeShop.Data.Models.Enums;

    public class Comment : BaseDeletableModel<string>
    {
        public Comment()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CommentStatus = CommentStatus.NotApproved;
        }

        [Required]
        [MaxLength(DataValidation.CommentContentMaxLenght)]
        public string Content { get; set; }

        public CommentStatus CommentStatus { get; set; }

        [Required]
        public string ClientId { get; set; }

        public virtual ApplicationUser Client { get; set; }

        [Required]
        public string RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
