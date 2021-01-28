namespace CakeShop.Services.Data.Orders
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IOrdersService
    {
        Task<string> AddToBasketAsync(string userId, string dessertId, int quantity);

        Task<decimal> GetTotalPriceCurrentOrderByOrderAsync(string orderId);

        Task<decimal> GetTotalPriceCurrentOrderByUserAsync(string userId);

        Task<int> GetTotalQuantitiesCurrentOrderAsync(string orderId);

        Task<IEnumerable<T>> GetDessertsInBasketAsync<T>(string userId);

        Task<IEnumerable<T>> RemoveFromBasketAsync<T>(string dessertOrderId, string userId);

        Task<string> GetOrderIdByUserAsync(string userId);

        Task<string> FinishOrderAsync(string userId);

        Task AddDetailsToCurrentOrderAsync(string orderId, string deliveryAddress, string notes);

        Task<IEnumerable<T>> GetDessertsCurrentOrderAsync<T>(string orderId);

        Task<T> GetOrderDetailsAsync<T>(string orderId);

        Task<IEnumerable<T>> GetAllAsync<T>(string status);
    }
}
