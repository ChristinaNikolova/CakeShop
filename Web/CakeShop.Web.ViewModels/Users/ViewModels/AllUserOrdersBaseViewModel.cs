namespace CakeShop.Web.ViewModels.Users.ViewModels
{
    using System.Collections.Generic;

    using CakeShop.Web.ViewModels.Common.ViewModels;

    public class AllUserOrdersBaseViewModel : PaginationViewModel
    {
        public IEnumerable<UserOrderBaseViewModel> UserOrders { get; set; }
    }
}
