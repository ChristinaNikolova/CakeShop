namespace CakeShop.Services.Data.Recipes
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IRecipesService
    {
        Task<IEnumerable<T>> GetAllAsync<T>();

        Task AddAsync(string title, string content, IFormFile picture, int portions, int preparationTime, int cookingTime, string categoryId);

        Task<T> GetDetailsAsync<T>(string id);

        Task UpdateAsync(string id, string title, string content, IFormFile newPicture, int portions, int cookingTime, int preparationTime, string categoryId);

        Task DeleteAsync(string id);

        Task<string> GetPictureAsync(string id);

        Task<bool> IsFavouriteAsync(string id, string userId);

        Task<bool> LikeRecipeAsync(string recipeId, string userId);

        Task<int> GetLikesCountAsync(string recipeId);

        Task<IEnumerable<T>> GetRecentRecipesAsync<T>();

        Task<IEnumerable<T>> GetPopulartRecipesAsync<T>();

        Task<IEnumerable<T>> GetByCategoryAsync<T>(string categoryId);
    }
}
