namespace CakeShop.Services.Data.Orders
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CakeShop.Data.Common.Repositories;
    using CakeShop.Data.Models;
    using CakeShop.Data.Models.Enums;
    using CakeShop.Services.Data.Desserts;
    using CakeShop.Services.Data.Users;
    using CakeShop.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class OrdersService : IOrdersService
    {
        private readonly IRepository<Order> ordersRepository;
        private readonly IRepository<DessertOrder> dessertOrdersRepository;
        private readonly IUsersService usersService;
        private readonly IDessertsService dessertsService;

        public OrdersService(
            IRepository<Order> ordersRepository,
            IRepository<DessertOrder> dessertOrdersRepository,
            IUsersService usersService,
            IDessertsService dessertsService)
        {
            this.ordersRepository = ordersRepository;
            this.dessertOrdersRepository = dessertOrdersRepository;
            this.usersService = usersService;
            this.dessertsService = dessertsService;
        }

        public async Task<string> AddToBasketAsync(string userId, string dessertId, int quantity)
        {
            var hasUserAlreadyBasket = await this.ordersRepository
                .All()
                .AnyAsync(o => o.ClientId == userId && o.Status == Status.NotFinish);

            var dessertPrice = await this.dessertsService.GetDessertPriceAsync(dessertId);
            var order = null as Order;

            if (hasUserAlreadyBasket)
            {
                order = await this.AddDessertToAlreadyExistingOrderAsync(userId, dessertId, quantity, dessertPrice, order);
            }
            else
            {
                order = await this.AddDessertToNewOrderAsync(userId, dessertId, quantity, dessertPrice, order);
            }

            await this.ordersRepository.SaveChangesAsync();

            return order.Id;
        }

        public async Task<decimal> GetTotalPriceCurrentOrderByOrderAsync(string orderId)
        {
            var totalSum = await this.ordersRepository
                .All()
                .Where(o => o.Id == orderId)
                .Select(o => o.TotalPrice)
                .FirstOrDefaultAsync();

            return totalSum;
        }

        public async Task<decimal> GetTotalPriceCurrentOrderByUserAsync(string userId)
        {
            var totalPrice = await this.ordersRepository
                 .All()
                 .Where(o => o.ClientId == userId && o.Status == Status.NotFinish)
                 .Select(o => o.TotalPrice)
                 .FirstOrDefaultAsync();

            return totalPrice;
        }

        public async Task<IEnumerable<T>> GetDessertsInBasketAsync<T>(string userId)
        {
            var desserts = await this.dessertOrdersRepository
                .All()
                .Where(deo => deo.Order.Status == Status.NotFinish && deo.Order.ClientId == userId)
                .To<T>()
                .ToListAsync();

            return desserts;
        }

        public async Task<IEnumerable<T>> RemoveFromBasketAsync<T>(string dessertOrderId, string userId)
        {
            var dessertOrderToRemove = await this.dessertOrdersRepository
                .All()
                .FirstOrDefaultAsync(deo => deo.Id == dessertOrderId);

            var order = await this.ordersRepository
                .All()
                .FirstOrDefaultAsync(o => o.ClientId == userId && o.Status == Status.NotFinish);

            var dessertPrice = await this.dessertsService.GetDessertPriceAsync(dessertOrderToRemove.DessertId);
            order.TotalPrice -= dessertPrice;

            this.dessertOrdersRepository.Delete(dessertOrderToRemove);
            this.ordersRepository.Update(order);
            await this.ordersRepository.SaveChangesAsync();
            await this.dessertOrdersRepository.SaveChangesAsync();

            var desserts = await this.GetDessertsInBasketAsync<T>(userId);

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

        private async Task<Order> AddDessertToNewOrderAsync(string userId, string dessertId, int quantity, decimal dessertPrice, Order order)
        {
            var clientAddress = await this.usersService.GetUserAddressByIdAsync(userId);

            order = new Order()
            {
                ClientId = userId,
                DeliveryAddress = clientAddress,
            };

            var dessertOrder = new DessertOrder()
            {
                DessertId = dessertId,
                OrderId = order.Id,
                Quantity = quantity,
            };

            order.DessertOrders.Add(dessertOrder);
            order.TotalPrice += dessertPrice * quantity;

            await this.ordersRepository.AddAsync(order);
            return order;
        }

        private async Task<Order> AddDessertToAlreadyExistingOrderAsync(string userId, string dessertId, int quantity, decimal dessertPrice, Order order)
        {
            order = await this.ordersRepository
                                .All()
                                .FirstOrDefaultAsync(o => o.ClientId == userId && o.Status == Status.NotFinish);

            var dessertOrder = new DessertOrder()
            {
                DessertId = dessertId,
                OrderId = order.Id,
                Quantity = quantity,
            };

            order.DessertOrders.Add(dessertOrder);
            order.TotalPrice += dessertPrice * quantity;

            this.ordersRepository.Update(order);
            return order;
        }
    }
}
