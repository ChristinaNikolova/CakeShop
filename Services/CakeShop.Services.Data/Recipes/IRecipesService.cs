namespace CakeShop.Services.Data.Recipes
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRecipesService
    {
        Task<IEnumerable<T>> GetAllAsync<T>();
    }
}
