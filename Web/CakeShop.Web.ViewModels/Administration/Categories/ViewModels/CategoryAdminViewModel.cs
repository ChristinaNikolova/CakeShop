namespace CakeShop.Web.ViewModels.Administration.Categories.ViewModels
{
    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;
    using CakeShop.Web.ViewModels.Categories.ViewModels;

    public class CategoryAdminViewModel : BaseCategoryViewModel, IMapFrom<Category>
    {
        public string Picture { get; set; }

        public int DessertsCount { get; set; }

        public int RecipesCount { get; set; }
    }
}
