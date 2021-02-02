namespace CakeShop.Services.Data.DessertLikes
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CakeShop.Data.Common.Repositories;
    using CakeShop.Data.Models;
    using CakeShop.Services.Data.Desserts;
    using Microsoft.EntityFrameworkCore;

    public class DessertLikesService : IDessertLikesService
    {
        private readonly IRepository<DessertLike> dessertLikesRepository;
        private readonly IDessertsService dessertsService;

        public DessertLikesService(
            IRepository<DessertLike> dessertLikesRepository,
            IDessertsService dessertsService)
        {
            this.dessertLikesRepository = dessertLikesRepository;
            this.dessertsService = dessertsService;
        }

        public async Task<bool> IsFavouriteAsync(string dessertId, string userId)
        {
            var isFavourite = await this.dessertLikesRepository
                .All()
                .AnyAsync(dl => dl.DessertId == dessertId && dl.ClientId == userId);

            return isFavourite;
        }

        public async Task<bool> LikeDessertAsync(string dessertId, string userId)
        {
            var isAdded = true;
            var isExisting = await this.IsFavouriteAsync(dessertId, userId);

            if (isExisting)
            {
                isAdded = await this.RemoveFromFavouriteAsync(dessertId, userId, isAdded);
            }
            else
            {
                await this.AddToFavouriteAsync(dessertId, userId);
            }

            await this.dessertLikesRepository.SaveChangesAsync();

            return isAdded;
        }

        public async Task<IEnumerable<T>> UnlikeDessertAsync<T>(string dessertId, string userId)
        {
            await this.RemoveFromFavouriteAsync(dessertId, userId, true);

            await this.dessertLikesRepository.SaveChangesAsync();

            var favouriteDesserts = await this.dessertsService.GetUserFavouriteDessertsAsync<T>(userId);

            return favouriteDesserts;
        }

        private async Task AddToFavouriteAsync(string dessertId, string userId)
        {
            var dessertLike = new DessertLike()
            {
                ClientId = userId,
                DessertId = dessertId,
            };

            await this.dessertLikesRepository.AddAsync(dessertLike);
        }

        private async Task<bool> RemoveFromFavouriteAsync(string dessertId, string userId, bool isAdded)
        {
            var dessertLike = await this.dessertLikesRepository
                                .All()
                                .FirstOrDefaultAsync(dl => dl.DessertId == dessertId && dl.ClientId == userId);

            isAdded = false;
            this.dessertLikesRepository.Delete(dessertLike);

            return isAdded;
        }
    }
}
