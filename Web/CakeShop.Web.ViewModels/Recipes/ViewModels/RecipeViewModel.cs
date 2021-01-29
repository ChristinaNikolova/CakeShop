namespace CakeShop.Web.ViewModels.Recipes.ViewModels
{
    using System;
    using System.Net;
    using System.Text.RegularExpressions;

    using CakeShop.Common;
    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;
    using Ganss.XSS;

    public class RecipeViewModel : IMapFrom<Recipe>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public string ShortContent
        {
            get
            {
                var content = WebUtility.HtmlDecode(Regex.Replace(this.SanitizedContent, @"<[^>]+>", string.Empty));

                return content.Length > GlobalConstants.RepiceShortDescriptionLength
                        ? content.Substring(0, GlobalConstants.RepiceShortDescriptionLength) + "..."
                        : content;
            }
        }

        public string Picture { get; set; }

        public string Author { get; set; }

        //format
        public DateTime CreatedOn { get; set; }

        public string CategoryName { get; set; }

        public int LikesCount { get; set; }

        public int CommentsCount { get; set; }
    }
}
