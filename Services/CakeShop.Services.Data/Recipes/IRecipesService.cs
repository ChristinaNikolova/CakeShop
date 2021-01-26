namespace CakeShop.Services.Data.Recipes
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IRecipesService
    {
        Task<IEnumerable<T>> GetAllAsync<T>();

        Task AddAsync(string title, string content, IFormFile picture, int portions, int preparationTime, int cookingTime, string categoryId);
    }
}
