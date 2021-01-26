namespace CakeShop.Services.Data.Tags
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITagsService
    {
        Task<string> GetTagIdByNameAsync(string name);

        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<bool> AddAsync(string name);

        Task<T> GetDetailsForUpdateAsync<T>(string id);

        Task UpdateAsync(string id, string name);

        Task DeleteAsync(string id);
    }
}
