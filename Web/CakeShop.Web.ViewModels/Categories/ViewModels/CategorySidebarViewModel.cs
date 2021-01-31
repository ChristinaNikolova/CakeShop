namespace CakeShop.Web.ViewModels.Categories.ViewModels
{
    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;

    public class CategorySidebarViewModel : BaseCategoryViewModel, IMapFrom<Category>
    {
        public int RecipesCount { get; set; }
    }
}
