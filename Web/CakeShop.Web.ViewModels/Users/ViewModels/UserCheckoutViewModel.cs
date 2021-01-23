namespace CakeShop.Web.ViewModels.Users.ViewModels
{
    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;

    public class UserCheckoutViewModel : UserOrderDetailsViewModel, IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string Address { get; set; }
    }
}
