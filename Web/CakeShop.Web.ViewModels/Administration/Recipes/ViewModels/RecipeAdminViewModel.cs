namespace CakeShop.Web.ViewModels.Administration.Recipes.ViewModels
{
    using System.Linq;

    using AutoMapper;
    using CakeShop.Data.Models;
    using CakeShop.Data.Models.Enums;
    using CakeShop.Services.Mapping;

    public class RecipeAdminViewModel : RecipeAdminBaseViewModel, IMapFrom<Recipe>, IHaveCustomMappings
    {
        public int CommentsCount { get; set; }

        public int RecipeLikesCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Recipe, RecipeAdminViewModel>().ForMember(
                m => m.CommentsCount,
                opt => opt.MapFrom(x => x.Comments
                .Where(y => y.CommentStatus == CommentStatus.Approved)
                .Count()));
        }
    }
}
