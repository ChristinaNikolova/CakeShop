namespace CakeShop.Services.Data.Tags
{
    using System.Linq;
    using System.Threading.Tasks;

    using CakeShop.Data.Common.Repositories;
    using CakeShop.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class TagsService : ITagsService
    {
        private readonly IRepository<Tag> tagsRepository;

        public TagsService(IRepository<Tag> tagsRepository)
        {
            this.tagsRepository = tagsRepository;
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
    }
}
