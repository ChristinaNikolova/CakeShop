namespace CakeShop.Services.Data.Orders
{
    using System.Linq;
    using System.Threading.Tasks;

    using CakeShop.Data.Common.Repositories;
    using CakeShop.Data.Models;
    using CakeShop.Data.Models.Enums;
    using CakeShop.Services.Data.Desserts;
    using CakeShop.Services.Data.Users;
    using Microsoft.EntityFrameworkCore;

    public class OrdersService : IOrdersService
    {
        private readonly IRepository<Order> ordersRepository;
        private readonly IUsersService usersService;
        private readonly IDessertsService dessertsService;

        public OrdersService(
            IRepository<Order> ordersRepository,
            IUsersService usersService,
            IDessertsService dessertsService)
        {
            this.ordersRepository = ordersRepository;
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
            }
            else
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
    }
}
