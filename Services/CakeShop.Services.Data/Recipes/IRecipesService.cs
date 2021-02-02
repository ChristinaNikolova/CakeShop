namespace CakeShop.Services.Data.Recipes
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IRecipesService
    {
        Task<IEnumerable<T>> GetAllAsync<T>(int take = 0, int skip = 0);

        Task AddAsync(string title, string content, IFormFile picture, int portions, int preparationTime, int cookingTime, string categoryId);

        Task<T> GetDetailsAsync<T>(string id);

        Task UpdateAsync(string id, string title, string content, IFormFile newPicture, int portions, int cookingTime, int preparationTime, string categoryId);

        Task DeleteAsync(string id);

        Task<string> GetPictureAsync(string id);

        Task<IEnumerable<T>> GetRecentRecipesAsync<T>();

        Task<IEnumerable<T>> GetPopulartRecipesAsync<T>();

        Task<IEnumerable<T>> GetByCategoryAsync<T>(string categoryId);

        Task<int> GetTotalCountRecipesAsync();

        Task<IEnumerable<T>> OrderRecipesByCriteriaAsync<T>(string criteria);

        Task<IEnumerable<T>> GetUserFavouriteRecipesAsync<T>(string userId);
    }
}
