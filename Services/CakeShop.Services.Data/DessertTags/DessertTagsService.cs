namespace CakeShop.Services.Data.DessertTags
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CakeShop.Data.Common.Repositories;
    using CakeShop.Data.Models;
    using CakeShop.Services.Data.Tags;
    using CakeShop.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class DessertTagsService : IDessertTagsService
    {
        private readonly IRepository<DessertTag> dessertTagsRepository;
        private readonly ITagsService tagsService;

        public DessertTagsService(
            IRepository<DessertTag> dessertTagsRepository,
            ITagsService tagsService)
        {
            this.dessertTagsRepository = dessertTagsRepository;
            this.tagsService = tagsService;
        }

        public async Task<bool> AddTagToDessertAsync(string dessertId, string name)
        {
            var isAdded = true;

            var tagId = await this.tagsService.GetTagIdByNameAsync(name);

            if (tagId == null)
            {
                return !isAdded;
            }

            var isAlreadyAdded = await this.dessertTagsRepository
                .All()
                .AnyAsync(di => di.TagId == tagId && di.DessertId == dessertId);

            if (isAlreadyAdded)
            {
                return !isAdded;
            }

            var dessertTag = new DessertTag()
            {
                DessertId = dessertId,
                TagId = tagId,
            };

            await this.dessertTagsRepository.AddAsync(dessertTag);
            await this.dessertTagsRepository.SaveChangesAsync();

            return isAdded;
        }

        public async Task<IEnumerable<T>> GetAllCurrentDessertAsync<T>(string dessertId)
        {
            var tags = await this.dessertTagsRepository
               .All()
               .Where(di => di.DessertId == dessertId)
               .OrderBy(di => di.Tag.Name)
               .To<T>()
               .ToListAsync();

            return tags;
        }

        public async Task RemoveTagFromDessertAsync(string dessertId, string tagName)
        {
            var tagId = await this.tagsService.GetTagIdByNameAsync(tagName);

            var dessertTag = await this.dessertTagsRepository
                .All()
                .FirstOrDefaultAsync(di => di.DessertId == dessertId && di.TagId == tagId);

            this.dessertTagsRepository.Delete(dessertTag);
            await this.dessertTagsRepository.SaveChangesAsync();
        }
    }
}
