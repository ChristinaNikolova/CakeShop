namespace CakeShop.Web.ViewModels.Administration.Users.ViewModels
{
    using AutoMapper;
    using CakeShop.Data.Models;
    using CakeShop.Data.Models.Enums;
    using CakeShop.Services.Mapping;
    using System.Linq;

    public class UserAdminViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public int OrdersCount { get; set; }

        public int ReviewsCount { get; set; }

        public int CommentsCount { get; set; }

        public decimal TotalSumOrders { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, UserAdminViewModel>().ForMember(
                m => m.TotalSumOrders,
                opt => opt.MapFrom(x => x.Orders.Where(o => o.Status == Status.Delivered).Sum(y => y.TotalPrice)));
        }
    }
}
