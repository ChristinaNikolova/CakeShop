namespace CakeShop.Services.Data.Orders
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IOrdersService
    {
        Task<string> AddToBasketAsync(string userId, string dessertId, int quantity);

        Task<decimal> GetTotalPriceCurrentOrderByOrderAsync(string orderId);

        Task<decimal> GetTotalPriceCurrentOrderByUserAsync(string userId);

        Task<IEnumerable<T>> RemoveFromBasketAsync<T>(string dessertOrderId, string userId);

        Task<string> GetOrderIdByUserAsync(string userId);

        Task<string> FinishOrderAsync(string userId);

        Task AddDetailsToCurrentOrderAsync(string orderId, string deliveryAddress, string notes);

        Task<T> GetOrderDetailsAsync<T>(string orderId);

        Task<IEnumerable<T>> GetAllAsync<T>(string status);

        Task<IEnumerable<T>> GetUserOrdersListAsync<T>(string userId, int take, int skip);

        Task DeleteAsync(string id);

        Task<string> ChangeStatusAsync(string id, string status);

        Task<int> GetOrdersCountCurrentUserAsync(string userId);

        Task<int> GetProcessingOrdersCountAsync();

        Task UpdateOrderReviewStatusAsync(string orderId);
    }
}
