namespace CakeShop.Services.Data.Users
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CakeShop.Data.Common.Repositories;
    using CakeShop.Data.Models;
    using CakeShop.Data.Models.Enums;
    using CakeShop.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class UsersService : IUsersService
    {
        private readonly IRepository<ApplicationUser> usersRepository;
        private readonly IRepository<Order> ordersRepository;

        public UsersService(
            IRepository<ApplicationUser> usersRepository,
            IRepository<Order> ordersRepository)
        {
            this.usersRepository = usersRepository;
            this.ordersRepository = ordersRepository;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            var users = await this.usersRepository
                .All()
                .OrderBy(u => u.UserName)
                .To<T>()
                .ToListAsync();

            return users;
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

        public async Task<string> GetUserEmailByIdAsync(string userId)
        {
            var email = await this.usersRepository
              .All()
              .Where(u => u.Id == userId)
              .Select(u => u.Email)
              .FirstOrDefaultAsync();

            return email;
        }

        public async Task<T> GetUserDataByOrderIdAsync<T>(string orderId)
        {
            var user = await this.usersRepository
                .All()
                .Where(u => u.Orders.Any(o => o.Id == orderId))
                .To<T>()
                .FirstOrDefaultAsync();

            return user;
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

        public async Task<IEnumerable<T>> GetUserOrdersListAsync<T>(string userId, int take, int skip)
        {
            var orders = await this.ordersRepository
                .All()
                .Where(o => o.ClientId == userId && o.Status != Status.NotFinish && o.Status != Status.Default)
                .OrderByDescending(o => o.CreatedOn)
                .Skip(skip)
                .Take(take)
                .To<T>()
                .ToListAsync();

            return orders;
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
