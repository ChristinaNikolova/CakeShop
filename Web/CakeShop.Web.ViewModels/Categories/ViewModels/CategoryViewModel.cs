namespace CakeShop.Web.ViewModels.Categories.ViewModels
{
    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;

    public class CategoryViewModel : IMapFrom<Category>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Picture { get; set; }

        public string Description { get; set; }
    }
}
