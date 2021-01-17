namespace CakeShop.Services.Data.Orders
{
    using System.Threading.Tasks;

    public interface IOrdersService
    {
        Task<string> AddToBasketAsync(string userId, string dessertId, int quantity);

        Task<decimal> GetTotalPriceCurrentOrderAsync(string orderId);
    }
}
