namespace CakeShop.Services.Data.Users
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CakeShop.Data.Models;

    public interface IUsersService
    {
        Task<ApplicationUser> UpdateUserProfileAsync(string id, string firstName, string lastName, string address, string phoneNumber);

        Task<string> GetUserAddressByIdAsync(string userId);

        Task<T> GetUserDataAsync<T>(string userId);

        Task<IEnumerable<T>> GetUserOrdersListAsync<T>(string userId);

        Task<IEnumerable<T>> GetAllAsync<T>();
    }
}
