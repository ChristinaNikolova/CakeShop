namespace CakeShop.Services.Data.DessertIngredients
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDessertIngredientsService
    {
        Task<IEnumerable<T>> GetAllCurrentDessertAsync<T>(string dessertId);

        Task<bool> AddAsync(string dessertId, string name);

        Task RemoveAsync(string dessertId, string ingredientName);
    }
}
