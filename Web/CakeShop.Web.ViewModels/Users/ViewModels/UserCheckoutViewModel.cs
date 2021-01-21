namespace CakeShop.Web.ViewModels.Users.ViewModels
{
    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;

    public class UserCheckoutViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }
    }
}
