namespace CakeShop.Services.Data.Recipes
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IRecipesService
    {
        Task<IEnumerable<T>> GetAllAsync<T>();

        Task AddAsync(string title, string content, IFormFile picture, int portions, int preparationTime, int cookingTime, string categoryId);

        Task<T> GetDetailsForUpdateAsync<T>(string id);

        Task UpdateAsync(string id, string title, string content, IFormFile newPicture, int portions, int cookingTime, int preparationTime, string categoryId);

        Task DeleteAsync(string id);

        Task<string> GetPictureAsync(string id);
    }
}
