namespace CakeShop.Services.Data.Reviews
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IReviewsService
    {
        Task<IEnumerable<T>> GetReviewsCurrentDessertAsync<T>(string dessertId);

        Task AddAsync(string content, int points, string orderId, string dessertId, string userId);
    }
}
