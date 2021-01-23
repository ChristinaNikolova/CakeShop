namespace CakeShop.Services.Data.Desserts
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CakeShop.Common;
    using CakeShop.Data.Common.Repositories;
    using CakeShop.Data.Models;
    using CakeShop.Services.Cloudinary;
    using CakeShop.Services.Mapping;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;

    public class DessertsService : IDessertsService
    {
        private readonly IRepository<Dessert> dessertsRepository;
        private readonly IRepository<DessertLike> dessertLikesRepository;
        private readonly ICloudinaryService cloudinaryService;

        public DessertsService(
            IRepository<Dessert> dessertsRepository,
            IRepository<DessertLike> dessertLikesRepository,
            ICloudinaryService cloudinaryService)
        {
            this.dessertsRepository = dessertsRepository;
            this.dessertLikesRepository = dessertLikesRepository;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task UpdateAsync(string id, string name, string description, decimal price, IFormFile newPicture, string categoryId)
        {
            var dessert = await this.GetByIdAsync(id);

            dessert.Name = name;
            dessert.Description = description;
            dessert.Price = price;
            dessert.CategoryId = categoryId;

            if (newPicture != null)
            {
                var pictureAsString = await this.GetPictureAsStringAsync(name, newPicture);
                dessert.Picture = pictureAsString;
            }

            this.dessertsRepository.Update(dessert);
            await this.dessertsRepository.SaveChangesAsync();
        }

        public async Task<T> GetDetailsForUpdateAsync<T>(string id)
        {
            var dessert = await this.dessertsRepository
                .All()
                .Where(d => d.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

            return dessert;
        }

        public async Task<IEnumerable<T>> OrderDessertsAsync<T>(string targetCriteria, string categoryId)
        {
            var query = this.dessertsRepository
                .All()
                .Where(d => d.CategoryId == categoryId)
                .AsQueryable();

            if (targetCriteria.ToLower() == GlobalConstants.CriteriaPrice)
            {
                query = query
                     .OrderBy(d => d.Price);
            }
            else if (targetCriteria.ToLower() == GlobalConstants.CriteriaName)
            {
                query = query
                    .OrderBy(d => d.Name);
            }
            else
            {
                query = query
                    .OrderByDescending(d => d.CreatedOn);
            }

            var orderedDesserts = await query
                .To<T>()
                .ToListAsync();

            return orderedDesserts;
        }

        public async Task<IEnumerable<T>> GetAllCurrentCategoryAsync<T>(string categoryId, int take, int skip)
        {
            var desserts = await this.dessertsRepository
                .All()
                .Where(d => d.CategoryId == categoryId)
                .OrderByDescending(d => d.CreatedOn)
                .Skip(skip)
                .Take(take)
                .To<T>()
                .ToListAsync();

            return desserts;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            var desserts = await this.dessertsRepository
                .All()
                .OrderBy(d => d.Category.Name)
                .ThenBy(d => d.Name)
                .To<T>()
                .ToListAsync();

            return desserts;
        }

        public async Task<IEnumerable<T>> GetAllWithCurrentTagsAsync<T>(string categoryId, string[] tagTagNames)
        {
            var desserts = await this.dessertsRepository
                .All()
                .Where(d => d.CategoryId == categoryId && d.DessertTags.Any(dt => tagTagNames.Contains(dt.Tag.Name)))
                .To<T>()
                .ToListAsync();

            return desserts;
        }

        public async Task<decimal> GetDessertPriceAsync(string dessertId)
        {
            var price = await this.dessertsRepository
                .All()
                .Where(d => d.Id == dessertId)
                .Select(d => d.Price)
                .FirstOrDefaultAsync();

            return price;
        }

        public async Task<T> GetDetailsAsync<T>(string id)
        {
            var dessert = await this.dessertsRepository
                .All()
                .Where(d => d.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

            return dessert;
        }

        public async Task<int> GetTotalCountDessertsByCategoryAsync(string categoryId)
        {
            var count = await this.dessertsRepository
                .All()
                .Where(d => d.CategoryId == categoryId)
                .CountAsync();

            return count;
        }

        public async Task<IEnumerable<T>> GetUserFavouriteDessertsAsync<T>(string userId)
        {
            var desserts = await this.dessertsRepository
                .All()
                .Where(d => d.DessertLikes.Any(dl => dl.ClientId == userId))
                .OrderBy(d => d.Name)
                .To<T>()
                .ToListAsync();

            return desserts;
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

            var favouriteDesserts = await this.GetUserFavouriteDessertsAsync<T>(userId);

            return favouriteDesserts;
        }

        public async Task DeleteAsync(string id)
        {
            var dessert = await this.GetByIdAsync(id);

            dessert.IsDeleted = true;

            this.dessertsRepository.Update(dessert);
            await this.dessertsRepository.SaveChangesAsync();
        }

        private async Task<Dessert> GetByIdAsync(string id)
        {
            return await this.dessertsRepository
                .All()
                .FirstOrDefaultAsync(d => d.Id == id);
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

        private async Task<string> GetPictureAsStringAsync(string name, IFormFile picture)
        {
            return await this.cloudinaryService.UploudAsync(picture, name);
        }

        public async Task<string> GetPictureAsync(string id)
        {
            var picture = await this.dessertsRepository
                 .All()
                 .Where(d => d.Id == id)
                 .Select(d => d.Picture)
                 .FirstOrDefaultAsync();

            return picture;
        }
    }
}
