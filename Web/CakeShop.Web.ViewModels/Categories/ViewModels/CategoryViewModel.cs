namespace CakeShop.Web.ViewModels.Categories.ViewModels
{
    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;

    public class CategoryViewModel : BaseCategoryViewModel, IMapFrom<Category>
    {
        public string Picture { get; set; }

        public string Description { get; set; }
    }
}
