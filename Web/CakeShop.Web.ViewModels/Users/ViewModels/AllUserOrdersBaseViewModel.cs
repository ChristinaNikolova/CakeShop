namespace CakeShop.Web.ViewModels.Users.ViewModels
{
    using System.Collections.Generic;

    public class AllUserOrdersBaseViewModel
    {
        public IEnumerable<UserOrderBaseViewModel> UserOrders { get; set; }
    }
}
