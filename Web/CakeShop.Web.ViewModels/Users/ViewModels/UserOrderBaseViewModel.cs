namespace CakeShop.Web.ViewModels.Users.ViewModels
{
    using System;

    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;

    public class UserOrderBaseViewModel : IMapFrom<Order>
    {
        public string Id { get; set; }

        public decimal TotalPrice { get; set; }

        public bool IsPaid { get; set; }

        public string PaidAsString
            => this.IsPaid ? "Yes" : "No";

        public string Status { get; set; }

        public DateTime FinalizeOrder { get; set; }

        //date as Eng
        public string FormattedFinalizeOrder
            => string.Format("{0:f}", this.FinalizeOrder);
    }
}
