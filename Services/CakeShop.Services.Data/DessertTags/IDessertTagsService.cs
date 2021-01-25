namespace CakeShop.Services.Data.DessertTags
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDessertTagsService
    {
        Task<IEnumerable<T>> GetAllCurrentDessertAsync<T>(string dessertId);

        Task<bool> AddTagToDessertAsync(string dessertId, string name);

        Task RemoveTagFromDessertAsync(string dessertId, string tagName);
    }
}
