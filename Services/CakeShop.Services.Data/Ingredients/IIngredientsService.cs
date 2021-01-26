namespace CakeShop.Services.Data.Ingredients
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IIngredientsService
    {
        Task<string> GetIngredientIdByNameAsync(string name);

        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<bool> AddAsync(string name);

        Task DeleteAsync(string id);

        Task<T> GetDetailsForUpdateAsync<T>(string id);

        Task UpdateAsync(string id, string name);
    }
}
