namespace CakeShop.Services.Data.Orders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CakeShop.Data.Common.Repositories;
    using CakeShop.Data.Models;
    using CakeShop.Data.Models.Enums;
    using CakeShop.Services.Data.DessertOrders;
    using CakeShop.Services.Data.Desserts;
    using CakeShop.Services.Data.Users;
    using CakeShop.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class OrdersService : IOrdersService
    {
        private readonly IRepository<Order> ordersRepository;
        private readonly IUsersService usersService;
        private readonly IDessertsService dessertsService;
        private readonly IDessertOrdersService dessertOrdersService;

        public OrdersService(
            IRepository<Order> ordersRepository,
            IUsersService usersService,
            IDessertsService dessertsService,
            IDessertOrdersService dessertOrdersService)
        {
            this.ordersRepository = ordersRepository;
            this.usersService = usersService;
            this.dessertsService = dessertsService;
            this.dessertOrdersService = dessertOrdersService;
        }

        public async Task<string> AddToBasketAsync(string userId, string dessertId, int quantity)
        {
            var hasUserAlreadyBasket = await this.ordersRepository
                .All()
                .AnyAsync(o => o.ClientId == userId && o.OrderStatus == OrderStatus.NotFinish);

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

        public async Task<string> ChangeStatusAsync(string id, string status)
        {
            var order = await this.GetByIdAsync(id);
            order.OrderStatus = Enum.Parse<OrderStatus>(status);

            this.ordersRepository.Update(order);
            await this.ordersRepository.SaveChangesAsync();

            return order.ClientId;
        }

        public async Task DeleteAsync(string id)
        {
            var order = await this.GetByIdAsync(id);

            order.IsDeleted = true;

            this.ordersRepository.Update(order);
            await this.ordersRepository.SaveChangesAsync();
        }

        public async Task<decimal> GetTotalPriceCurrentOrderByUserAsync(string userId)
        {
            var totalPrice = await this.ordersRepository
                 .All()
                 .Where(o => o.ClientId == userId && o.OrderStatus == OrderStatus.NotFinish)
                 .Select(o => o.TotalPrice)
                 .FirstOrDefaultAsync();

            return totalPrice;
        }

        public async Task<IEnumerable<T>> RemoveFromBasketAsync<T>(string dessertOrderId, string userId)
        {
            var dessertOrderToRemove = await this.dessertOrdersService.GetByIdAsync(dessertOrderId);

            var order = await this.ordersRepository
                .All()
                .FirstOrDefaultAsync(o => o.ClientId == userId && o.OrderStatus == OrderStatus.NotFinish);

            var dessertPrice = await this.dessertsService.GetDessertPriceAsync(dessertOrderToRemove.DessertId);
            order.TotalPrice -= dessertPrice * dessertOrderToRemove.Quantity;
            order.ReviewsCount--;

            await this.dessertOrdersService.DeleteAsync(dessertOrderToRemove);

            this.ordersRepository.Update(order);
            await this.ordersRepository.SaveChangesAsync();

            var desserts = await this.dessertOrdersService.GetDessertsInBasketAsync<T>(userId);

            return desserts;
        }

        public async Task<string> GetOrderIdByUserAsync(string userId)
        {
            var orderId = await this.ordersRepository
                .All()
                .Where(o => o.ClientId == userId && o.OrderStatus == OrderStatus.NotFinish)
                .Select(o => o.Id)
                .FirstOrDefaultAsync();

            return orderId;
        }

        public async Task<string> FinishOrderAsync(string userId)
        {
            var order = await this.ordersRepository
                .All()
                .FirstOrDefaultAsync(o => o.ClientId == userId && o.OrderStatus == OrderStatus.NotFinish);

            order.IsPaid = true;
            order.OrderStatus = OrderStatus.Processing;
            order.FinalizeOrder = DateTime.UtcNow;

            this.ordersRepository.Update(order);
            await this.ordersRepository.SaveChangesAsync();

            return order.Id;
        }

        public async Task<IEnumerable<T>> GetUserOrdersListAsync<T>(string userId, int take, int skip)
        {
            var orders = await this.ordersRepository
                .All()
                .Where(o => o.ClientId == userId
                         && o.OrderStatus != OrderStatus.NotFinish
                         && o.OrderStatus != OrderStatus.Default)
                .OrderByDescending(o => o.CreatedOn)
                .Skip(skip)
                .Take(take)
                .To<T>()
                .ToListAsync();

            return orders;
        }

        public async Task AddDetailsToCurrentOrderAsync(string orderId, string deliveryAddress, string notes)
        {
            var order = await this.ordersRepository
                .All()
                .FirstOrDefaultAsync(o => o.Id == orderId);

            order.DeliveryAddress = deliveryAddress;
            order.Notes = notes;

            this.ordersRepository.Update(order);
            await this.ordersRepository.SaveChangesAsync();
        }

        public async Task<T> GetOrderDetailsAsync<T>(string orderId)
        {
            var orderDetails = await this.ordersRepository
                .All()
                .Where(o => o.Id == orderId)
                .To<T>()
                .FirstOrDefaultAsync();

            return orderDetails;
        }

        public async Task<int> GetOrdersCountCurrentUserAsync(string userId)
        {
            var ordersCount = await this.ordersRepository
                .All()
                .Where(o => o.ClientId == userId && o.OrderStatus != OrderStatus.NotFinish && o.OrderStatus != OrderStatus.Default)
                .CountAsync();

            return ordersCount;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>(string status)
        {
            var orders = await this.ordersRepository
                 .All()
                 .Where(o => o.OrderStatus == Enum.Parse<OrderStatus>(status))
                 .OrderByDescending(o => o.FinalizeOrder.Date)
                 .To<T>()
                 .ToListAsync();

            return orders;
        }

        public async Task<int> GetProcessingOrdersCountAsync()
        {
            var count = await this.ordersRepository
                .All()
                .CountAsync(o => o.OrderStatus == OrderStatus.Processing);

            return count;
        }

        public async Task UpdateOrderReviewStatusAsync(string orderId)
        {
            var order = await this.GetByIdAsync(orderId);
            order.ReviewsCount--;

            if (order.ReviewsCount == 0)
            {
                order.IsReview = true;
            }

            this.ordersRepository.Update(order);
            await this.ordersRepository.SaveChangesAsync();
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
            order.ReviewsCount++;

            await this.ordersRepository.AddAsync(order);

            return order;
        }

        private async Task<Order> AddDessertToAlreadyExistingOrderAsync(string userId, string dessertId, int quantity, decimal dessertPrice, Order order)
        {
            order = await this.ordersRepository
                                .All()
                                .FirstOrDefaultAsync(o => o.ClientId == userId && o.OrderStatus == OrderStatus.NotFinish);

            var dessertOrder = new DessertOrder()
            {
                DessertId = dessertId,
                OrderId = order.Id,
                Quantity = quantity,
            };

            order.DessertOrders.Add(dessertOrder);
            order.TotalPrice += dessertPrice * quantity;
            order.ReviewsCount++;

            this.ordersRepository.Update(order);

            return order;
        }

        private async Task<Order> GetByIdAsync(string id)
        {
            return await this.ordersRepository
                .All()
                .FirstOrDefaultAsync(o => o.Id == id);
        }
    }
}
