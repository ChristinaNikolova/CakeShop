namespace CakeShop.Web.ViewModels.Administration.Tags.ViewModels
{
    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;

    public class TagAdminViewModel : IMapFrom<Tag>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int CupcakeTagsCount { get; set; }
    }
}
