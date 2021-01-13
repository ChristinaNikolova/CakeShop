namespace CakeShop.Services.Data.Users
{
    using System.Threading.Tasks;

    using CakeShop.Data.Models;

    public interface IUsersService
    {
        Task<ApplicationUser> UpdateUserProfileAsync(string id, string firstName, string lastName, string address, string phoneNumber);
    }
}
