namespace CakeShop.Services.Data.Tags
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CakeShop.Data.Common.Repositories;
    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class TagsService : ITagsService
    {
        private readonly IRepository<Tag> tagsRepository;

        public TagsService(IRepository<Tag> tagsRepository)
        {
            this.tagsRepository = tagsRepository;
        }

        public async Task<bool> AddAsync(string name)
        {
            var isAdded = true;

            var isAlreadyAdded = await this.tagsRepository
                 .All()
                 .AnyAsync(i => i.Name.ToLower() == name.ToLower());

            if (isAlreadyAdded)
            {
                return !isAdded;
            }

            var tag = new Tag()
            {
                Name = name,
            };

            await this.tagsRepository.AddAsync(tag);
            await this.tagsRepository.SaveChangesAsync();

            return isAdded;
        }

        public async Task DeleteAsync(string id)
        {
            var tag = await this.GetByIdAsync(id);

            tag.IsDeleted = true;

            this.tagsRepository.Update(tag);
            await this.tagsRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            var tags = await this.tagsRepository
                .All()
                .OrderBy(t => t.Name)
                .To<T>()
                .ToListAsync();

            return tags;
        }

        public async Task<T> GetDetailsForUpdateAsync<T>(string id)
        {
            var tag = await this.tagsRepository
                .All()
                .Where(i => i.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

            return tag;
        }

        public async Task<string> GetTagIdByNameAsync(string name)
        {
            var id = await this.tagsRepository
                .All()
                .Where(t => t.Name.ToLower() == name.ToLower())
                .Select(t => t.Id)
                .FirstOrDefaultAsync();

            return id;
        }

        public async Task UpdateAsync(string id, string name)
        {
            var tag = await this.GetByIdAsync(id);

            tag.Name = name;

            this.tagsRepository.Update(tag);
            await this.tagsRepository.SaveChangesAsync();
        }

        private async Task<Tag> GetByIdAsync(string id)
        {
            return await this.tagsRepository
                .All()
                .FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}
