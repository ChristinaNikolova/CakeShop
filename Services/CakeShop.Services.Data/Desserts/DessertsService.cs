namespace CakeShop.Services.Data.Desserts
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CakeShop.Data.Common.Repositories;
    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class DessertsService : IDessertsService
    {
        private readonly IRepository<Dessert> dessertsRepository;

        public DessertsService(IRepository<Dessert> dessertsRepository)
        {
            this.dessertsRepository = dessertsRepository;
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

        public async Task<IEnumerable<T>> GetAllWithCurrentTags<T>(string categoryId, string[] tagTagNames)
        {
            var desserts = await this.dessertsRepository
                .All()
                .Where(d => d.CategoryId == categoryId && d.DessertTags.Any(dt => tagTagNames.Contains(dt.Tag.Name)))
                .To<T>()
                .ToListAsync();

            return desserts;
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
    }
}
