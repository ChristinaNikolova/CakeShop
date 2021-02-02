namespace CakeShop.Services.Data.DessertOrders
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CakeShop.Data.Models;

    public interface IDessertOrdersService
    {
        Task<IEnumerable<T>> GetDessertsCurrentOrderAsync<T>(string orderId);

        Task<int> GetTotalQuantitiesCurrentOrderAsync(string orderId);

        Task<IEnumerable<T>> GetDessertsInBasketAsync<T>(string userId);

        Task<IEnumerable<T>> GetDessertsForReviewAsync<T>(string userId);

        Task UpdateDessertOrderReviewStatusAsync(string dessertId, string orderId);

        Task<DessertOrder> GetByIdAsync(string dessertOrderId);

        Task DeleteAsync(DessertOrder dessertOrder);
    }
}
