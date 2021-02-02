namespace CakeShop.Web.ViewModels.Recipes.ViewModels
{
    using System.Linq;
    using System.Net;
    using System.Text.RegularExpressions;

    using AutoMapper;
    using CakeShop.Common;
    using CakeShop.Data.Models;
    using CakeShop.Data.Models.Enums;
    using CakeShop.Services.Mapping;
    using Ganss.XSS;

    public class RecipeViewModel : SidebarRecipeViewModel, IMapFrom<Recipe>, IHaveCustomMappings
    {
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

        public string Author { get; set; }

        public string CategoryName { get; set; }

        public int RecipeLikesCount { get; set; }

        public int CommentsCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Recipe, RecipeViewModel>().ForMember(
                m => m.CommentsCount,
                opt => opt.MapFrom(x => x.Comments
                .Where(y => y.CommentStatus == CommentStatus.Approved)
                .Count()));
        }
    }
}
