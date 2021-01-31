namespace CakeShop.Web.ViewModels.Categories.ViewModels
{
    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;

    public class BaseCategoryViewModel : IMapFrom<Category>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
