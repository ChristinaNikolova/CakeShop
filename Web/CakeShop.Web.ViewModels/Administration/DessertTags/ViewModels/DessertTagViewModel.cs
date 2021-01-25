namespace CakeShop.Web.ViewModels.Administration.DessertTags.ViewModels
{
    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;

    public class DessertTagViewModel : IMapFrom<DessertTag>
    {
        public string TagId { get; set; }

        public string TagName { get; set; }
    }
}
