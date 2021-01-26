namespace CakeShop.Services.Data.Categories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface ICategoriesService
    {
        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<IEnumerable<SelectListItem>> GetAllAsSelectListItemAsync();

        Task<bool> AddAsync(string name, IFormFile picture, string description);

        Task<T> GetDetailsForUpdateAsync<T>(string id);

        Task UpdateAsync(string id, string name, IFormFile newPicture, string description);

        Task DeleteAsync(string id);

        Task<string> GetPictureAsync(string id);
    }
}
