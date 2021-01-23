namespace CakeShop.Web.ViewModels.Recipes.ViewModels
{
    using System;
    using CakeShop.Common;
    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;

    public class RecipeViewModel : IMapFrom<Recipe>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string ShortContent
            => this.Content.Length > GlobalConstants.RepiceShortDescriptionLength
            ? this.Content.Substring(0, GlobalConstants.RepiceShortDescriptionLength) + "..."
            : this.Content;

        public string Picture { get; set; }

        public string Author { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CategoryName { get; set; }

        public int LikesCount { get; set; }

        public int CommentsCount { get; set; }
    }
}
