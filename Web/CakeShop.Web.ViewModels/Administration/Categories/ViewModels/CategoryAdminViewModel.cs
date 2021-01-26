namespace CakeShop.Web.ViewModels.Administration.Categories.ViewModels
{
    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;

    public class CategoryAdminViewModel : IMapFrom<Category>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Picture { get; set; }

        public int DessertsCount { get; set; }

        public int RecipesCount { get; set; }
    }
}
