namespace CakeShop.Services.Data.Orders
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IOrdersService
    {
        Task<string> AddToBasketAsync(string userId, string dessertId, int quantity);

        Task<decimal> GetTotalPriceCurrentOrderByOrderAsync(string orderId);

        Task<decimal> GetTotalPriceCurrentOrderByUserAsync(string userId);

        Task<IEnumerable<T>> GetDessertsInBasketAsync<T>(string userId);

        Task<IEnumerable<T>> RemoveFromBasketAsync<T>(string dessertOrderId, string userId);
    }
}
