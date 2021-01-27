namespace CakeShop.Services.Data.RecipeIngredients
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRecipeIngredientsService
    {
        Task<IEnumerable<T>> GetAllCurrentRecipeAsync<T>(string id);

        Task<bool> AddIngredientToRecipeAsync(string recipeId, string name, string quantity);

        Task RemoveIngredientFromRecipeAsync(string recipeId, string ingredientName);
    }
}
