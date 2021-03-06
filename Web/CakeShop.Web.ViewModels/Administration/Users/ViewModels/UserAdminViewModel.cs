﻿namespace CakeShop.Web.ViewModels.Administration.Users.ViewModels
{
    using System.Linq;

    using AutoMapper;
    using CakeShop.Data.Models;
    using CakeShop.Data.Models.Enums;
    using CakeShop.Services.Mapping;

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
                opt => opt.MapFrom(x => x.Orders.Where(o => o.OrderStatus == OrderStatus.Delivered).Sum(y => y.TotalPrice)));

            configuration.CreateMap<ApplicationUser, UserAdminViewModel>().ForMember(
                m => m.CommentsCount,
                opt => opt.MapFrom(x => x.Comments
                .Where(y => y.CommentStatus == CommentStatus.Approved)
                .Count()));
        }
    }
}
