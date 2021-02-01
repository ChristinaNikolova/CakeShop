namespace CakeShop.Web.ViewModels.Administration.Desserts.ViewModels
{
    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;
    using CakeShop.Web.ViewModels.Desserts.ViewModels;

    public class DessertAdminViewModel : DessertViewModel, IMapFrom<Dessert>
    {
        public int DessertLikesCount { get; set; }
    }
}
