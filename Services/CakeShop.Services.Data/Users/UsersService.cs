namespace CakeShop.Services.Data.Users
{
    using System.Linq;
    using System.Threading.Tasks;

    using CakeShop.Data.Common.Repositories;
    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class UsersService : IUsersService
    {
        private readonly IRepository<ApplicationUser> usersRepository;

        public UsersService(IRepository<ApplicationUser> usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public async Task<string> GetUserAddressByIdAsync(string userId)
        {
            var address = await this.usersRepository
                .All()
                .Where(u => u.Id == userId)
                .Select(u => u.Address)
                .FirstOrDefaultAsync();

            return address;
        }

        public async Task<T> GetUserDataAsync<T>(string userId)
        {
            var user = await this.usersRepository
                .All()
                .Where(u => u.Id == userId)
                .To<T>()
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<ApplicationUser> UpdateUserProfileAsync(string id, string firstName, string lastName, string address, string phoneNumber)
        {
            var user = await this.usersRepository
                            .All()
                            .FirstOrDefaultAsync(u => u.Id == id);

            user.FirstName = firstName;
            user.LastName = lastName;
            user.Address = address;
            user.PhoneNumber = phoneNumber;

            await this.usersRepository.SaveChangesAsync();

            return user;
        }
    }
}
