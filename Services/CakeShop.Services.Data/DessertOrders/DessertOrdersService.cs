namespace CakeShop.Services.Data.DessertOrders
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CakeShop.Data.Common.Repositories;
    using CakeShop.Data.Models;
    using CakeShop.Data.Models.Enums;
    using CakeShop.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class DessertOrdersService : IDessertOrdersService
    {
        private readonly IRepository<DessertOrder> dessertOrdersRepository;

        public DessertOrdersService(IRepository<DessertOrder> dessertOrdersRepository)
        {
            this.dessertOrdersRepository = dessertOrdersRepository;
        }

        public async Task<IEnumerable<T>> GetDessertsInBasketAsync<T>(string userId)
        {
            var desserts = await this.dessertOrdersRepository
                .All()
                .Where(deo => deo.Order.OrderStatus == OrderStatus.NotFinish
                           && deo.Order.ClientId == userId)
                .To<T>()
                .ToListAsync();

            return desserts;
        }

        public async Task<int> GetTotalQuantitiesCurrentOrderAsync(string orderId)
        {
            var quantities = await this.dessertOrdersRepository
                .All()
                .Where(deo => deo.OrderId == orderId)
                .SumAsync(deo => deo.Quantity);

            return quantities;
        }

        public async Task<IEnumerable<T>> GetDessertsCurrentOrderAsync<T>(string orderId)
        {
            var desserts = await this.dessertOrdersRepository
                .All()
                .Where(deo => deo.OrderId == orderId)
                .OrderByDescending(deo => deo.Quantity)
                .ThenBy(deo => deo.Dessert.Name)
                .To<T>()
                .ToListAsync();

            return desserts;
        }

        public async Task<IEnumerable<T>> GetDessertsForReviewAsync<T>(string userId)
        {
            var desserts = await this.dessertOrdersRepository
                .All()
                .Where(deo => deo.Order.ClientId == userId
                          && deo.Order.OrderStatus == OrderStatus.Delivered
                          && !deo.Order.IsReview
                          && !deo.IsReview)
                .OrderByDescending(deo => deo.CreatedOn)
                .ThenBy(deo => deo.OrderId)
                .ThenBy(deo => deo.Dessert.Name)
                .To<T>()
                .ToListAsync();

            return desserts;
        }

        public async Task UpdateDessertOrderReviewStatusAsync(string dessertId, string orderId)
        {
            var dessertOrder = await this.dessertOrdersRepository
                .All()
                .FirstOrDefaultAsync(deo => deo.DessertId == dessertId && deo.OrderId == orderId);

            dessertOrder.IsReview = true;

            this.dessertOrdersRepository.Update(dessertOrder);
            await this.dessertOrdersRepository.SaveChangesAsync();
        }

        public async Task<DessertOrder> GetByIdAsync(string dessertOrderId)
        {
            return await this.dessertOrdersRepository
                .All()
                .FirstOrDefaultAsync(deo => deo.Id == dessertOrderId);
        }

        public async Task DeleteAsync(DessertOrder dessertOrder)
        {
            this.dessertOrdersRepository.Delete(dessertOrder);
            await this.dessertOrdersRepository.SaveChangesAsync();
        }
    }
}
