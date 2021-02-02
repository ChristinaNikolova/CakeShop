namespace CakeShop.Services.Data.Users
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CakeShop.Data.Common.Repositories;
    using CakeShop.Data.Models;
    using CakeShop.Data.Models.Enums;
    using CakeShop.Services.Cloudinary;
    using CakeShop.Services.Mapping;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;

    public class UsersService : IUsersService
    {
        private readonly IRepository<ApplicationUser> usersRepository;
        private readonly ICloudinaryService cloudinaryService;

        public UsersService(
            IRepository<ApplicationUser> usersRepository,
            ICloudinaryService cloudinaryService)
        {
            this.usersRepository = usersRepository;
            this.cloudinaryService = cloudinaryService;
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

        public async Task<ApplicationUser> UpdateUserProfileAsync(string id, string firstName, string lastName, string address, string phoneNumber, IFormFile newPicture)
        {
            var user = await this.usersRepository
                            .All()
                            .FirstOrDefaultAsync(u => u.Id == id);

            user.FirstName = firstName;
            user.LastName = lastName;
            user.Address = address;
            user.PhoneNumber = phoneNumber;

            if (newPicture != null)
            {
                string newPictureAsUrl = await this.cloudinaryService.UploudAsync(newPicture, user.UserName);
                user.Picture = newPictureAsUrl;
            }

            await this.usersRepository.SaveChangesAsync();

            return user;
        }

        public async Task<bool> CheckForBellAsync(string userId)
        {
            var isBell = await this.usersRepository
                .All()
                .Where(u => u.Id == userId)
                .AnyAsync(u => u.Orders
                .Any(o => o.OrderStatus == OrderStatus.Delivered && o.IsReview == false));

            return isBell;
        }
    }
}
