namespace CakeShop.Services.Data.Reviews
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CakeShop.Data.Common.Repositories;
    using CakeShop.Data.Models;
    using CakeShop.Services.Data.Orders;
    using CakeShop.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class ReviewsService : IReviewsService
    {
        private readonly IRepository<Review> reviewsRepository;
        private readonly IOrdersService ordersService;

        public ReviewsService(
            IRepository<Review> reviewsRepository,
            IOrdersService ordersService)
        {
            this.reviewsRepository = reviewsRepository;
            this.ordersService = ordersService;
        }

        public async Task AddAsync(string content, int points, string orderId, string dessertId, string userId)
        {
            var review = new Review()
            {
                Content = content,
                Points = points,
                ClientId = userId,
                DessertId = dessertId,
            };

            await this.ordersService.UpdateOrderReviewStatusAsync(orderId);

            await this.reviewsRepository.AddAsync(review);
            await this.reviewsRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetReviewsCurrentDessertAsync<T>(string dessertId)
        {
            var reviews = await this.reviewsRepository
                .All()
                .Where(r => r.DessertId == dessertId)
                .OrderByDescending(r => r.CreatedOn)
                .To<T>()
                .ToListAsync();

            return reviews;
        }
    }
}
