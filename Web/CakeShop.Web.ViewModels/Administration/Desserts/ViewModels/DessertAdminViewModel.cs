namespace CakeShop.Web.ViewModels.Administration.Desserts.ViewModels
{
    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;

    public class DessertAdminViewModel : IMapFrom<Dessert>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Picture { get; set; }

        public string CategoryName { get; set; }

        public int DessertLikesCount { get; set; }
    }
}
