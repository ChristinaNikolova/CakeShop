namespace CakeShop.Services.Data.Desserts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDessertsService
    {
        Task<IEnumerable<T>> GetAllCurrentCategoryAsync<T>(string categoryId, int take, int skip);

        Task<int> GetTotalCountDessertsByCategoryAsync(string categoryId);
    }
}
