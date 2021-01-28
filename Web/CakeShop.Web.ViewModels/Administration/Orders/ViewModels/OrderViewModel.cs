namespace CakeShop.Web.ViewModels.Administration.Orders.ViewModels
{
    using System;

    using CakeShop.Common;
    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;

    public class OrderViewModel : IMapFrom<Order>
    {
        public string Id { get; set; }

        public decimal TotalPrice { get; set; }

        public bool IsPaid { get; set; }

        public string PaidAsString
            => this.IsPaid ? "Paid" : "Not Paid";

        public DateTime FinalizeOrder { get; set; }

        public string FormattedFinalizeOrder
            => string.Format(
                 GlobalConstants.DateTimeFormat,
                 TimeZoneInfo.ConvertTimeFromUtc(this.FinalizeOrder, TimeZoneInfo.FindSystemTimeZoneById(GlobalConstants.LocalTimeZone)));
    }
}
