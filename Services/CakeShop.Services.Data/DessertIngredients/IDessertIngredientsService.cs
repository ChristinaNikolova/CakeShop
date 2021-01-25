namespace CakeShop.Services.Data.DessertIngredients
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDessertIngredientsService
    {
        Task<IEnumerable<T>> GetAllCurrentDessertAsync<T>(string dessertId);

        Task<bool> AddIngredientToDessertAsync(string dessertId, string name);

        Task RemoveIngredientFromDessertAsync(string dessertId, string ingredientName);
    }
}
