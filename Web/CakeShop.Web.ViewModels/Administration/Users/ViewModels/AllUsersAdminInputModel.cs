namespace CakeShop.Web.ViewModels.Administration.Users.ViewModels
{
    using System.Collections.Generic;

    public class AllUsersAdminInputModel
    {
        public IEnumerable<UserAdminViewModel> Users { get; set; }
    }
}
