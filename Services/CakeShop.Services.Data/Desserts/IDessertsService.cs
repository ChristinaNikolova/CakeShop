﻿namespace CakeShop.Services.Data.Desserts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IDessertsService
    {
        Task<IEnumerable<T>> GetAllCurrentCategoryAsync<T>(string categoryId, int take, int skip);

        Task<int> GetTotalCountDessertsByCategoryAsync(string categoryId);

        Task<T> GetDetailsAsync<T>(string id);

        Task<IEnumerable<T>> GetAllWithCurrentTagsAsync<T>(string categoryId, string[] tagTagNames);

        Task<bool> LikeDessertAsync(string dessertId, string userId);

        Task<bool> IsFavouriteAsync(string dessertId, string userId);

        Task<decimal> GetDessertPriceAsync(string dessertId);

        Task<IEnumerable<T>> GetUserFavouriteDessertsAsync<T>(string userId);

        Task<IEnumerable<T>> UnlikeDessertAsync<T>(string dessertId, string userId);

        Task<IEnumerable<T>> OrderDessertsAsync<T>(string targetCriteria, string categoryId);

        Task<IEnumerable<T>> GetAllAsync<T>();

        Task DeleteAsync(string id);

        Task<T> GetDetailsForUpdateAsync<T>(string id);

        Task UpdateAsync(string id, string name, string description, decimal price, IFormFile newPicture, string categoryId);

        Task<string> GetPictureAsync(string id);
    }
}
