namespace CakeShop.Web.ViewModels.Users.ViewModels
{
    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;

    public class UserOrderDetailsViewModel : IMapFrom<ApplicationUser>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }
    }
}
